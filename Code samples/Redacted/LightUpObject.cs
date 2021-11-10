using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpObject : MonoBehaviour
{
    //add this to interactable objects to make them light up 
    
    Shader inUseShader;
    Material mat;
    Renderer render;

    public bool isLit;
    public bool currentlyLit;

    Color initialColor;
    Color finalColor;

    private void Awake()
    {
        isLit = false;
        currentlyLit = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        mat = render.material;

        initialColor = mat.color;
        finalColor = Color.green;
        //change color up there^
    }

    // Update is called once per frame
    void Update()
    {
        if (isLit && !currentlyLit)
        {
            LightUp();
        }
        if (!isLit)
        {
            currentlyLit = false;
            LightDown();
        }
    }
    void LightUp()
    {
        //In order to make it emit light, switch what's commented after isLit
        currentlyLit = true;

        mat.color = new Color(finalColor.r,finalColor.g,finalColor.b,.5f);
        if(!transform.GetComponent<MeshRenderer>().enabled)
        {
            transform.GetComponent<MeshRenderer>().enabled = true;
        }
        //mat.EnableKeyword("_EMISSION");
        //mat.SetColor("_EmissionColor", Color.green);
    }
    void LightDown()
    {
        //same deal here, flip what's commented to make it emit light
        mat.color = initialColor;
        transform.GetComponent<MeshRenderer>().enabled = false;
        //mat.DisableKeyword("_EMISSION");
    }
}
