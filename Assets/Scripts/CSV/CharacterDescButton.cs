using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterDescButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Localization text;
    [SerializeField] private Localization desc;
    [SerializeField] private Localization hpLabel;
    [SerializeField] private Localization atkLabel;
    [SerializeField] private Localization defLabel;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI atk;
    [SerializeField] private TextMeshProUGUI def;

    public void SetEmpty()
    {
        iconImage.sprite = null;
        text.id = string.Empty;
        desc.id = string.Empty;
        hpLabel.id = string.Empty;
        atkLabel.id = string.Empty;
        defLabel.id = string.Empty;
        hp.text = string.Empty;
        atk.text = string.Empty;
        def.text = string.Empty;
    }
    private void OnEnable()
    {
        CharacterButton.OnClick += GetID;
    }

    private void OnDisable()
    {
        CharacterButton.OnClick -= GetID;
    }

    public void GetID(string id)
    {
        CharacterData characterData = DataTableManager.CharacterTable.Get(id);
        ChangeId(characterData);
    }

    private void ChangeId(CharacterData characterData)
    {
        iconImage.sprite = characterData.SpriteIcon;
        text.Id = characterData.Name;
        desc.Id = characterData.Desc;
        hpLabel.Id = "HP";
        atkLabel.Id = "ATK";
        defLabel.Id = "DEF";
        hp.text = $"{characterData.Health}";
        atk.text = $"{characterData.AttackPower}";
        def.text = $"{characterData.Defense}";
    }
}
