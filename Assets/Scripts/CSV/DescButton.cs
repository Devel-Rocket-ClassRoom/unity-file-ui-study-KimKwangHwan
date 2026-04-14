using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescButton : MonoBehaviour
{
    //private ItemData itemData;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI desc;
    //[SerializeField] private Localization textName;
    //[SerializeField] private Localization descName;
    public void SetEmpty()
    {
        iconImage.sprite = null;
        text.text = string.Empty;
        desc.text = string.Empty;
    }
    private void OnEnable()
    {
        ItemButton.OnButton += GetID;
    }

    private void OnDisable()
    {
        ItemButton.OnButton -= GetID;
    }

    //public void GetItemData(ItemData itemData)
    //{
    //    this.itemData = itemData;
    //    ChangeButton();
    //}

    public void GetID(string id)
    {
        ItemData itemData = DataTableManager.ItemTable.Get(id);
        ChangeItem(itemData);
    }

    private void ChangeItem(ItemData itemData)
    {
        iconImage.sprite = itemData.SpriteIcon;
        text.text = itemData.StringName;
        desc.text = itemData.StringDesc;
    }
}
