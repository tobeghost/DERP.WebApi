using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DERP.Services.Abstract;
using DERP.WebApi.Domain.Entities;
using DERP.WebApi.Infrastructure;
using DERP.WebApi.Infrastructure.Context;
using DERP.WebApi.Infrastructure.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DERP.Services.Concrete;

public class SettingService : ISettingService
{
    private readonly DerpContext _derpContext;

    public SettingService(DerpContext derpContext)
    {
        _derpContext = derpContext;
    }

    public Task ClearCache()
    {
        throw new NotImplementedException();
    }

    public Task DeleteSetting(Setting setting)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSetting<T>() where T : ISettings, new()
    {
        throw new NotImplementedException();
    }

    public Task DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector)
        where T : ISettings, new()
    {
        throw new NotImplementedException();
    }

    public IList<Setting> GetAllSettings()
    {
        return _derpContext.Setting.Find(new BsonDocument()).SortBy(x => x.Name).ToList();
    }

    public Task<Setting> GetSetting(string key, bool loadSharedValueIfNotFound = false)
    {
        throw new NotImplementedException();
    }

    public Task<Setting> GetSettingById(string settingId)
    {
        throw new NotImplementedException();
    }

    public T GetSettingByKey<T>(string key, T defaultValue = default, bool loadSharedValueIfNotFound = false)
    {
        if (String.IsNullOrEmpty(key))
            return defaultValue;

        var settings = GetAllSettings();
        var setting = settings.FirstOrDefault(i => i.Name == key);
        if (setting != null)
        {
            return CommonHelper.To<T>(setting.Value);
        }

        return defaultValue;
    }

    public T LoadSetting<T>() where T : ISettings, new()
    {
        throw new NotImplementedException();
    }

    public ISettings LoadSetting(Type type)
    {
        var settings = Activator.CreateInstance(type);

        foreach (var prop in type.GetProperties())
        {
            // get properties we can read and write to
            if (!prop.CanRead || !prop.CanWrite)
                continue;

            var key = type.Name + "." + prop.Name;
            //load by store
            var setting = GetSettingByKey<string>(key, loadSharedValueIfNotFound: true);
            if (setting == null || setting.Length == 0)
                continue;

            var converter = TypeDescriptor.GetConverter(prop.PropertyType);

            if (!converter.CanConvertFrom(typeof(string)))
                continue;
            try
            {
                var value = converter.ConvertFromInvariantString(setting);
                //set property
                prop.SetValue(settings, value, null);
            }
            catch (Exception ex)
            {
                //var msg = $"{key} ayarı türe dönüştürülemedi { prop.PropertyType.FullName}";
                //_mediator.Send(new InsertLogCommand() { LogLevel = Domain.Logging.LogLevel.Error, ShortMessage = msg, FullMessage = ex.Message });
            }
        }

        return settings as ISettings;
    }

    public Task SaveSetting<T>(T settings) where T : ISettings, new()
    {
        throw new NotImplementedException();
    }

    public Task SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector,
        bool clearCache = true) where T : ISettings, new()
    {
        throw new NotImplementedException();
    }

    public Task SetSetting<T>(string key, T value, bool clearCache = true)
    {
        throw new NotImplementedException();
    }

    public bool SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector)
        where T : ISettings, new()
    {
        throw new NotImplementedException();
    }
}