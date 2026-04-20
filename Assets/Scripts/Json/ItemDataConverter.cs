using Newtonsoft.Json;
using System;

public class ItemDataConverter : JsonConverter<ItemData>
{
    public override ItemData ReadJson(JsonReader reader, Type objectType, ItemData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string id = reader.Value as string;
        return DataTableManager.ItemTable.Get(id);
    }

    public override void WriteJson(JsonWriter writer, ItemData value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Id);
    }
}