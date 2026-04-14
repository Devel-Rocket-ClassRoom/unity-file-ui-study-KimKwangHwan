using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Localization : MonoBehaviour
{
    public string id;
    public Languages language;
    public TextMeshProUGUI text;

    public string Id
    {
        get { return id; }
        set
        {
            if (id == value)
                return;
            id = value;
            OnChangedId();
        }
    }

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            OnChangedId();
        }
#if UNITY_EDITOR
        else
        {
            OnChangedLanguage(language);
            //OnChangedId();
        }
        Variables.OnLanguageChanged += OnChangedLanguage;
#endif
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangedLanguage(language);
        //OnChangedId();
#else
        OnChangeLanguage();
        //OnChangedId();
#endif
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= OnChangedLanguage;
    }

    public void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }
    public void OnChangedLanguage()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }
#if UNITY_EDITOR
    private void OnChangedLanguage(Languages lang)
    {
        var stringTable = DataTableManager.GetStringTable(lang);
        text.text = stringTable.Get(id);
    }
#endif
#if UNITY_EDITOR
    [ContextMenu("ChangeLanguage")]
    private void ChangeLanguage()
    {
        Variables.Language = language;
    }
#endif
}
