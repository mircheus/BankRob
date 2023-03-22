using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using File = System.IO.File;

public class SaveManager
{
    public bool Save(Data saveObject)
    {
        string path = Application.persistentDataPath + "/savegame.json";

        if (File.Exists(path))
        {
            Debug.Log("Data exists. Deleting old file and writing new one!");
            File.Delete(path);
        }
        else
        {
            Debug.Log("Creating file for the first time!");
        }
        
        using FileStream stream = File.Create(path);
        stream.Close();
        // File.WriteAllText(path, JsonUtility.ToJson(saveObject));
        File.WriteAllText(path, JsonConvert.SerializeObject(saveObject));
        return true;
    }

    // public Data Load()
    // {
    //     string path = Application.persistentDataPath + "/savegame.json";
    //     // Data saveObject = JsonUtility.FromJson<Data>(File.ReadAllText(path));
    //     Data saveObject = JsonConvert.DeserializeObject<Data>(File.ReadAllText(path));
    //     
    //     return saveObject;
    // }

    public T Load<T>()
    {
        string path = Application.persistentDataPath + "/savegame.json";
        T saveObject = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        return saveObject;
    }
}
