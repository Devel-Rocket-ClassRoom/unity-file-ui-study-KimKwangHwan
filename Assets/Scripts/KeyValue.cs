using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class KeyValue : MonoBehaviour
{
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = Path.Combine(path, "settings.cfg");

        using (FileStream fs = File.Create(path))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine("master_volume=80");
            sw.WriteLine("bgm_volume=70");
            sw.WriteLine("sfx_volume=90");
            sw.WriteLine("language=kr");
            sw.WriteLine("show_damage=true");
        }
        Debug.Log($"파일 생성: {path}");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            string path = Path.Combine(Application.persistentDataPath, "SaveData", "settings.cfg");
            Dictionary<string, string> dict = new Dictionary<string, string>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine();
                    string[] keyValue = line.Split('=');
                    dict.Add(keyValue[0], keyValue[1]);
                }
            }
            dict["bgm_volume"] = "50";
            dict["language"] = "en";

            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (string key in dict.Keys)
                {
                    sw.WriteLine($"{key}={dict[key]}");
                }
            }
            string text = File.ReadAllText(path);
            Debug.Log(text);
        }
    }
}
