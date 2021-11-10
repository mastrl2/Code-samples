using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    public Vector3 lastPos;
    bool left = false;
    public float gravOffsetX = .5f;
    public float gravOffsetY = .5f;
    public Vector3 gravityVec = new Vector3 (0f, -9.8f, 0f);
    public bool dead = false;

    public float gravityAxisVal;
    public float gravityAxisValSlow;

    public bool triggers = true;

    int invert = 1;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Used with the controller, only changes if the character is alive
        if (triggers){
            gravityAxisVal = Input.GetAxis("Gravity") * invert;
            gravityAxisValSlow = Input.GetAxis("GravitySlow") * invert;
            if(Mathf.Abs(gravityAxisVal) >= Mathf.Abs(gravityAxisValSlow))
            {
                if (!dead && (gravityAxisVal > 0.01f || gravityAxisVal < -0.01f))
                {
                    Debug.Log(gravityAxisVal);
                    transform.Rotate(0, 0, gravityAxisVal);
                }
            }
            else
            {
                //Makes the gravity change slower if you press lightly
                if (!dead && (gravityAxisValSlow > 0.01f || gravityAxisValSlow < -0.01f))
                {
                    Debug.Log(gravityAxisValSlow);
                    transform.Rotate(0, 0, gravityAxisValSlow/2.0f);
                }
            }
        }
        else{
            gravityAxisVal = Input.GetAxis("GravAlt") * invert;
            if (!dead && (gravityAxisVal > 0.01f || gravityAxisVal < -0.01f))
            {
                Debug.Log(gravityAxisVal);
                transform.Rotate(0, 0, gravityAxisVal);
            }
        }
        
        //Used with mouse and keyboard, not recommended
        if (!dead && Input.GetMouseButton(0)){
            Debug.Log(Input.mousePosition);
            if (Input.mousePosition.x == lastPos.x){
                if (left){
                    transform.Rotate(0, 0, -1.5f);
                }
                else{
                    transform.Rotate(0, 0, 1.5f);
                }
            }
            if (Input.mousePosition.x > lastPos.x){
                transform.Rotate(0, 0, 1.5f);
                left = false;
            }
            if (Input.mousePosition.x < lastPos.x){
                transform.Rotate(0, 0, -1.5f);
                left = true;
            }
        }

        if (!dead && Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, 1.0f);
        }
        if (!dead && Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, -1.0f);
        }
        lastPos = Input.mousePosition;
        Physics.gravity = -1f * transform.up.normalized * Physics.gravity.magnitude;
        //Debug.DrawRay(transform.position, Physics.gravity,Color.blue);
    }

    public void Invert()
    {
        invert *= -1;
    }
}
