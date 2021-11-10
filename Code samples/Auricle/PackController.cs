using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PackController : MonoBehaviour
{
    public GameObject [] pack;

    MoveTo move;

    Vector3 initialPosition;

    bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        move = gameObject.GetComponent<MoveTo>();
    }

    // Update is called once per frame
    void Update()
    {
        //If it is only the one, run away and disappear
        int en = CheckPackNumber();
        if (en == 1)
        {
            if (first)
            {
                move.SetInital();
                move.pursue = false;
                first = false;
            }
            float dist = Vector3.Distance(transform.position, initialPosition);
            if (dist < 2.5f)
            {

                gameObject.SetActive(false);
            }
        }
    }
    //Check how many pack members are actually active and not defeated
    public int CheckPackNumber()
    {
        int enabled = 0;
        for (int i = 0; i < pack.Length; i++)
        {
            if(pack[i].activeSelf == true)
            {
                enabled++;
            }
        }
        return enabled;
    }
}
