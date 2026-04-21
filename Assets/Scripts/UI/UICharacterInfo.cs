using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image imageIcon;
    public Image imageEquip;
    public Image imageWeapon;

    private bool onMenu = false;

    public UIInvenSlotList invenSlotList;

    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDescription;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textHealth;
    public TextMeshProUGUI textAttackPower;
    public TextMeshProUGUI textDefense;

    private SaveCharacterData currentCharacter;

    private void Awake()
    {
        invenSlotList.gameObject.SetActive(false);
    }

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
        currentCharacter = saveCharData;
        CharacterData data = currentCharacter.CharacterData;
        imageIcon.sprite = data.SpriteIcon;

        imageEquip.sprite = currentCharacter.EquipArmor != null ? currentCharacter.EquipArmor.SpriteIcon : null;
        imageWeapon.sprite = currentCharacter.EquipWeapon != null ? currentCharacter.EquipWeapon.SpriteIcon : null;

        textName.text = string.Format(FormatCommon, st.Get("NAME"), data.StringName);
        textDescription.text = string.Format(FormatCommon, st.Get("DESC"), data.StringDesc);

        string typeId = data.Type.ToString().ToUpper();
        textType.text = string.Format(FormatCommon, st.Get("TYPE"), st.Get(typeId));

        textHealth.text = string.Format(FormatCommon, st.Get("HP"), data.Health);
        textAttackPower.text = string.Format(FormatCommon, st.Get("ATK"), data.AttackPower + (currentCharacter.EquipWeapon?.AttackPower ?? 0));
        textDefense.text = string.Format(FormatCommon, st.Get("DEF"), data.Defense + (currentCharacter.EquipArmor?.Defense ?? 0));
    }

    public void OnEquipSlotClick()
    {
        if (!onMenu)
        {
            invenSlotList.gameObject.SetActive(true);
            invenSlotList.targetCharacter = currentCharacter;
            onMenu = true;
        }
        else if (onMenu)
        {
            invenSlotList.gameObject.SetActive(false);
            invenSlotList.targetCharacter = null;
            onMenu = false;
        }
    }
}
