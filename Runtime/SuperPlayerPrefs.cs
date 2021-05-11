using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class SuperPlayerPrefs
{

    public static Dictionary<string, object> saveData = new Dictionary<string, object>();

    /// <summary>
    /// 
    /// </summary>
    static SuperPlayerPrefs()
    {
        Debug.Log($"Constructor....");
        Initialize();
    }

    /// <summary>
    /// 
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    static void Initialize()
    {
        Debug.Log($"Intializing....");

        // If we are in the editor then this could instantiated and called a number of times, so
        // we need to make sure that the subscription is removed, before we subscribe.
        Application.quitting -= OnApplicationQuit;
        Application.quitting += OnApplicationQuit;
        Load();
    }

    /// <summary>
    /// 
    /// </summary>
    private static void Load()
    {
        if (File.Exists(GetDataPath()))
        {
            using (FileStream fileStream = new FileStream(GetDataPath(), FileMode.Open))
            {
                if (fileStream == null)
                {
                    Debug.Log("ERROR: Could not load Super Preferences!");
                    return;
                }

                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    saveData = formatter.Deserialize(fileStream) as Dictionary<string, object>;
                }
                catch (Exception arg)
                {
                    Debug.Log(string.Format("Critical Error loading Super Preferences : {0}", arg));
                    return;
                }
            }

            return;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private static void OnApplicationQuit()
    {
        Save();
        Application.quitting -= OnApplicationQuit;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static string GetDataPath()
    {
        return $"{Application.persistentDataPath}/PlayerPrefsX";
    }

    /// <summary>
    /// 
    /// </summary>
    public static void Save()
    {
        try
        {
            using (FileStream fileStream = new FileStream(GetDataPath(), FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, saveData);
                fileStream.Close();
            }
        }
        catch (Exception arg)
        {
            Debug.LogError(string.Format("Critical error saving PlayerPresX data: {0}", arg));
        }
    }

    public static void DeleteAll()
    {
        saveData.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    public static void DeleteKey(string key)
    {
        saveData.Remove(key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool HasKey(string key)
    {
        return saveData.ContainsKey(key);
    }

    #region Get Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static float GetFloat(string key)
    {
        return GetFloat(key, 0f);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static float GetFloat(string key, float defaultValue)
    {
        return saveData.Get<float>(key, defaultValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetInt(string key, int defaultValue)
    {
        return saveData.Get<int>(key, defaultValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetString(string key)
    {
        return GetString(key, string.Empty);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string GetString(string key, string defaultValue)
    {
        return saveData.Get<string>(key, defaultValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool GetBool(string key)
    {
        return GetBool(key, false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static double GetDouble(string key)
    {
        return GetDouble(key, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static double GetDouble(string key, double defaultValue)
    {
        return saveData.Get<double>(key, defaultValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static decimal GetDecimal(string key)
    {
        return GetDecimal(key, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static decimal GetDecimal(string key, decimal defaultValue)
    {
        return saveData.Get<decimal>(key, defaultValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static bool GetBool(string key, bool defaultValue)
    {
        return saveData.Get<bool>(key, defaultValue);
    }

    #endregion

    #region Set Methods
    /// <summary>
    /// 
    /// </summary>
    public static void SetFloat(string key, object value)
    {
        saveData.Set<float>(key, value);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetInt(string key, object value)
    {
        saveData.Set<int>(key, value);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetString(string key, object value)
    {
        saveData.Set<string>(key, value);
    }

    public static void SetBool(string key, object value)
    {
        saveData.Set<bool>(key, value);
    }
    #endregion

}

