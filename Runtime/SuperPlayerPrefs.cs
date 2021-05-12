using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public static class SuperPlayerPrefs
{

    private static Dictionary<string, object> saveData = new Dictionary<string, object>();

    /// <summary>
    /// 
    /// </summary>
    static SuperPlayerPrefs()
    {
        Debug.Log($"Constructor....");

#if UNITY_EDITOR
        // If we are in the editor then this could instantiated and called a number of times, so
        // we need to make sure that the subscription is removed, before we subscribe.
        Application.quitting -= OnApplicationQuit;
#endif
        Initialize();
    }

    /// <summary>
    /// 
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        Debug.Log($"Intializing....");
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
                    Debug.Log("ERROR: Could not load SuperPlayerPrefs!");
                    return;
                }

                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    saveData = formatter.Deserialize(fileStream) as Dictionary<string, object>;
                }
                catch (Exception arg)
                {
                    Debug.Log(string.Format("Critical Error loading SuperPlayerPrefs : {0}", arg));
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
        return $"{Application.persistentDataPath}/SuperPlayerPrefs";
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
            Debug.LogError(string.Format("Critical error saving SuperPlayerPrefs data: {0}", arg));
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T Get<T>(string key)
    {
        return Get<T>(key, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static T Get<T>(string key, object defaultValue)
    {
        if (saveData.ContainsKey(key))
        {
            return (T)saveData[key];
        }
        else
        {
            return (T)defaultValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void Set<T>(string key, object value)
    {
        saveData.Add(key, (T)value);
    }

}
