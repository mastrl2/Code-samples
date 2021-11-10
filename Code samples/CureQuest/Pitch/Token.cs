using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Pitch Token")]
public class Token : ScriptableObject
{
    //The multipliers for the two bars
    public int socialMult;
    public int mgmntMult;

    //The name
    public string name;
    //If it is in use
    public bool used = false;

    //What button it is attached to
    public NoteButton attached;
}
