using System;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using File = System.IO.File;

public class JsonDataService : IDataService
{
    public bool SaveData<T>(string relativePath, T saveObject)
    {
        string path = Application.persistentDataPath + relativePath;

        try
        {
            if (File.Exists(path))
            {
                Debug.Log("Data exists. Deleting old file and writing new one!");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Creating file for the first time!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unable to save data due to: {e.Message} {e.StackTrace}");
            throw;
        }

        using FileStream stream = File.Create(path);
        stream.Close();
        File.WriteAllText(path, JsonConvert.SerializeObject(saveObject));
        return true;
    }
    
    public T LoadData<T>(string relativePath)
    {
        string path = Application.persistentDataPath + relativePath;

        if (File.Exists(path) == false)
        {
            Debug.LogError($"Cannot load the file at {path}. File does not exist");
            throw new FileNotFoundException($"{path} does not exist!");
        }

        try
        {
            T saveObject = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return saveObject;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }
}
