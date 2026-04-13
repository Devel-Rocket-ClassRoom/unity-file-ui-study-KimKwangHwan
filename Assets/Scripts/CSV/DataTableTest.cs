using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    public string NameStringTableKr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    public void OnClickStringTableKr()
    {
        Debug.Log(DataTableManager.StringTable.Get("ATTACK"));
    }

    public void OnClickStringTableEn()
    {
        var table = new StringTable();
        table.Load(NameStringTableEn);
        Debug.Log(table.Get("ATTACK"));
    }

    public void OnClickStringTableJp()
    {
        var table = new StringTable();
        table.Load(NameStringTableJp);
        Debug.Log(table.Get("ATTACK"));
    }
}
