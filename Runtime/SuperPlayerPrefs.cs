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
    /// Constructor for our class.
    /// </summary>
    static SuperPlayerPrefs()
    {
#if UNITY_EDITOR
        // If we are in the editor then this could instantiated and called a number of times, so
        // we need to make sure that the subscription is removed, before we subscribe.

        Application.quitting -= OnApplicationQuit;
#endif
        Initialize();
    }

    /// <summary>
    /// Initialise method that subscribes to the application on quiting event, and then loads the
    /// data into memory.
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        Application.quitting += OnApplicationQuit;
        Load();
    }

    /// <summary>
    /// Load method
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
        }
    }

    /// <summary>
    /// OnQuit method, which gets called when the game is exiting.
    /// </summary>
    private static void OnApplicationQuit()
    {
        Save();
        Application.quitting -= OnApplicationQuit;
    }

    /// <summary>
    /// Gets the datapath and name to where the save will be saved, and loaded.
    /// </summary>
    /// <returns></returns>
    private static string GetDataPath()
    {
        return $"{Application.persistentDataPath}/SuperPlayerPrefs";
    }

    /// <summary>
    /// Save method
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

    /// <summary>
    /// Deletes all entries from the dictionary.
    /// </summary>
    public static void DeleteAll()
    {
        saveData.Clear();
    }

    /// <summary>
    /// Deletes a key from the dictionary
    /// </summary>
    /// <param name="key">name of entry to delete from the dictionary</param>
    public static void DeleteKey(string key)
    {
        saveData.Remove(key);
    }

    /// <summary>
    /// Checks to see if the dictionary has a specific key.
    /// </summary>
    /// <param name="key">key to search for</param>
    /// <returns></returns>
    public static bool HasKey(string key)
    {
        return saveData.ContainsKey(key);
    }

    /// <summary>
    /// Returns the specific object from the saved data with the specified key, if found.
    /// </summary>
    /// <param name="key">retrieve object with this key</param>
    /// <returns></returns>
    public static T Get<T>(string key)
    {
        return Get<T>(key, 0);
    }

    /// <summary>
    /// Returns the specific object from the saved data, using the key. If it is not found then
    /// return it as the specified default value.
    /// </summary>
    /// <param name="key">get with the key.</param>
    /// <param name="defaultValue">set to this value if can't be found.</param>
    /// <returns></returns>
    public static T Get<T>(string key, object defaultValue)
    {
        if (saveData.ContainsKey(key))
        {
            return (T)saveData[key];
        }
        else
        {
            if (typeof(T).IsValueType)
                return (T)defaultValue;
            else
                return (T)default;
        }
    }

    /// <summary>
    /// Save the specified object of type in the saved data, with the specified key.
    /// </summary>
    /// <typeparam name="T">Specifies the object Type</typeparam>
    /// <param name="key">will be index by the key</param>
    /// <param name="value">value to save in the dictionary</param>
    public static void Set<T>(string key, object value)
    {
        if(saveData.ContainsKey(key))
        {
            saveData[key] = value;
        } else
        {
            saveData.Add(key, (T)value);
        }
    }

}
