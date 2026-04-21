using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
[Serializable]
public class SaveItemData
{
    public Guid InstanceId { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }

    public DateTime CreationTime { get; set; }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        List<string> itemIds = DataTableManager.ItemTable.GetItemIds();
        newItem.ItemData = DataTableManager.ItemTable.Get(itemIds[Random.Range(0, itemIds.Count)]);

        return newItem;
    }

    public SaveItemData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{ItemData.StringName} {InstanceId} {CreationTime}";
    }
}
