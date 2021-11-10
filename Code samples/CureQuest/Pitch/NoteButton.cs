using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteButton : MonoBehaviour
{
    //Contained note
    public Notes note;

    //The controller needed for the pitch system
    public PitchController controller;

    //To check which scrollview the note belongs to
    public int side;

    //The attached token if there is one
    public Token multiplier;

    public Button tokenButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //On click function
    public void SwitchClick(){
        //If a token is selected it attaches the token, otherwise switches to the other scrollview
        if (controller.currentToken == null){
            controller.SwapNotes(note, side);
            if (side == 1){
                DeselectToken();
            }
            Destroy(gameObject);
            Debug.Log(controller.chosenNotes.contents.transform.childCount);
            controller.UpdateGraph();
        }
        else{
            if (side == 1){
                multiplier = controller.currentToken;
                multiplier.used = true;
                if (multiplier.attached != null){
                    multiplier.attached.DeselectToken();
                }
                multiplier.attached = gameObject.GetComponent<NoteButton>();
                controller.currentToken = null;
                tokenButton.GetComponentInChildren<Text>().text = multiplier.name;
                //controller.UpdateGraph();
            }
        }
    }

    public void DeselectToken(){
        if (multiplier != null){
            multiplier.used = false;
            multiplier.attached = null;
            tokenButton.GetComponentInChildren<Text>().text = "No token attached";
            multiplier = null;
            controller.UpdateGraph();
        }
    }
}
