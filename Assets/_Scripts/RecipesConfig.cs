using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class RecipeStruct
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _number;
    
    public ItemType ItemType => _itemType;
    public int Number => _number;
    
}

[CreateAssetMenu(fileName = "NewRecipeConfig", menuName = "Tool/RecipeConfig")]
public class RecipesConfig : ScriptableObject
{
    [SerializeField] private List<RecipeStruct> _ingredientsList;
    [SerializeField] private List<RecipeStruct> _resultItems;

    public List<RecipeStruct> IngredientsList => _ingredientsList;
    public List<RecipeStruct> resultItems => _resultItems;
}


