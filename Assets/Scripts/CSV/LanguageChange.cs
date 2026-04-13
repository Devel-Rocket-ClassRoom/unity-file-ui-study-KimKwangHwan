using UnityEngine;

public class LanguageChange : MonoBehaviour
{
    public void OnClickButtonKR()
    {
        Variables.Language = Languages.Korean;
    }
    public void OnClickButtonEN()
    {
        Variables.Language = Languages.English;
    }
    public void OnClickButtonJP()
    {
        Variables.Language = Languages.Japanese;
    }
}
