using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecipeCard recipeCardPrefab;
    [SerializeField] private Transform recipeContainer;
    [SerializeField] private GameObject cratfMaterialsPanel;

    [Header("Recipe Info")]
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Image item1Icon;
    [SerializeField] private TextMeshProUGUI item1Name;
    [SerializeField] private TextMeshProUGUI item1Amount;
    [SerializeField] private Image item2Icon;
    [SerializeField] private TextMeshProUGUI item2Name;
    [SerializeField] private TextMeshProUGUI item2Amount;
    [SerializeField] private Button cratfButton;

    [Header("Final Item")]
    [SerializeField] private Image finalItemIcon;
    [SerializeField] private TextMeshProUGUI finalItemName;
    [SerializeField] private TextMeshProUGUI finalItemDescription;

    [Header("Recipes")]
    [SerializeField] private RecipeList recipes; // SO

    public Recipe RecipeSeleted { get; private set; }

    private void Start()
    {
        LoadRecipes();
    }

    private void LoadRecipes()
    {
        for (int i = 0; i < recipes.Recipes.Length; i++)
        {
            RecipeCard card = Instantiate(recipeCardPrefab, recipeContainer);
            card.InitRecipeCard(recipes.Recipes[i]);
        }
    }

    public void CraftItem()
    {
        for (int i=0; i<RecipeSeleted.Item1Amount; i++)
        {
            Inventory.Instance.ConsumeItem(RecipeSeleted.Item1.ID);
        }

        for (int i = 0; i < RecipeSeleted.Item2Amount; i++)
        {
            Inventory.Instance.ConsumeItem(RecipeSeleted.Item2.ID);
        }

        Inventory.Instance.AddItem(RecipeSeleted.FinalItem, RecipeSeleted.FinalItemAmount);
        ShowRecipe(RecipeSeleted);
    }

    public void ShowRecipe(Recipe recipe)
    {
        if (cratfMaterialsPanel.activeSelf == false)
        {
            cratfMaterialsPanel.SetActive(true);
        }

        RecipeSeleted = recipe;

        recipeName.text = recipe.Name;

        item1Icon.sprite = recipe.Item1.Icon;
        item1Name.text = recipe.Item1.Name;
        item2Icon.sprite = recipe.Item2.Icon;
        item2Name.text = recipe.Item2.Name;

        item1Amount.text = $"{recipe.Item1Amount}/{Inventory.Instance.GetItemCurrentStock(recipe.Item1.ID)}";
        item2Amount.text = $"{recipe.Item2Amount}/{Inventory.Instance.GetItemCurrentStock(recipe.Item2.ID)}";

        finalItemIcon.sprite = recipe.FinalItem.Icon;
        finalItemName.text = recipe.FinalItem.Name;
        finalItemDescription.text = recipe.FinalItem.Description;

        cratfButton.interactable = CanCraftItem(recipe);
    }

    public bool CanCraftItem(Recipe recipe)
    {
        int item1Stock = Inventory.Instance.GetItemCurrentStock(recipe.Item1.ID);
        int item2Stock = Inventory.Instance.GetItemCurrentStock(recipe.Item2.ID);

        if(item1Stock >= recipe.Item1Amount && item2Stock >= recipe.Item2Amount)
        {
            return true;
        }

        return false;
    }
}
