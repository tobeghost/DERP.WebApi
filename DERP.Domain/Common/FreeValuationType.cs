using System.ComponentModel;

namespace DERP.Domain.Common;

public partial class FreeValuationType : Dictionary<string, object>
{
    public T GetValue<T>(string key)
    {
        try
        {
            if (!this.TryGetValue(key, out object value)) return default(T);
            if (value == null) return default(T);
            
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                return (T)converter.ConvertFromString(value.ToString());
            }

            return default(T);
        }
        catch (NotSupportedException)
        {
            return default(T);
        }
    }
}