using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    public Text pressEText;
    public Image detectionImage;
    public Sprite controls;
    public Sprite firstDocument;
    public Sprite secondDocument;
    public Sprite thirdDocument;
    public Sprite fourthDocument;
    public Sprite fifthDocument;
    public Sprite sixthDocument;
    public Sprite placeholder;
    public Sprite numPadSprite;
    public Image documentImageSpace;
    public Image numPadImageSpace;
    public Text countDownText;

    public Sprite detectedEye;
    public Sprite transparentPhoto;

    public int docCount;
    //public List<string> collectedDocuments;
    public bool detected = false;
    public bool lookingAtDocuments = false;

    public PlayerCam inUseCamera;
    public PlayerMove playerMovement;
    public SafeCode theSafe;
    public Button[] numPad;

    public int docNum;

    public bool firstFound;
    public bool secondFound;
    public bool thirdFound;
    public bool fourthFound;
    public bool fifthFound;
    public bool sixthFound;
    private bool safeOn;

    // Start is called before the first frame update
    void Start()
    {
        pressEText = GameObject.FindWithTag("Press E").GetComponent<Text>() as Text;
        detectionImage = GameObject.FindWithTag("Detection image").GetComponent<Image>() as Image;
        docCount = 0;
        documentImageSpace = GameObject.FindWithTag("Document space").GetComponent<Image>() as Image;
        documentImageSpace.sprite = transparentPhoto;

        numPadImageSpace.sprite = transparentPhoto;

        docNum = 0;

        lookingAtDocuments = false;
        safeOn = false;

        firstFound = false;
        secondFound = false;
        thirdFound = false;
        fourthFound = false;
        fifthFound = false;
        sixthFound = false;

        numPad[1] = GameObject.Find("one").GetComponent<UnityEngine.UI.Button>();
        numPad[2] = GameObject.Find("two").GetComponent<UnityEngine.UI.Button>();
        numPad[3] = GameObject.Find("three").GetComponent<UnityEngine.UI.Button>();
        numPad[4] = GameObject.Find("four").GetComponent<UnityEngine.UI.Button>();
        numPad[5] = GameObject.Find("five").GetComponent<UnityEngine.UI.Button>();
        numPad[6] = GameObject.Find("six").GetComponent<UnityEngine.UI.Button>();
        numPad[7] = GameObject.Find("seven").GetComponent<UnityEngine.UI.Button>();
        numPad[8] = GameObject.Find("eight").GetComponent<UnityEngine.UI.Button>();
        numPad[9] = GameObject.Find("nine").GetComponent<UnityEngine.UI.Button>();
        numPad[0] = GameObject.Find("zero").GetComponent<UnityEngine.UI.Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && safeOn == true)
        {
            NumPadOff();
        }
        if (Input.GetKeyUp(KeyCode.Q) && safeOn == false)
        {
            Check();
        }

        if (lookingAtDocuments == true)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                if (docNum != 0)
                {
                    docNum -= 1;
                    PageTurn();
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                if (docNum != 6)
                {
                    docNum += 1;
                    PageTurn();
                }
            }
        }

        if (detected == true)
        {
            detectionImage.sprite = detectedEye;
        }
        if (detected == false)
        {
            detectionImage.sprite = transparentPhoto;
        }
    }
    //Interact text
    public void PressE()
    {
        pressEText.text = "Click to Interact";
    }
    //Clears all current text in a box
    public void ClearText()
    {
        pressEText.text = "";
    }
    //Shows you the documents you have collected or turns them off
    void Check()
    {
        if (lookingAtDocuments == false)
        {
            SeeDocuments();
            lookingAtDocuments = true;
        }
        else if (lookingAtDocuments == true)
        {
            ClearDocs();
            lookingAtDocuments = false;
            
        }
    }
    //Stops you to read documents and them up
    void SeeDocuments()
    {
        inUseCamera.allowPlayerControl = false;
        playerMovement.allowPlayerMovement = false;
        documentImageSpace.sprite = controls;
    }
    //Clears documents from the screen
    void ClearDocs()
    {
        documentImageSpace.sprite = transparentPhoto;
        inUseCamera.allowPlayerControl = true;
        playerMovement.allowPlayerMovement = true;
        docNum = 0;
    }
    //Controls for showing the next document in sequence
    void PageTurn()
    {
        if (docNum == 0)
        {
            documentImageSpace.sprite = controls;
        }
        if (docNum == 1)
        {
            if (firstFound == true)
            {
                documentImageSpace.sprite = firstDocument;
            }
            else
            {
                documentImageSpace.sprite = placeholder;
            }
        }
        if (docNum == 2)
        {
            if (secondFound == true)
            {
                documentImageSpace.sprite = secondDocument;
            }
            else
            {
                documentImageSpace.sprite = placeholder;
            }
        }
        if (docNum == 3)
        {
            if (thirdFound == true)
            {
                documentImageSpace.sprite = thirdDocument;
            }
            else
            {
                documentImageSpace.sprite = placeholder;
            }
        }
        if (docNum == 4)
        {
            if (fourthFound == true)
            {
                documentImageSpace.sprite = fourthDocument;
            }
            else
            {
                documentImageSpace.sprite = placeholder;
            }
        }
        if (docNum == 5)
        {
            if (fifthFound == true)
            {
                documentImageSpace.sprite = fifthDocument;
            }
            else
            {
                documentImageSpace.sprite = placeholder;
            }
        }
        if (docNum == 6)
        {
            if (sixthFound == true)
            {
                documentImageSpace.sprite = sixthDocument;
            }
            else
            {
                documentImageSpace.sprite = placeholder;
            }
        }
    }
    //Used for switching on the numpad for entering the code into the safe
    void NumPadOn()
    {
        inUseCamera.allowPlayerControl = false;
        playerMovement.allowPlayerMovement = false;
        //inUseCamera.SendMessage("ShowCursor");
        numPadImageSpace.sprite = numPadSprite;
        for (int i = 0; i < 10; i++)
        {
            numPad[i].interactable = true;
            numPad[i].gameObject.SetActive(true);
        }
        safeOn = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void NumPadOff()
    {
        //inUseCamera.SendMessage("LockCursor");
        inUseCamera.allowPlayerControl = true;
        playerMovement.allowPlayerMovement = true;
        numPadImageSpace.sprite = transparentPhoto;
        for (int i = 0; i < 10; i++)
        {
            numPad[i].interactable = false;
            numPad[i].GetComponent<Image>().sprite = transparentPhoto;
        }
        safeOn = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //For each number button on the safe
    void OnePressed()
    {
        //Debug.Log("one");
        theSafe.SendMessage("Pressed", 1);
    }
    void TwoPressed()
    {
        //Debug.Log("two");
        theSafe.SendMessage("Pressed", 2);
    }
    void ThreePressed()
    {
        //Debug.Log("three");
        theSafe.SendMessage("Pressed", 3);
    }
    void FourPressed()
    {
        //Debug.Log("four");
        theSafe.SendMessage("Pressed", 4);
    }
    void FivePressed()
    {
        //Debug.Log("five");
        theSafe.SendMessage("Pressed", 5);
    }
    void SixPressed()
    {
        //Debug.Log("six");
        theSafe.SendMessage("Pressed", 6);
    }
    void SevenPressed()
    {
        //Debug.Log("seven");
        theSafe.SendMessage("Pressed", 7);
    }
    void EightPressed()
    {
        //Debug.Log("eight");
        theSafe.SendMessage("Pressed", 8);
    }
    void NinePressed()
    {
        //Debug.Log("nine");
        theSafe.SendMessage("Pressed", 9);
    }
    void ZeroPressed()
    {
        int i = 0;
        //Debug.Log("zero");
        theSafe.SendMessage("Pressed", i);
    }
    void CountDown(int num)
    {
        countDownText.text = num.ToString();
    }
}
