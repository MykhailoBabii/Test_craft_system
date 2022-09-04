using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemStruct
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _number;

    public ItemType ItemType => _itemType;
    public int Number => _number;

    public void AddNumber()
    {
        _number++;
    }

    public void RemoveNumber()
    {
        _number--;
    }
}


[CreateAssetMenu(fileName = "InventaryConfig", menuName = "Tool/InventaryConfig")]
public class InventaryConfig : ScriptableObject
{
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private ItemType _currentItimeType;
    [SerializeField] private List<ItemStruct> _itemStructList;

    public InventorySystem inventorySystem => _inventorySystem;
    public ItemType currentItimeType => _currentItimeType;
    public List<ItemStruct> ItemStructList => _itemStructList;


    public void AddNumberItemToList()
    {
        foreach (var item in _itemStructList)
        {
            if (item.ItemType == _currentItimeType)
            {
                item.AddNumber();
            }
        }
    }


    public void RemoveNumberItemFromList()
    {
        foreach (var item in _itemStructList)
        {
            if (item.ItemType == _currentItimeType)
            {
                item.RemoveNumber();
            }
        }
    }


    public void SetCurrentItimType(ItemType itemType)
    {
        _currentItimeType = itemType;
    }


    
}
