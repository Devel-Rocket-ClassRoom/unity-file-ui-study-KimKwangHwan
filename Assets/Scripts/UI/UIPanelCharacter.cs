using NUnit.Framework;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UICharacterSlotList;

public class UIPanelCharacter : MonoBehaviour
{
    //public TMP_Dropdown sorting;
    //public TMP_Dropdown filtering;
    // 옵션이 저장되도록

    public UICharacterSlotList uiCharSlotList;

    private void OnEnable()
    {
        //uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
        //OnLoad();

        //sorting.options.Clear();
        //filtering.options.Clear();

        //string[] sortingOptions =
        //{
        //    DataTableManager.StringTable.Get("DATE_ASSCENDING"),
        //    DataTableManager.StringTable.Get("DATE_DESCENDING"),
        //    DataTableManager.StringTable.Get("NAME_ASSCENDING"),
        //    DataTableManager.StringTable.Get("NAME_DESCENDING"),
        //    DataTableManager.StringTable.Get("COST_ASSCENDING"),
        //    DataTableManager.StringTable.Get("COST_DESCENDING"),
        //    DataTableManager.StringTable.Get("VALUE_ASSCENDING"),
        //    DataTableManager.StringTable.Get("VALUE_DESCENDING"),
        //};

        //string[] filteringOptions =
        //{
        //    DataTableManager.StringTable.Get("NONE"),
        //    DataTableManager.StringTable.Get("WEAPON"),
        //    DataTableManager.StringTable.Get("EQUIP"),
        //    DataTableManager.StringTable.Get("CONSUMABLE"),
        //    DataTableManager.StringTable.Get("NONCONSUMABLE"),
        //};

        //sorting.AddOptions(sortingOptions.ToList());
        //filtering.AddOptions(filteringOptions.ToList());
    }

    //private void Start()
    //{
    //    sorting.value = (int)uiInvenSlotList.Sorting;
    //    filtering.value = (int)uiInvenSlotList.Filtering;
    //}

    //public void OnChangeSorting(int index)
    //{
    //    uiCharSlotList.Sorting = (UIInvenSlotList.SortingOptions)index;
    //}

    //public void OnChangeFiltering(int index)
    //{
    //    uiCharSlotList.Filtering = (UIInvenSlotList.FilteringOptions)index;
    //}

    //public void OnSave()
    //{
    //    SaveLoadManager.Data. = uiCharSlotList.GetSaveCharDataList();
    //    SaveLoadManager.Save();
    //}
    //public void OnLoad()
    //{
    //    SaveLoadManager.Load();
    //    uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    //}
    //public void OnAddItem()
    //{
    //    uiInvenSlotList.AddRandomItem();
    //}
    //public void OnRemoveItem()
    //{
    //    uiInvenSlotList.RemoveItem();
    //}
}
