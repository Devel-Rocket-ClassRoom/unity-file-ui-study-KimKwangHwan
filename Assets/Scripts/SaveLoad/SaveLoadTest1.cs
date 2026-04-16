using UnityEngine;
using System.Collections.Generic;
using SaveDataVC = SaveDataV3;
using Random = UnityEngine.Random;

public class SaveLoadTest1 : MonoBehaviour
{
    public SaveLoadManager.SaveMode saveMode = SaveLoadManager.SaveMode.Text;

    public void Save(int slot = 0)
    {
        SaveLoadManager.Data = new SaveDataVC();
        SaveLoadManager.Data.Name = "TEST11132";
        SaveLoadManager.Data.Gold = 300000;
        List<string> ids = DataTableManager.ItemTable.GetItemIds();
        List<string> saveItems = new List<string>();
        foreach (string id in ids)
        {
            float pb = Random.value;

            if (pb > 0.5f)
            {
                saveItems.Add(id);
            }
        }
        Debug.Log($"Save Items : {string.Join(", ", saveItems)}");
        SaveLoadManager.Data.ItemList = saveItems;
        SaveLoadManager.Save(slot, saveMode);
    }

    public void Load(int slot = 0)
    {
        SaveLoadManager.Load(slot, saveMode);

        Debug.Log($"Name : {SaveLoadManager.Data.Name} / Gold : {SaveLoadManager.Data.Gold} / ItemList : {string.Join(", ", SaveLoadManager.Data.ItemList)}");
    }
}
