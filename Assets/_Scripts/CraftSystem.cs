using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public static Action OnReternItemAction;
    public static Action OnCraftTableIsFullAction;

    public Transform _craftTableSlot;
    public Transform _resultSlot;

    [SerializeField] private int _craftTableSize;
    [SerializeField] private List<Item> _craftList;
    [SerializeField] private List<RecipesConfig> _recipeConfigs;
    [SerializeField] private InventaryConfig _inventaryConfig;
    [SerializeField] private InventorySystem _inventorySystem;
    

    // Start is called before the first frame update

    private void OnEnable()
    {
        Item.OnUseItemAction += AddToCraftList;
    }


    private void OnDisable()
    {
        Item.OnUseItemAction -= AddToCraftList;
    }

    void Start()
    {
        _recipeConfigs = new List<RecipesConfig>(Resources.LoadAll<RecipesConfig>("Recipes"));
    }

    public void AddToCraftList()
    {
        var itemObject = Instantiate(Resources.Load<GameObject>($"Items/{Enum.GetName(typeof(ItemType), _inventaryConfig.currentItimeType)}"), _craftTableSlot);
        itemObject.TryGetComponent(out Item item);

        

        _craftList.Add(item);

        item.AddNumber();
        item._onCraftTable = true;
        item.PlugCraftSystem(this);
        item.PlugInventorySystem(_inventorySystem);

        CheckRecipe();

        if (_craftList.Count >= _craftTableSize)
            OnCraftTableIsFullAction?.Invoke();
    }

    public void CheckRecipe()
    {
        foreach (var recipe in _recipeConfigs)
        {
            if (_craftList.Count == recipe.IngredientsList.Count)
            {
                int coincidence = 0;
                for (int i = 0; i < recipe.IngredientsList.Count; i++)
                {
                    if (_craftList[i].ItemType == recipe.IngredientsList[i].ItemType)
                        coincidence++;

                    if (coincidence == recipe.IngredientsList.Count)
                    {
                        foreach (var craftItem in _craftList)
                        {
                            craftItem._onCraftTable = false;
                            Destroy(craftItem.gameObject);
                        }

                        _inventaryConfig.SetCurrentItimType(recipe.resultItems[0].ItemType);
                        var itemObject = Instantiate(Resources.Load<GameObject>($"Items/{Enum.GetName(typeof(ItemType), _inventaryConfig.currentItimeType)}"), _resultSlot);

                        itemObject.TryGetComponent(out Item resultItem);

                        _craftList.Add(resultItem);

                        resultItem.AddNumber();
                        resultItem._onCraftTable = true;
                        resultItem.PlugCraftSystem(this);
                        resultItem.PlugInventorySystem(_inventorySystem);

                        _craftList.Clear();
                    }
                }
            }
        }
    }


    public void RemoveFromCraftList(Item item)
    {
        _craftList.Remove(item);
    }
}
