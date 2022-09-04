using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventorySystem : MonoBehaviour
{
    [SerializeField] private CraftSystem _craftSystem;
    [SerializeField] private InventaryConfig _inventaryConfig;
    [SerializeField] private Transform _path;

    public InventaryConfig inventaryConfig => _inventaryConfig;



    private void OnEnable()
    {
        Item.OnUpdateItemAction += UpdateInventary;
    }


    private void OnDisable()
    {
        Item.OnUpdateItemAction -= UpdateInventary;
    }


    private void Awake()
    {
        UpdateInventary();
    }


    public void UpdateInventary()
    {
        foreach (var item in _inventaryConfig.ItemStructList)
        {
            if (item.Number > 0)
            {
                var itemObject = Instantiate(Resources.Load<GameObject>($"Items/{Enum.GetName(typeof(ItemType), item.ItemType)}"), _path);
                itemObject.TryGetComponent(out Item newItem);
                newItem.GetNumber(item.Number);

                newItem.PlugCraftSystem(_craftSystem);
                newItem.PlugInventorySystem(this);
            }
        }
    }


    
}
