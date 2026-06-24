using System.IO;
using UnityEngine;

public class SaveSystem
{
    public SaveData saveFile;
    public bool IsSaveLoaded {  get; private set; }
    private string path => Application.persistentDataPath + "/save.json";
    
    public SaveSystem()
    {
        IsSaveLoaded = Load();
        Debug.Log($"path{path}");
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public SaveData GetSave()
    {
        return saveFile;
    }

    private bool Load()
    {
        if (!File.Exists(path))
        {
            return false;
        }

        string json = File.ReadAllText(path);
        saveFile = JsonUtility.FromJson<SaveData>(json);
        return true;
    }
}