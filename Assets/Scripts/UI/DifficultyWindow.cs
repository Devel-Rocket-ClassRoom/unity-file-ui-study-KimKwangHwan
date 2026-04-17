using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;

    private int selected = 1;

    private string directoryPath;
    private string fileName = "Difficulty.txt";

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
        directoryPath = Path.Combine(Application.persistentDataPath, "Difficulty");
    }

    public override void Open()
    {
        string filePath = Path.Combine(directoryPath, fileName);
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            selected = int.Parse(data);
        }
        toggles[selected].isOn = true;
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if (active)
        {
            selected = 0;
        }
    }
    public void OnNormal(bool active)
    {
        if (active)
        {
            selected = 1;
        }
    }
    public void OnHard(bool active)
    {
        if (active)
        {
            selected = 2;
        }
    }

    public void OnCancelButtonClick()
    {
        windowManager.Open(0);
    }

    public void OnApplyButtonClick()
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        string filePath = Path.Combine(directoryPath, fileName);
        File.WriteAllText(filePath, $"{selected}");

        windowManager.Open(0);
    }
}
