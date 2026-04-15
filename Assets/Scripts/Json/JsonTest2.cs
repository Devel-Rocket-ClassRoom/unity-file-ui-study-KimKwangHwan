using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Random = UnityEngine.Random;

[Serializable]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
    public string name;

    public override string ToString()
    {
        return $"{name} / {pos} / {rot} / {scale} / {color}";
    }
}

public class JsonTest2 : MonoBehaviour
{
    public string fileName = "some.json";
    
    private Dictionary<string, GameObject> prefabDict;

    public readonly static string cubeName = "Cube";
    public readonly static string sphereName = "Sphere";
    public readonly static string capsuleName = "Capsule";
    public readonly static string cylinderName = "Cylinder";

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject capsulePrefab;
    [SerializeField] private GameObject cylinderPrefab;

    private List<GameObject> objList;
    private List<string> keyList;

    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);
    private JsonSerializerSettings jsonSettings;

    private void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());

        prefabDict = new Dictionary<string, GameObject>();

        prefabDict[cubeName] = cubePrefab;
        prefabDict[sphereName] = spherePrefab;
        prefabDict[capsuleName] = capsulePrefab;
        prefabDict[cylinderName] = cylinderPrefab;

        objList = new List<GameObject>();
        keyList = new List<string>() { cubeName, sphereName, capsuleName, cylinderName };
    }

    public void Save()
    {
        List<SomeClass> scList = new List<SomeClass>();
        foreach (var obj in objList)
        {
            SomeClass sc = new SomeClass()
            {
                pos = obj.transform.position,
                rot = obj.transform.rotation,
                scale = obj.transform.localScale,
                color = obj.GetComponent<Renderer>().material.color,
                name = obj.tag
            };
            scList.Add(sc);
        }

        string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest");

        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        string json = JsonConvert.SerializeObject(scList, Formatting.Indented, jsonSettings);
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        Clear();
        string json = File.ReadAllText(FileFullPath);
        List<SomeClass> scList = JsonConvert.DeserializeObject<List<SomeClass>>(json);

        foreach (var sc in scList)
        {
            GenObject(sc);
        }
    }

    public void Create()
    {
        int objNum = Random.Range(1, 11);

        for (int i = 0; i < objNum; i++)
        {
            SomeClass sc = new SomeClass()
            {
                pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5f,
                rot = Random.rotation,
                scale = new Vector3(Random.Range(0.1f, 3f), Random.Range(0.1f, 3f), Random.Range(0.1f, 3f)),
                color = new Color(Random.value, Random.value, Random.value, 1f),
                name = keyList[Random.Range(0, keyList.Count)]
            };
            GenObject(sc);
        }
    }
    public void Clear()
    {
        foreach(var obj in objList)
        {
            Destroy(obj);
        }
        objList.Clear();
    }

    private void GenObject(SomeClass sc)
    {
        if (prefabDict.ContainsKey(sc.name))
        {
            GameObject obj = Instantiate(prefabDict[sc.name], sc.pos, sc.rot);
            obj.transform.localScale = sc.scale;
            obj.GetComponent<Renderer>().material.color = sc.color;
            objList.Add(obj);
        }
    }
}
