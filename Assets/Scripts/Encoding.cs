using UnityEngine;
using System.IO;
using System.Linq.Expressions;

public class Encoding : MonoBehaviour
{
    string config;
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveData", "secret.txt");
        config = "Hello World. My name is KIM KWANG HWAN.";
        Debug.Log($"원본: {config}");
        File.WriteAllText(path, config);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            using (FileStream read = new FileStream(Path.Combine(Application.persistentDataPath, "SaveData", "secret.txt"), FileMode.Open))
            using (FileStream write = new FileStream(Path.Combine(Application.persistentDataPath, "SaveData", "encrypted.dat"), FileMode.Create))
            {
                int fileSize = 0;
                while(true)
                {
                    int data = read.ReadByte();
                    fileSize++;
                    if (data == -1)
                    {
                        break;
                    }
                    
                    write.WriteByte((byte)(data ^ 0xAB));
                }
                Debug.Log($"암호화 완료 (파일 크기: {fileSize} bytes)");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            using (FileStream read = new FileStream(Path.Combine(Application.persistentDataPath, "SaveData", "encrypted.dat"), FileMode.Open))
            using (FileStream write = new FileStream(Path.Combine(Application.persistentDataPath, "SaveData", "decrypted.txt"), FileMode.Create))
            {
                while (true)
                {
                    int data = read.ReadByte();
                    if (data == -1)
                    {
                        break;
                    }

                    write.WriteByte((byte)(data ^ 0xAB));
                }
                Debug.Log("복호화 완료");
            }

            using (StreamReader sr = new StreamReader(Path.Combine(Application.persistentDataPath, "SaveData", "decrypted.txt")))
            {
                string allText = sr.ReadToEnd();
                Debug.Log($"복호화 결과: {allText}");
                Debug.Log($"원본과 일치: {allText == config}");
            }
        }
    }
}
