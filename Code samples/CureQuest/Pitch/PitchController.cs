using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PitchController : MonoBehaviour
{
    //All collected notes by the player
    public List<Notes> notes;

    //The populators for the note and token scrollbars
    public PitchScrollPopulator allNotes;
    public PitchScrollPopulator chosenNotes;

    //For the amount of notes in chosen and whether the graph has been updated
    public int chosenCount = 0;
    bool updated = true;

    //Populator for the token scroll view
    public TokenPopulator tokenPopulator;

    //The static height of the bar
    float barHeight;

    //The bars for the graph
    public Image socialBar;
    public Image mgmntBar;

    //Currently selected token
    public Token currentToken = null;

    //Player's collected tokens
    public List<Token> tokens;

    //Target numbers for the pitch
    public float socialTarget;
    public float mgmntTarget; 

    //Current values for each according to the pitch
    float currentSocial = 0f;
    float currentMgmnt = 0f;

    //Are you sure screen
    public GameObject sureScreen;

    //Button for making the pitch
    public Button makePitchButton;

    //Player progress to get notes
    public PlayerProgress prog;

    // Start is called before the first frame update
    void Start()
    {
        //Sets notes to the player's notes
        notes = prog.notes;

        //Sets the bar height and makes the base graph
        barHeight = 45f;
        //chosenNotes.MakeButtons(notes);
        ChangeGraph(0f, 0f);

        allNotes.MakeButtons(notes);
        tokenPopulator.MakeButtons(tokens);
        sureScreen.SetActive(false);

        int temp = chosenNotes.contents.transform.childCount;
        Debug.Log(temp);
    }

    // Update is called once per frame
    void Update()
    {
        if (updated == false && chosenNotes.contents.transform.childCount == chosenCount){
            UpdateGraph();
            updated = true;
        }
    }

    //Changes graph based upon scores
    public void ChangeGraph(float s, float m)
    {
        socialBar.rectTransform.sizeDelta = new Vector2(s, barHeight);
        mgmntBar.rectTransform.sizeDelta = new Vector2(m, barHeight);
    }

    //Swaps the notes from the original notes bar to the chosen and vice versa
    public void SwapNotes(Notes n, int side){
        //Checks which bar the note clicked came from
        //Then moves it accordingly to the other one
        updated = false;
        if (side == 0){
            chosenNotes.AddButton(n);
            chosenCount += 1;
        }
        else{
            allNotes.AddButton(n);
            chosenCount -= 1;
        }
        //BeginGraphUpdate();
        //UpdateGraph();
    }

    //Updates the graph and does the calculations for the current size of the graph
    public void UpdateGraph(){
        float s = 0.0f;
        float m = 0.0f;
        int temp = chosenNotes.contents.transform.childCount;
        //Debug.Log(temp);
        if (chosenCount == 0){
            currentSocial = 0;
            currentMgmnt = 0;
        }
        else{
            //Goes through all the chosen notes and calculates the scores for both
            foreach (Transform b in chosenNotes.contents.transform){
                GameObject g = b.gameObject;
                Notes n = g.GetComponent<NoteButton>().note;
                Token mult = g.GetComponent<NoteButton>().multiplier;
                float sMult = 1f;
                float mMult = 1f;
                if (mult != null){
                    sMult = mult.socialMult;
                    mMult = mult.mgmntMult;
                }
                //Debug.Log(n.social);
                s += (n.social * sMult);
                m += (n.mgmnt * mMult);
            }
        }
        currentSocial = s;
        currentMgmnt = m;
        //Debug.Log(chosenNotes.contents.transform.childCount);
        ChangeGraph(s, m);
    }

    //Swaps which token is selected, and unselects one if tapped twice
    public void SelectToken(Token t){
        if (currentToken == t){
            currentToken = null;
        }
        else{
            currentToken = t;
        }
    }
    
    //Pull up the are you sure screen
    public void AreYouSure(){
        sureScreen.SetActive(true);
        makePitchButton.interactable = false;
    }

    //Go back to the make a pitch screen if you aren't positive you want to make the pitch
    public void NotSure(){
        sureScreen.SetActive(false);
        makePitchButton.interactable = true;
    }


    //Ending the pitch, if you've hit the targets it is a success
    public void EndPitch(){
        makePitchButton.interactable = true;
        if (mgmntTarget < currentMgmnt && socialTarget < currentSocial){
            EndPitchSuccess();
        }
        else{
            EndPitchFailure();
        }

    }

    //Begins the pitch
    [YarnCommand("begin_pitch")]
    public void BeginPitch(){
        FlipActive();
        sureScreen.SetActive(false);
    }

    //If the pitch is a success
    [YarnCommand("end_pitch_success")]
    public void EndPitchSuccess(){
        FlipActive();
    }

    //If the pitch is a failure
    [YarnCommand("end_pitch_failure")]
    public void EndPitchFailure(){
        FlipActive();
    }

    //Flips what parts of the canvas are active, allowing you to pull this up and disable the other buttons in the scene
    public void FlipActive(){
        foreach (Transform currentItem in gameObject.transform)
        {
            GameObject g = currentItem.gameObject;
            if (g.activeInHierarchy == false){
                g.SetActive(true);
            }
            else{
                g.SetActive(false);
            }
        }
    }

    //Updates the notes, not sure if this is needed but just in case 
    public void UpdateNotes(){
        notes = prog.notes;
    }
}
