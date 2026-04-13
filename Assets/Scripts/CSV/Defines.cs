public enum Languages
{
    Korean,
    English,
    Japanese,
}

public static class Variables
{
    public static event System.Action OnLanguageChanged; // 언어 변할 때 함수 이벤트로 추가
    public static Languages language = Languages.Korean;
    public static Languages Language
    {
        get
        {
            return language;
        }
        set
        {
            if (language == value)
            {
                return;
            }
            language = value;
            OnLanguageChanged?.Invoke();
        }
    }
}

public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
    };
    public static string String => StringTableIds[(int)Variables.Language];
}