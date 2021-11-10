using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeClasses : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//Base class that holds recipe information
[System.Serializable]
public class RecipeInformation
{
    //Whether or not you know the recipe
    [SerializeField]
    bool known = false;
    //What the ingredients are
    [SerializeField]
    List<string> ingredients;
    //What the final product is and what image to display for it
    //[SerializeField]
    //string finalProduct;
    [SerializeField]
    InventoryItem resultingItem;

    //Get and set for known
    public void makeKnown()
    {
        known = true;
    }
    public bool isKnown()
    {
        return known;
    }
    //Shows the resulting item
    public InventoryItem Result()
    {
        return resultingItem;
    }
}

//For easy editing in the inspector, helps hold all the information
[System.Serializable]
public class DictionaryOfRecipes
{
    //The specific information for each recipe
    [SerializeField]
    List<RecipeInformation> information;
    //The possible combinations of items
    [SerializeField]
    List<string> recipes;

    //Gets the index of a specific combination
    public int GetKeyIndex(string item)
    {
        //Debug.Log(combinations.IndexOf(item) + " " + item);
        return recipes.IndexOf(item);
    }

    //Tells you if you know the recipe already
    public bool IsRecipeKnown(string recipe)
    {
        int index = GetKeyIndex(recipe);
        //Debug.Log(recipe + " " + index);
        return information[index].isKnown();
    }

    //Only used when result is made, adds the recipe to known recipes
    public InventoryItem AddRecipeToKnown(string key)
    {
        int index = GetKeyIndex(key);
        information[index].makeKnown();
        return information[index].Result();
    }

    //Returns the resulting item
    public InventoryItem GetResultingItem(string key)
    {
        int index = GetKeyIndex(key);
        return information[index].Result();
    }
}
