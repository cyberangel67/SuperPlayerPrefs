using System.IO;
using UnityEngine;
using UnityEditor;

public class Cleardata : MonoBehaviour
{
    /// <summary>
    /// Create a menu item that deletes the saved data.
    /// </summary>
    [MenuItem("Studious/Super Preferences/Clear Saved Data")]
    private static void ClearSavedData()
    {
        if (File.Exists(GetDataPath()))
            File.Delete(GetDataPath());
    }

    /// <summary>
    /// Create data save string
    /// </summary>
    /// <returns>File and path to saved data file</returns>
    private static string GetDataPath()
    {
        return $"{Application.persistentDataPath}/SuperPlayerPrefs";
    }

}
