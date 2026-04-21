using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image imageIcon;
    public Image imageEquip;
    public Image imageWeapon;

    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDescription;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textHealth;
    public TextMeshProUGUI textAttackPower;
    public TextMeshProUGUI textDefense;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDescription.text = string.Empty;
        textType.text = string.Empty;
        textHealth.text = string.Empty;
        textAttackPower.text = string.Empty;
        textDefense.text = string.Empty;
    }

    public void SetSaveCharacterData(SaveCharacterData saveCharData)
    {
        var st = DataTableManager.StringTable;
        CharacterData data = saveCharData.CharacterData;

        imageIcon.sprite = data.SpriteIcon;

        textName.text = string.Format(FormatCommon, st.Get("NAME"), data.StringName);
        textDescription.text = string.Format(FormatCommon, st.Get("DESC"), data.StringDesc);

        string typeId = data.Type.ToString().ToUpper();
        textType.text = string.Format(FormatCommon, st.Get("TYPE"), st.Get(typeId));

        textHealth.text = string.Format(FormatCommon, st.Get("HP"), data.Health);
        textAttackPower.text = string.Format(FormatCommon, st.Get("ATK"), data.AttackPower);
        textDefense.text = string.Format(FormatCommon, st.Get("DEF"), data.Defense);
    }
}
