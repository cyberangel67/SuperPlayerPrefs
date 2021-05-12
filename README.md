
# SuperPlayerPrefs
 
Is a small library that can be used in replace of PlayerPrefs, it behaves in the same manner as PlayerPres, only that SuperPlayerPrefs saves the data to the games data directory. Where as during development, if one is not careful, you could end up with a million entries over n games. So rather than bog down the registry on Windows, while in development, you can jst simpley delete the file from the games save directory.

Therefore because it follows the Application persistentDataPath then the variables can be found in the following locations.

**Windows Store Apps:** *Application.persistentDataPath points to %userprofile%\AppData\Local\Packages\<productname>\LocalState.*

**Windows Editor and Standalone Player:** *Application.persistentDataPath usually points to %userprofile%\AppData\LocalLow\<companyname>\<productname>. It is resolved by SHGetKnownFolderPath with FOLDERID_LocalAppDataLow, or SHGetFolderPathW with CSIDL_LOCAL_APPDATA if the former is not available*.

**WebGL:** *Application.persistentDataPath points to /idbfs/<md5 hash of data path> where the data path is the URL stripped of everything including and after the last '/' before any '?' components.*

**Linux:** *Application.persistentDataPath points to $XDG_CONFIG_HOME/unity3d or $HOME/.config/unity3d.*

**iOS:** *Application.persistentDataPath points to /var/mobile/Containers/Data/Application/<guid>/Documents.*

**tvOS:** *Application.persistentDataPath is not supported and returns an empty string.*

**Android:** *Application.persistentDataPath points to /storage/emulated/0/Android/data/<packagename>/files on most devices (some older phones might point to location on SD card if present), the path is resolved using android.content.Context.getExternalFilesDir.*

**Mac:** *Application.persistentDataPath points to the user Library folder. (This folder is often hidden.) In recent Unity releases user data is written into ~/Library/Application Support/company name/product name. Older versions of Unity wrote into the ~/Library/Caches folder, or ~/Library/Application Support/unity.company name.product name. These folders are all searched for by Unity. The application finds and uses the oldest folder with the required data on your system*

## Insallation
At the presnt time this will not work as a package, but once I figure out why it is not working, it will be updated. For the present time, all you need to do is download the files and install into your project, and that is it. It will self setup and be ready to go from there.

## Usage
Just like PlayerPrefs it isn't difficult to use, and because it is simplistic in nature, it can save all types of data.

**Example usage**

```javascript
    // Get a string
    string testString = SuperPlayerPrefs.Get<string>("test");
    
    // Get a string with a default value 
    string testString = SuperPlayerPrefs.Get<string>("Test", "default value to return");
    
    // Get a boolean
    bool levelCompleted = SuperPlayerPrefs.Get<string>("Level1_Completed", false);
    
    //Storing/Saving a variable
    SuperPlayerPrefs.Set<string>("Test");
    
    // can also save serialised classes
    [Serializable]
    public class TestClass {
	    public string Name;
	    public string Address;
	    public int Age;
    }
    
    public class TestScript : Monobehaviour {
	    TestClass tc = new TestClass();
	    tc.Name = "Test Name";
	    tc.Address = "Somewhere around the internet";
	    tc.Age = 2021;
	    
	    SuperPlayerPrefs.Set<TestClass>("MainPerson", tc);
    }
```