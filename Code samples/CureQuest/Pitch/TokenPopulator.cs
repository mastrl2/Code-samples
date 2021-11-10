using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenPopulator : ScrollPopulator
{
    //Used to populate the scrollview
    public void MakeButtons(List<Token> tokens){
        foreach(Token t in tokens){
            GameObject button = Instantiate(baseButton) as GameObject;
            button.GetComponentInChildren<Text>().text = t.name;
            button.transform.SetParent(contents.transform);
        }
    }
}
