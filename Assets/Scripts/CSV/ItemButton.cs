using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string id;

    public static event Action<string> OnButton;

    private void OnEnable()
    {
        ChangeItem();
    }
    private void OnValidate()
    {
        ChangeItem();
    }

    private void ChangeItem()
    {
        ItemData itemData = DataTableManager.ItemTable.Get(id);
        if (itemData != null)
        {
            iconImage.sprite = itemData.SpriteIcon;
            text.GetComponent<Localization>().id = itemData.Name;
        }
    }

    public void OnButtonClicked()
    {
        OnButton?.Invoke(id);
    }
}
