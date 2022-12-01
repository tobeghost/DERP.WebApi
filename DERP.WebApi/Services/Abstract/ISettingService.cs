﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DERP.WebApi.Domain.Entities;
using DERP.WebApi.Infrastructure;

namespace DERP.Services.Abstract;

public interface ISettingService
{
    /// <summary>
    /// Gets a setting by identifier
    /// </summary>
    /// <param name="settingId">Setting identifier</param>
    /// <returns>Setting</returns>
    Task<Setting> GetSettingById(string settingId);

    /// <summary>
    /// Deletes a setting
    /// </summary>
    /// <param name="setting">Setting</param>
    Task DeleteSetting(Setting setting);

    /// <summary>
    /// Get setting by key
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
    /// <returns>Setting</returns>
    Task<Setting> GetSetting(string key, bool loadSharedValueIfNotFound = false);

    /// <summary>
    /// Get setting value by key
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="key">Key</param>
    /// <param name="defaultValue">Default value</param>
    /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
    /// <returns>Setting value</returns>
    T GetSettingByKey<T>(string key, T defaultValue = default(T), bool loadSharedValueIfNotFound = false);

    /// <summary>
    /// Set setting value
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="key">Key</param>
    /// <param name="value">Value</param>
    /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
    Task SetSetting<T>(string key, T value, bool clearCache = true);

    /// <summary>
    /// Gets all settings
    /// </summary>
    /// <returns>Settings</returns>
    IList<Setting> GetAllSettings();

    /// <summary>
    /// Determines whether a setting exists
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TPropType">Property type</typeparam>
    /// <param name="settings">Settings</param>
    /// <param name="keySelector">Key selector</param>
    /// <returns>true -setting exists; false - does not exist</returns>
    bool SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();

    /// <summary>
    /// Load settings
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    T LoadSetting<T>() where T : ISettings, new();

    /// <summary>
    /// Load settings
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    ISettings LoadSetting(Type type);

    /// <summary>
    /// Save settings object
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="settings">Setting instance</param>
    Task SaveSetting<T>(T settings) where T : ISettings, new();

    /// <summary>
    /// Save settings object
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TPropType">Property type</typeparam>
    /// <param name="settings">Settings</param>
    /// <param name="keySelector">Key selector</param>
    /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
    Task SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, bool clearCache = true)
        where T : ISettings, new();

    /// <summary>
    /// Delete all settings
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    Task DeleteSetting<T>() where T : ISettings, new();

    /// <summary>
    /// Delete settings object
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TPropType">Property type</typeparam>
    /// <param name="settings">Settings</param>
    /// <param name="keySelector">Key selector</param>
    Task DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();

    /// <summary>
    /// Clear cache
    /// </summary>
    Task ClearCache();
}