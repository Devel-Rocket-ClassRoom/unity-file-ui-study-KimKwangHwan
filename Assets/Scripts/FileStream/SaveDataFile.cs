using UnityEngine;
using System.IO;

public class SaveDataFile : MonoBehaviour
{
    private int saveIndex = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            string path = Path.Combine(Application.persistentDataPath, "SaveData");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.Log($"폴더 생성: {path}");
            }
            else
            {
                Debug.Log($"폴더 존재: {path}");
            }

            string filePath = Path.Combine(path, $"save{saveIndex++}.txt");

            string config = $"volume={Random.Range(0, 55)}\nresolution={Random.Range(960, 1921)}x{Random.Range(480, 1081)}\nfullscreen={Random.Range(0, 2)}";
            File.WriteAllText(filePath, config);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            string path = Path.Combine(Application.persistentDataPath, "SaveData");
            
            string[] files = Directory.GetFiles(path);
            Debug.Log("=== 세이브 파일 목록 ===");
            foreach (string file in files)
            {
                Debug.Log($"- {Path.GetFileName(file)} ({Path.GetExtension(file)})");
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            string srcPath = Path.Combine(Application.persistentDataPath, "SaveData", "save1.txt");
            string dstPath = Path.Combine(Application.persistentDataPath, "SaveData", "save1_backup.txt");
            if (File.Exists(srcPath))
            {
                File.Copy(srcPath, dstPath);
                Debug.Log("save1.txt -> save1_backup.txt 복사 완료");
            }
            else
            {
                Debug.Log("복사할 파일이 존재하지 않습니다.");
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            string path = Path.Combine(Application.persistentDataPath, "SaveData", "save3.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("save3.txt 삭제 완료");
            }
            else
            {
                Debug.Log("삭제할 파일이 존재하지 않습니다.");
            }
        }
    }
}
