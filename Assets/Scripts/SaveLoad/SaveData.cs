using System.Collections.Generic;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}

[System.Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;

        return saveData;
    }
}

[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold { get; set; } = 0;
    public SaveDataV2()
    {
        Version = 2;
    }
    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV3();
        saveData.Name = Name;
        saveData.Gold = Gold;
        return saveData;
    }
}

// SaveDataV3
// ItemList 추가 (ItemTable "ItemId")
[System.Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold { get; set; } = 0;
    public List<string> ItemList = new List<string>();

    public SaveDataV3()
    {
        Version = 3;
    }
    public override SaveData VersionUp()
    {
        SaveDataV4 data = new SaveDataV4();
        data.Name = Name;
        data.Gold = Gold;
        foreach (string id in ItemList)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.ItemData = DataTableManager.ItemTable.Get(id);
            data.ItemList.Add(itemData);
        }
        data.invenSortingOption = UIInvenSlotList.InvenSortingOptions.CreationTimeAsscending;
        data.invenFilteringOption = UIInvenSlotList.InvenFilteringOptions.None;
        return data;
    }
}

[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> ItemList = new List<SaveItemData>();
    public List<SaveCharacterData> CharList = new List<SaveCharacterData>();

    public UIInvenSlotList.InvenSortingOptions invenSortingOption;
    public UIInvenSlotList.InvenFilteringOptions invenFilteringOption;
    public UICharacterSlotList.CharSortingOptions charSortingOption;
    public UICharacterSlotList.CharFilteringOptions charFilteringOption;

    public SaveDataV4()
    {
        Version= 4;
    }
    public override SaveData VersionUp()
    {
        return base.VersionUp();
    }
}