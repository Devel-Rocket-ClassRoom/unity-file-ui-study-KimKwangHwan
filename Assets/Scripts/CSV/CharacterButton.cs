using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string id;

    public static event Action<string> OnClick;

    private void OnEnable()
    {
        ChangeId();
    }

    private void OnValidate()
    {
        ChangeId();
    }

    private void ChangeId()
    {
        CharacterData characterData = DataTableManager.CharacterTable.Get(id);

        if (characterData != null)
        {
            iconImage.sprite = characterData.SpriteIcon;
            text.GetComponent<Localization>().id = characterData.Name;
        }
    }

    public void OnButtonClicked()
    {
        OnClick?.Invoke(id);
    }
}
