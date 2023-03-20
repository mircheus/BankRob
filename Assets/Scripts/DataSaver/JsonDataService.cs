using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{
    
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;
        
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
            
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }

    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        if (File.Exists(path) == false)
        {
            Debug.LogError($"Cannot load the file at {path}.File does not exist");
            throw new FileNotFoundException($"{path} does not exist!");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            Debug.Log("Loaded successfully");
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }
}
