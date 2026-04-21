using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class UIInvenSlotList : MonoBehaviour
{
    public enum InvenSortingOptions // 가격, 코스트 추가
    {
        CreationTimeAsscending,
        CreationTimeDescending,
        NameAccending,
        NameDescending,
        CostAsscending,
        CostDescending,
        ValueAsscending,
        ValueDescending,
    }

    public enum InvenFilteringOptions // NonConsumable 추가
    {
        None,
        Weapon,
        Equip,
        Consumable,
        NonConsumable
    }

    public enum InvenMode
    {
        Default,
        Equip,
    }

    public InvenMode currentMode = InvenMode.Default;
    public UICharacterInfo characterInfo;
    public SaveCharacterData targetCharacter;

    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime), // 오름차순
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime), // 내림차순
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        (lhs, rhs) => lhs.ItemData.Cost.CompareTo(rhs.ItemData.Cost),
        (lhs, rhs) => rhs.ItemData.Cost.CompareTo(lhs.ItemData.Cost),
        (lhs, rhs) => lhs.ItemData.Value.CompareTo(rhs.ItemData.Value),
        (lhs, rhs) => rhs.ItemData.Value.CompareTo(lhs.ItemData.Value),
    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable,
        (x) => x.ItemData.Type != ItemTypes.Consumable,
    };

    public UIInvenSlot prefab;
    public UIInvenSlot emptySlotPrefab;
    private UIInvenSlot unequipSlot;

    public ScrollRect scrollRect;

    private List<UIInvenSlot> uiSlotList = new List<UIInvenSlot>();
    //public int uiSlotMaxCount = 50;

    private List<SaveItemData> saveItemDataList = new List<SaveItemData>();

    private InvenSortingOptions sorting = InvenSortingOptions.CreationTimeAsscending;
    private InvenFilteringOptions filtering = InvenFilteringOptions.None;

    public GameObject itemInfo;
    private bool onMenu = false;

    public InvenSortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                SaveLoadManager.Data.invenSortingOption = sorting;
                UpdateSlots();
            }
            
        }
    }

    public InvenFilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                SaveLoadManager.Data.invenFilteringOption = filtering;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlot;
    public UnityEvent<SaveItemData, int, int> onSelectSlot;

    private void OnSelectSlot(SaveItemData saveItemData, int oldIndex, int newIndex)
    {
        if (currentMode == InvenMode.Default)
        {
            if (oldIndex != newIndex || !onMenu)
            {
                itemInfo.GetComponent<UIItemInfo>().SetSaveItemData(saveItemData);
                itemInfo.SetActive(true);
                onMenu = true;
            }
            else if (oldIndex == newIndex)
            {
                itemInfo.SetActive(false);
                onMenu = false;
            }
        }
        else if (currentMode == InvenMode.Equip)
        {
            if (saveItemData.ItemData.Type == ItemTypes.Equip)
            {
                targetCharacter.EquipArmor = saveItemData.ItemData;
                characterInfo.SetSaveCharacterData(targetCharacter);
            }
            else if (saveItemData.ItemData.Type == ItemTypes.Weapon)
            {
                targetCharacter.EquipWeapon = saveItemData.ItemData;
                characterInfo.SetSaveCharacterData(targetCharacter);
            }
        }
    }

    private void Awake()
    {
        onSelectSlot.AddListener(OnSelectSlot);
        itemInfo.SetActive(false);
        Sorting = SaveLoadManager.Data.invenSortingOption;
        Filtering = SaveLoadManager.Data.invenFilteringOption;

        unequipSlot = Instantiate(emptySlotPrefab, scrollRect.content);
        unequipSlot.SetEmpty();
        unequipSlot.gameObject.SetActive(false);
        unequipSlot.button.onClick.AddListener(OnUnequipSlotClick);
    }

    private void OnEnable()
    {
        // TEST
        SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }

    private void OnDisable()
    {
        //SaveLoadManager.Data.ItemList = saveItemDataList;
        //SaveLoadManager.Save();
        saveItemDataList = null;
    }

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }

    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    onSelectSlot.Invoke(newSlot.SaveItemData, selectedSlotIndex, newSlot.slotIndex);
                    selectedSlotIndex = newSlot.slotIndex;
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlot.Invoke();

        unequipSlot.transform.SetAsLastSibling();
        unequipSlot.gameObject.SetActive(currentMode == InvenMode.Equip);
    }

    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].SaveItemData);
        UpdateSlots();
    }

    private void OnUnequipSlotClick()
    {
        if (targetCharacter == null) return;

        targetCharacter.EquipArmor = null;
        targetCharacter.EquipWeapon = null;

        characterInfo.SetSaveCharacterData(targetCharacter);
    }
}
