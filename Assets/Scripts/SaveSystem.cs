using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public struct SavedData
{
    public int saved_level;
    public int saved_points;
}

public class SaveSystem
{
    public void SaveGame(SavedData info)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        string destination = Application.persistentDataPath + "/save.dat";

        if (File.Exists(destination))
            file = File.OpenWrite(destination);
        else
            file = File.Create(destination);

        Debug.Log("Saving level: " + info.saved_level);
        Debug.Log("Saving points: " + info.saved_points);
        bf.Serialize(file, info);
        file.Close();
        Debug.Log("Game Saved");
    }

    public SavedData LoadGame()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;
        SavedData data;
        data.saved_level = 0;
        data.saved_points = 0;
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(destination))
            file = File.OpenRead(destination);
        else
        {
            Debug.Log("File not found");
            return data;
        }

        BinaryFormatter bf = new BinaryFormatter();
        data = (SavedData)bf.Deserialize(file);
        file.Close();

        Debug.Log("Level loaded: " + data.saved_level);
        Debug.Log("Points loaded: " + data.saved_points);
        return data;
    }
}
