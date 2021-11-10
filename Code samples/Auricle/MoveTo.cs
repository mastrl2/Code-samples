using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class MoveTo : MonoBehaviour
{
    public Transform player = null;
    NavMeshAgent agent;
    public float stoppingDistance;
    public Vector3 initialPos;
    public float furthest = 15f;
    public bool pursue = false;
    public bool stay = true;


    //audio, which I did not handle
    public AudioSource footstepSource;
    public AudioClip[] footstepSounds;
    private bool playing = false;
    private float footTimer = 0;
    private int randomRet = 0;
    public float footstepTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Get the navmesh, which is what they use to move and set distances
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
        stoppingDistance = GetComponentInChildren<CloseTransition>().minDistance;
        agent.stoppingDistance = stoppingDistance;

        initialPos = transform.position;
        if(player == null) player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //furthest = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pursue){
            agent.destination = player.position;
            stay = true;
            //print(agent.destination);
        }
        else if (stay){
            agent.destination = initialPos;
            stay = false;
            //print("second line " + agent.destination);
        }
        if(agent.velocity.magnitude > 0.1)
        {
            if (!playing)
            {
                footTimer = 0;

                if (footstepSounds.Length > 0)
                {
                    randomRet = Random.Range(0, footstepSounds.Length);
                    footstepSource.clip = footstepSounds[randomRet];
                    footstepSource.Play();
                    playing = true;
                }
            }
        }
        if (playing)
        {
            footTimer += Time.deltaTime;
            if (footTimer > footstepTime)
            {
                playing = false;
            }
        }
        //agent.destination = player.position;

    }
    //Check if the player is in range
    public bool IsInRange()
    {
        if (Mathf.Abs((player.transform.position - gameObject.transform.position).magnitude) <= stoppingDistance)
        {
            //Debug.Log((player.transform.position - gameObject.transform.position).magnitude);

            return true;
        }
        return false;
    }
    //If it's moved too far
    public void TooFar(){
        if (Vector3.Distance(initialPos, transform.position) > furthest){
            agent.destination = initialPos;
        }
    }
    public void SetInital()
    {
        agent.destination = initialPos;
    }
}
