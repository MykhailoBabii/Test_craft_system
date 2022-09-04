using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType
{
    C,
    A,
    T,
    D,
    O,
    G,
    R,
    
    Cat,
    Dog,
    Rat,

}

public class Item : MonoBehaviour
{
    public static Action OnUseItemAction;
    public static Action OnReturnItemAction;
    public static Action OnUpdateItemAction;

    [SerializeField] private Text _numberText;
    [SerializeField] private Button _click;
    [SerializeField] private Button _sellItemButton;

    [SerializeField] private CraftSystem _craftSystem;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private InventaryConfig _inventaryConfig;
    [SerializeField] private RecipesConfig _receptConfig;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _number;
    
    public bool _onCraftTable;
    public InventaryConfig InventaryConfig => _inventaryConfig;
    public RecipesConfig ReceptConfig => _receptConfig;
    public ItemType ItemType => _itemType;
    public int Number => _number;


    private void OnEnable()
    {
        OnReturnItemAction += ClearItems;
        CraftSystem.OnCraftTableIsFullAction += OffItem;
    }


    private void OnDisable()
    {
        OnReturnItemAction -= ClearItems;
        CraftSystem.OnCraftTableIsFullAction -= OffItem;

        if (_onCraftTable == true) //click in craft table
        {
            _inventaryConfig.SetCurrentItimType(_itemType);
            _inventaryConfig.AddNumberItemToList();
        }
    }


    void Start()
    {
        _click.onClick.AddListener(UseItem);

        if(_sellItemButton != null)
            _sellItemButton.onClick.AddListener(Sell);
    }


    public void PlugInventorySystem(InventorySystem inventorySystem)
    {
        _inventorySystem = inventorySystem;
    }


    public void PlugCraftSystem(CraftSystem craftSystem)
    {
        _craftSystem = craftSystem;
    }


    public void UseItem()
    {
        _number--;
        _numberText.text = $"{_number}";
        _inventaryConfig.SetCurrentItimType(_itemType);
        

        if (_onCraftTable == false) //click in inventory
        {
            _inventaryConfig.RemoveNumberItemFromList();
            OnUseItemAction?.Invoke();
            
        }
        
        else if (_onCraftTable == true) //click in craft table
        {
            Destroy(gameObject);
            _craftSystem.RemoveFromCraftList(this);
            
            OnReturnItemAction?.Invoke();
            _inventorySystem.UpdateInventary();
        }

        if (_number < 1) Destroy(gameObject);
    }


    private void Sell()
    {
        _number--;
        _inventaryConfig.SetCurrentItimType(_itemType);
        _inventaryConfig.RemoveNumberItemFromList();
        
        foreach (var item in _receptConfig.IngredientsList)
        {
            _inventaryConfig.SetCurrentItimType(item.ItemType);
            _inventaryConfig.AddNumberItemToList();
            OnReturnItemAction?.Invoke();
            OnUpdateItemAction?.Invoke();
        }
        if (_number < 1) Destroy(gameObject);

        Debug.Log("sell");
    }


    private void OffItem()
    {
        if (_onCraftTable == false)
            _click.interactable = false;
    }


    public void AddNumber()
    {
        _number++;
        GetNumber(_number);
    }


    private void ClearItems() //clear all items in inventary
    {
        if (_onCraftTable == false)
            Destroy(gameObject);
    }


    public void GetNumber(int number)
    {
        _number = number;
        _numberText.text = $"{_number}";
    }
}
