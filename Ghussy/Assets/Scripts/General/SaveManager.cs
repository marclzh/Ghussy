using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

public class SaveManager : MonoBehaviour
{
    // Save File 
    public SaveData activeSave;
    public static SaveManager instance;

    // Flag to determine if new game
    public bool hasLoaded;

    // Used for XOR Encryption
    private static readonly string EncryptionCodeWord = "Ghussy";

    public void Awake()
    {
        instance = this;
        LoadGame();
    }

    public void SaveGame()
    {
        // Serializes Sava Data in XML Format
        string serializedData = EncryptionUtils.SerializeXML<SaveData>(activeSave);
        
        // Encrypts Data using XOR Encryption with Encryption Code Word
        string encryptedData = EncryptionUtils.XOREncryptDecrypt(serializedData);

        // Convert encrypted string to Bytes and saving to Save File
        string dataPath = Application.persistentDataPath;
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        byte[] byteArr = Encoding.UTF8.GetBytes(encryptedData);
        stream.Write(byteArr, 0, byteArr.Length);
        stream.Close();
    }

    public void LoadGame()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            // Reads Bytes from Save File and convert to readable string
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            string fileData = EncryptionUtils.ReadFileFromStream(stream);

            // Decrypts data string to original XML formatted string
            string decryptedData = EncryptionUtils.XOREncryptDecrypt(fileData);
            
            // Deserializes decrypted string and loads to active save
            activeSave = EncryptionUtils.DeserializeXML<SaveData>(decryptedData);

            stream.Close();

            // Bool flag that file has successfully loaded
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

    /*
     * Class handling the encryption, serialization logic
     */
    private class EncryptionUtils
    {
        public static string SerializeXML<T>(System.Object inputData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, inputData);
                    return sww.ToString();
                }
            }
        }

        public static T DeserializeXML<T>(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var sww = new StringReader(data))
            {
                using (XmlReader reader = XmlReader.Create(sww))
                {
                    return (T) serializer.Deserialize(reader);
                }
            }
        }

        public static string ReadFileFromStream(FileStream stream)
        {
            int totalBytes = (int)stream.Length;
            byte[] bytes = new byte[totalBytes];
            int bytesRead = 0;

            while (bytesRead < totalBytes)
            {
                int len = stream.Read(bytes, bytesRead, totalBytes);
                bytesRead += len;
            }

            string text = Encoding.UTF8.GetString(bytes);

            return text;
        }

        public static string XOREncryptDecrypt(string data)
        {
            string resultString = "";

            for (int i = 0; i < data.Length; i++)
            {
                resultString += (char)(data[i] ^ EncryptionCodeWord[i % EncryptionCodeWord.Length]);
            }

            return resultString;
        }
    }
}

/*
 * Active Save Class
 */
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
    public float projectileSize = 1f;

    // Player Stat Boons
    public float[] permBoonMultiple = new float[3]; // Movement Speed, Max Health, Projectile Size (Count of how many times permanent stats have been applied [5%])
    public bool permBoonApplied;

    // Player Shop Boons
    public bool shopBossSkeletonPurchased;
    public bool shopBossHealthDeductionPurchased;
    public bool shopEnemyNumberDeductionPurchased;

    // Player Resources
    public int ectoplasmAmount;
    public int memoryShardAmount;

    // Player Settings
    public float musicVolume = 1;
    public float soundEffectsVolume = 1;

    // Player Room Progress Data
    public bool[] roomCompleted_M = { false, false, false };
    public bool[] roomCompleted_E = { false, false, false };
    public bool[] roomCompleted_P = { false, false, false };
    public int numOfRoomsCompleted = 0;
    public int lastRoomInteractedTypeIndex;

    // Player Position
    public float[] playerPos = { 0f, 0f, 0f }; // { x. y, z } used to derive Vector3 Position

    // Player Gameplay Tracker
    public int numOfDeaths = 0;

    // Player Progress Fields
    public bool playerBaseGuide = false;
    public int savePointSceneIndex;
}