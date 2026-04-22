using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Random = UnityEngine.Random;
[Serializable]
public class SaveCharacterData
{
    public Guid InstanceId { get; set; }

    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }

    public DateTime CreationTime { get; set; }

    public SaveItemData EquipArmor { get; set; }
    public SaveItemData EquipWeapon { get; set; }

    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newChar = new SaveCharacterData();
        List<string> charIds = DataTableManager.CharacterTable.GetCharIds();
        newChar.CharacterData = DataTableManager.CharacterTable.Get(charIds[Random.Range(0, charIds.Count)]);
        UnityEngine.Debug.Log(newChar.CharacterData.StringName);
        return newChar;
    }

    public SaveCharacterData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"";
    }
}
