using UnityEngine;
using SaveDataVC = SaveDataV3;
using Newtonsoft.Json;
using System.IO;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text, // (.json)
        Encrypted // (.dat)
    }

    public static SaveMode Mode { get; set; } = SaveMode.Encrypted;

    private static string Ext => Mode == SaveMode.Text ? ".json" : ".dat";

    public static int SaveDataVersion { get; } = 3;

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save"; 

    private static string[] SaveFileNames => new string[]
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3"
    };

    private static string GetSaveFileName(int slot, SaveMode mode)
    {
        return $"{SaveFileNames[slot]}{(mode == SaveMode.Text ? ".json" : ".dat")}";
    }

    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
        // 컨버터 추가

    };

    public static bool Save(int slot = 0)
    {
        return Save(slot, Mode);
    }

    public static bool Save(int slot, SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            string path = Path.Combine(SaveDirectory, GetSaveFileName(slot, mode));
            string json = JsonConvert.SerializeObject(Data, settings);

            switch (mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    byte[] encrypted = CryptoUtil.Encrypt(json);
                    File.WriteAllBytes(path, encrypted);
                    break;
            }
            
            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        return Load(slot, Mode);
    }

    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }
        string path = Path.Combine(SaveDirectory, GetSaveFileName(slot, mode));

        if (!File.Exists(path))
        {
            return false;
        }
        try
        {
            string json = "";

            switch (mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    byte[] bytes = File.ReadAllBytes(path);
                    json = CryptoUtil.Decrypt(bytes);
                    break;
            }

            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);

            while (saveData.Version < SaveDataVersion)
            {
                saveData = saveData.VersionUp();
            }

            Data = saveData as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }
    }
}