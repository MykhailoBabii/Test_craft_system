using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastCraft : MonoBehaviour
{
    [SerializeField] private Button _craftButton;

    [SerializeField] private Transform _resultSlot;

    [SerializeField] private InventaryConfig _inventaryConfig;
    [SerializeField] private RecipesConfig _recipesConfig;
    [SerializeField] private CraftSystem _craftSystem;
    [SerializeField] private InventorySystem _inventorySystem;

    private bool canCraft;


    // Start is called before the first frame update
    void Start()
    {
        _craftButton.onClick.AddListener(Crafting);
    }


    private void Crafting()
    {
       foreach (var inventartyItem in _inventaryConfig.ItemStructList)
       {
            for (int i = 0; i < _recipesConfig.IngredientsList.Count; i++)
            {
                if (inventartyItem.ItemType == _recipesConfig.IngredientsList[i].ItemType)
                {
                    if (inventartyItem.Number > 0)
                    {
                        canCraft = true;
                    }

                    else
                    {
                        canCraft = false;
                    }
                }
            }
       }

        if(canCraft == true)
        {
            foreach (var item in _inventaryConfig.ItemStructList)
            {
                for (int i = 0; i < _recipesConfig.IngredientsList.Count; i++)
                {
                    if (item.ItemType == _recipesConfig.IngredientsList[i].ItemType)
                    {
                        item.RemoveNumber();
                        Item.OnReturnItemAction?.Invoke();
                        _inventorySystem.UpdateInventary();
                    }
                }
            }

            foreach (var item in _recipesConfig.IngredientsList)
            {
                _inventaryConfig.SetCurrentItimType(item.ItemType);
                _craftSystem.AddToCraftList();
            }
        }
        
    }
}
