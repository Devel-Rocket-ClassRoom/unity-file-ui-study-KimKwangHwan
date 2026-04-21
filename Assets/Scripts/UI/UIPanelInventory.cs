using NUnit.Framework;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UIInvenSlotList;

public class UIPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    // 옵션이 저장되도록

    public UIInvenSlotList uiInvenSlotList;

    private void OnEnable()
    {
        OnLoad();
    }

    private void Start()
    {
        sorting.value = (int)uiInvenSlotList.Sorting;
        filtering.value = (int)uiInvenSlotList.Filtering;
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UIInvenSlotList.InvenSortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UIInvenSlotList.InvenFilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Save();
    }
    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }
    public void OnAddItem()
    {
        uiInvenSlotList.AddRandomItem();
    }
    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }
}
