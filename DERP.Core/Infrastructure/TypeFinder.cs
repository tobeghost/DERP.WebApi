namespace DERP.Core.Infrastructure;

public static class TypeFinder
{
    public static IEnumerable<T> FindClassesOfType<T>(bool onlyConcreteClasses = true)
    {
        var result = new List<Type>();

        var assignTypeFrom = typeof(T);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            if (types == null) continue;

            foreach (var t in types)
            {
                if (!assignTypeFrom.IsAssignableFrom(t) && (!assignTypeFrom.IsGenericTypeDefinition || !DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                    continue;
                
                if (t.IsInterface)
                    continue;

                if (onlyConcreteClasses)
                {
                    if (t.IsClass && !t.IsAbstract)
                    {
                        result.Add(t);
                    }
                }
                else
                {
                    result.Add(t);
                }
            }
        }

        return result.Select(s => (T)Activator.CreateInstance(s));
    }
    
    private static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
    {
        try
        {
            var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                    continue;

                var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                return isMatch;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}