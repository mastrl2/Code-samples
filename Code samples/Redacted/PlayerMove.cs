using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    private CharacterController charControl;
    private float playerY;
    private Vector3 stay;

    public bool allowPlayerMovement;

    private void Awake()
    {
        //Unity's character controller used on this character
        charControl = GetComponent<CharacterController>();
        allowPlayerMovement = true;
        stay = new Vector3(0, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (allowPlayerMovement == true)
        {
            Move();
            playerY = this.transform.position.y;
            //Debug.log
        }
        else
        {
            charControl.SimpleMove(stay);
        }
        
        
    }
    void Move()
    {
        //Moves the character based upon player input
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;
        float horiInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        
        Vector3 forwardMove = transform.forward * vertInput;
        Vector3 rightMove = transform.right * horiInput;

        charControl.SimpleMove(forwardMove + rightMove);
        
       
    }

    void OnTriggerEnter(Collider col)
    {
        //print("detected");
        if(col.gameObject.transform.tag == "CameraBox")
        {
            //print("detected");
            SceneManager.LoadScene("Alpha", LoadSceneMode.Single);
        }
    }

    
}
