using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchScrollPopulator : ScrollPopulator
{
    //Used to find out which scrollview the note belongs to
    public int side;

    //Populates the list with buttons
    public void MakeButtons(List <Notes> collectedNotes)
    {
        foreach (Notes note in collectedNotes)
        {
            GameObject button = Instantiate(baseButton) as GameObject;
            //button.SetActive(true);
            button.GetComponentInChildren<Text>().text = note.flavor;
            button.GetComponent<NoteButton>().note = note;
            //button.GetComponent<StandardButton>().item = note;
            button.GetComponent<NoteButton>().side = side;
            button.transform.SetParent(contents.transform);
        }
    }

    //To add singular buttons to the list
    public void AddButton(Notes n){
        GameObject button = Instantiate(baseButton) as GameObject;
        button.GetComponentInChildren<Text>().text = n.text;
        NoteButton nb = button.GetComponent<NoteButton>();
        nb.note = n;
        nb.multiplier = null;
        if (side == 1){
            nb.tokenButton.GetComponentInChildren<Text>().text = "No token selected";
        }
        button.transform.SetParent(contents.transform);
    }
}
