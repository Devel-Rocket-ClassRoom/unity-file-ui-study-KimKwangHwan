using UnityEngine;
using System.IO;
using System.Collections.Generic;
public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    public Dictionary<string, int> score = new Dictionary<string, int>
    {
        {"stage1", 100 },
        {"stage2", 200 },
    }; // Unity JsonUtility가 지원하지 않음
}

public class JsonUtilityTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Save
            PlayerInfo obj = new PlayerInfo
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
            };

            string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest");

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            string path = Path.Combine(pathFolder, "player.json");

            string json = JsonUtility.ToJson(obj, prettyPrint: true);
            File.WriteAllText(path, json);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Load
            string path = Path.Combine(Application.persistentDataPath, "JsonTest", "player.json");
            string json = File.ReadAllText(path);

            //PlayerInfo obj = JsonUtility.FromJson<PlayerInfo>(json);

            PlayerInfo obj = new PlayerInfo();
            JsonUtility.FromJsonOverwrite(json, obj);

            Debug.Log(json);
            Debug.Log($"{obj.playerName} / {obj.lives}");
        }
    }
}
