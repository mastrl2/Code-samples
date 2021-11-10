using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPopulator : MonoBehaviour
{
    //Button base for new button instantiation
    public GameObject baseButton;
    //The contents where it should be placed
    public GameObject contents;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //For dynamically creating buttons from a list
    public void MakeButtons(List<InventoryItem> items)
    {
        foreach (InventoryItem item in items)
        {
            GameObject button = Instantiate(baseButton) as GameObject;
            //button.SetActive(false);
            button.SetActive(true);
            button.GetComponentInChildren<Text>().text = item.name;
            //button.GetComponent<InventoryItem>() = item;
            button.GetComponent < StandardButton>().item = item; 
            button.GetComponent<Image>().sprite = item.sprite;
            button.transform.SetParent(contents.transform);
        }

    }

    //Clears all buttons
    public void ClearButtons()
    {
        foreach (Transform currentButton in contents.transform)
        {
            Destroy(currentButton.gameObject);
        }
    }
}
