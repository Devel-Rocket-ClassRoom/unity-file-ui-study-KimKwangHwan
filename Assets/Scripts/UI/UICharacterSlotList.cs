using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class UICharacterSlotList : MonoBehaviour
{
    public UICharacterSlot prefab;
    public ScrollRect scrollRect;

    private List<UICharacterSlot> uiSlotList = new List<UICharacterSlot>();

    private List<SaveCharacterData> saveCharDataList = new List<SaveCharacterData>();

    public GameObject charInfo;
    private bool onMenu = false;

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlot;
    public UnityEvent<SaveCharacterData, int, int> onSelectSlot;

    private void OnSelectSlot(SaveCharacterData saveCharData, int oldIndex, int newIndex)
    {
        if (oldIndex != newIndex || !onMenu)
        {
            charInfo.GetComponent<UICharacterInfo>().SetSaveCharacterData(saveCharData);
            charInfo.SetActive(true);
            onMenu = true;
        }
        else if (oldIndex == newIndex)
        {
            charInfo.SetActive(false);
            onMenu = false;
        }
    }

    private void Awake()
    {
        onSelectSlot.AddListener(OnSelectSlot);
        charInfo.SetActive(false);
    }


    private void OnDisable()
    {
        saveCharDataList = null;
    }

    public void SetSaveCharDataList(List<SaveCharacterData> source)
    {
        saveCharDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharacterData> GetSaveCharDataList()
    {
        return saveCharDataList;
    }

    private void UpdateSlots()
    {
        var list = saveCharDataList.ToList();
        
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
                    onSelectSlot.Invoke(newSlot.SaveCharacterData, selectedSlotIndex, newSlot.slotIndex);
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
    }

    public void AddRandomItem()
    {
        saveCharDataList.Add(SaveCharacterData.GetRandomCharacter());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveCharDataList.Remove(uiSlotList[selectedSlotIndex].SaveCharacterData);
        UpdateSlots();
    }
}