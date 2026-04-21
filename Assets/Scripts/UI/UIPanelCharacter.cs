using NUnit.Framework;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UICharacterSlotList;

public class UIPanelCharacter : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    // 옵션이 저장되도록

    public UICharacterSlotList uiCharSlotList;

    private void OnEnable()
    {
        OnLoad();
    }

    private void Start()
    {
        sorting.value = (int)uiCharSlotList.Sorting;
        filtering.value = (int)uiCharSlotList.Filtering;
    }

    public void OnChangeSorting(int index)
    {
        uiCharSlotList.Sorting = (UICharacterSlotList.CharSortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiCharSlotList.Filtering = (UICharacterSlotList.CharFilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.CharList = uiCharSlotList.GetSaveCharDataList();
        SaveLoadManager.Save();
    }
    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiCharSlotList.SetSaveCharDataList(SaveLoadManager.Data.CharList);
    }
    public void OnAddItem()
    {
        uiCharSlotList.AddRandomItem();
    }
    public void OnRemoveItem()
    {
        uiCharSlotList.RemoveItem();
    }
}
