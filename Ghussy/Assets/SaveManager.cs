using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Kryz.CharacterStats;

public class SaveManager : MonoBehaviour
{
    public SaveData activeSave;

    public static SaveManager instance;

    public bool hasLoaded;

    public void Awake()
    {
        instance = this;

        LoadGame();

    }

    public void SaveGame()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
    }

    public void LoadGame()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            hasLoaded = true;
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Game Saved");
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Save Loaded");
            LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Save Deleted");
            DeleteSaveData();
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;

    // Default Player Stat Values
    public float movementSpeedValue = 1f;
    public float fireRateValue;
    public float damageModifierValue;
    public float maxHealthValue = 100f;
    public float currentHealthValue = 100f;
    public float maxTransformationValue = 100f;
    public float currentTransformationValue = 100f;

    // Player Settings
    public float musicVolume = 1;
    public float soundEffectsVolume = 1;
}