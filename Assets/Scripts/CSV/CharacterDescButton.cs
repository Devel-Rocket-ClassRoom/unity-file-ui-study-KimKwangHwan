using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterDescButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private TextMeshProUGUI hpLabel;
    [SerializeField] private TextMeshProUGUI atkLabel;
    [SerializeField] private TextMeshProUGUI defLabel;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI atk;
    [SerializeField] private TextMeshProUGUI def;

    public void SetEmpty()
    {
        iconImage.sprite = null;
        text.text = string.Empty;
        desc.text = string.Empty;
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
        //text.text = characterData.StringName;
        text.GetComponent<Localization>().id = characterData.Name;
        // desc.text = characterData.StringDesc;
        desc.GetComponent<Localization>().id = characterData.Desc;
        hpLabel.GetComponent<Localization>().id = "HP";
        atkLabel.GetComponent<Localization>().id = "ATK";
        defLabel.GetComponent<Localization>().id = "DEF";
        hp.text = $"{characterData.Health}";
        atk.text = $"{characterData.AttackPower}";
        def.text = $"{characterData.Defense}";
    }
}
