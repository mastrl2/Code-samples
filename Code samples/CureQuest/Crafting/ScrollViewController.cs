using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ScrollViewController : MonoBehaviour
{
    //Current player's inventory
    public List<InventoryItem> inventory; 
    //public Inventory inventory;

    public Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();

    Recipe r;

    //Standard and filtered lists of items for crafting
    public GameObject standardInventory;
    ScrollPopulator standardPopulator;

    public GameObject filteredInventory;
    ScrollPopulator filteredPopulator;

    //Button for creating the shown item
    public Button createItem;

    //Value for which filter is selected
    int filterValue = 0;

    //Whichever item in the standard list is selected
    InventoryItem standardSelected;

    InventoryItem filteredSelected;

    public InventoryItem empty;

    //The base buttons that show what is currently selected
    public Button filtered;
    public Button standard;

    public Sprite noResult;
    public Sprite question;

    public PlayerProgress prog;

    // Start is called before the first frame update
    void Start()
    {
        //Set up playerprogress to use the inventory and add to known recipes if needed
        inventory = prog.inventory;

        //Sets up the recipe book
        CreateRecipeBook();

        //Find needed populators, set up base scrollbars
        standardPopulator = standardInventory.GetComponent<ScrollPopulator>();
        filteredPopulator = filteredInventory.GetComponent<ScrollPopulator>();

        //Sets up the regular inventory
        MakeRegularInventory(filteredPopulator);
        MakeRegularInventory(standardPopulator);

        //Sets up currently selected items
        SetStandard(empty);
        SelectFiltered(empty);

        //Misc button setup
        createItem.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Creates dictionary of all recipes from assest list
    public void CreateRecipeBook(){
        //Finds the folder containing the recipes and adds all their names to a list
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/ScriptableObjects/CraftingRecipes" });
        foreach (string recipeName in assetNames)
        {
            //For all the recipes in the list, it finds the path and the recipe
            var recipePath = AssetDatabase.GUIDToAssetPath(recipeName);
            var r = AssetDatabase.LoadAssetAtPath<Recipe>(recipePath);
            //Then adds it to the dictionary
            recipes.Add(r.key, r);
        }
    }

    //Creates the object that is the combination of the two selected items
    public void CreateObject()
    {
        //Makes the result and adds it to the inventory, as well as making sure it is added to known
        string key = FindKey(filteredSelected.name);
        recipes[key].known = true;
        inventory.Add(recipes[key].result); 

        //Removes previously selected items and resets all needed values
        inventory.Remove(filteredSelected);
        inventory.Remove(standardSelected);
        //inventory.RemoveItem(filteredSelected);
        //inventory.RemoveItem(standardSelected);

        SelectFiltered(empty);
        SetStandard(empty);
        ChangeValues();

        standardPopulator.ClearButtons();
        MakeRegularInventory(standardPopulator);
    }

    //Resets filtered list based upon what filter has been chosen
    public void ChangeValues()
    {
        //int menuValue = filters.value;
        //Debug.Log(menuValue);
        if (filterValue == 0)
        {
            ClearFiltered();
            MakeRegularInventory(filteredPopulator);
            //Debug.Log("Base position");
        }
        if (filterValue == 1)
        {
            ClearFiltered();
            AllRecipes();
            //Debug.Log("Position 1");
        }
        if (filterValue == 2)
        {
            ClearFiltered();
            OnlyKnown();
            //Debug.Log("Position 1");
        }
        if (filterValue == 3)
        {
            ClearFiltered();
            OnlyUnknown();
            //Debug.Log("Position 1");
        }

    }

    //Filter button functions, enabled by clicking buttons in the scene
    public void AllItemsEnabled(){
        filterValue = 0;
        ChangeValues();
    }

    public void AllPossibleRecipesEnabled(){
        filterValue = 1;
        ChangeValues();
    }

    public void OnlyKnownRecipesEnabled(){
        filterValue = 2;
        ChangeValues();
    }

    public void OnlyUnknownRecipesEnabled(){
        filterValue = 3;
        ChangeValues();
    }
    //Set scrollbar to whole inventory
    void MakeRegularInventory(ScrollPopulator pop)
    {
        //pop.ClearButtons();
        pop.MakeButtons(inventory);
    }

    //Use the only known recipes filter for only recipes that have the outcome known
    public void OnlyKnown()
    {
        //Creates a list for new buttons
        List<InventoryItem> knownRecipes = new List<InventoryItem>();
        //Checks which ones are needed for scrollbar
        foreach (InventoryItem item in inventory)
        {
            string currentKey = FindKey(item.name);
            if (recipes.TryGetValue(currentKey, out r) && recipes[currentKey].known == true)
            {
                knownRecipes.Add(item);
            }
        }
        //Clears and resets filtered list for new filter
        ClearFiltered();
        filteredPopulator.MakeButtons(knownRecipes);
    }

    //Opposite of known recipes filter
    public void OnlyUnknown()
    {
        filteredPopulator.ClearButtons();
        List<InventoryItem> unknownRecipes = new List<InventoryItem>();
        foreach (InventoryItem item in inventory)
        {
            string currentKey = FindKey(item.name);
            if (recipes.TryGetValue(currentKey, out r) && recipes[currentKey].known == false)
            {
                unknownRecipes.Add(item);
            }
        }
        ClearFiltered();
        filteredPopulator.MakeButtons(unknownRecipes);
    }

    //Show all possible recipes
    public void AllRecipes()
    {
        //filteredPopulator.ClearButtons();
        List<InventoryItem> possibleRecipes = new List<InventoryItem>();
        foreach (InventoryItem item in inventory)
        {
            string currentKey = FindKey(item.name);
            //Debug.Log(currentKey);
            if (recipes.TryGetValue(currentKey, out r))
            {
                //Debug.Log(recipes.GetKeyIndex(currentKey));
                possibleRecipes.Add(item);
            }
        }
        ClearFiltered();
        filteredPopulator.MakeButtons(possibleRecipes);
    }

    //Shows whole inventory
    public void WholeInventory()
    {
        MakeRegularInventory(filteredPopulator);
    }

    //Finds the key for the entry in the dictionary
    //Constructed in ALPHABETICAL ORDER
    public string FindKey(string item)
    {
        if (string.Compare(item, standardSelected.itemName) < 0)
        {
            return item + "X" + standardSelected.name;
        }
        else
        {
            return standardSelected.name + "X" + item;
        }
    }

    //Clears buttons from the filtered scrollbar
    public void ClearFiltered()
    {
        filteredPopulator.ClearButtons();
    }

    //Sets up selected item from standard inventory list
    public void SetStandard(InventoryItem n)
    {
        //Sets it to button at the bottom as to easily see what is selected
        standardSelected = n;
        standard.GetComponentInChildren<Text>().text = n.name;
        standard.GetComponent<Image>().sprite = n.sprite;
        ChangeValues();

        if (filteredSelected != null)
        {
            ResultingImage();
        }
    }

    //Sets up selected item from filtered inventory list
    public void SelectFiltered(InventoryItem n)
    {
        //Same thing here
        filteredSelected = n;
        filtered.GetComponentInChildren<Text>().text = n.name;
        filtered.GetComponent<Image>().sprite = n.sprite;

        ResultingImage();
    }

    //Shows the image that the combination would result in
    public void ResultingImage()
    {
        //Debug.Log(filteredSelected.name);
        string key = FindKey(filteredSelected.name);
        if (recipes.TryGetValue(key, out r) && !CheckSameObject(filteredSelected))
        {
            createItem.interactable = true;
            if (recipes[key].known == true)
            {
                InventoryItem res = recipes[key].result;
                //Makes the image given
                createItem.GetComponentInChildren<Text>().text = res.itemName;
                createItem.GetComponent<Image>().sprite = res.sprite;
            }
            else
            {
                //Makes a ?
                createItem.GetComponentInChildren<Text>().text = "???";
                createItem.GetComponent<Image>().sprite = question;
            }
        }
        else
        {
            //Makes an x for the center and deactivates it
            createItem.GetComponentInChildren<Text>().text = "None";
            createItem.GetComponent<Image>().sprite = noResult;
            createItem.interactable = false;
        }
    }

    //Resets all buttons and scrollers 
    public void ResetMenu(){
        //Resets scrollers to full inventory and no filters
        MakeRegularInventory(standardPopulator);
        AllItemsEnabled();

        //Resets bottom buttons and selected items
        SetStandard(empty);
        SelectFiltered(empty);

        //Resets center button
        ResultingImage();
    }

    bool CheckSameObject(InventoryItem item){
        if (standardSelected == filteredSelected){
            int first = inventory.IndexOf(item);
            int last = inventory.LastIndexOf(item);
            if (first != last){
                return false;
            }
            return true;
        }
        return false;
    }
}
