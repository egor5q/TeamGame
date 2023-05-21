using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoNavigation_caterpillar : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;

    public Vector3[] pathCorners;
    public int currentCornerIndex;

    public GameObject keySegment;
    public GameObject inviz;

    void Start()
    {

        mainCamera = Camera.main;
        agent = inviz.GetComponent<NavMeshAgent>();
        //inviz = GameObject.Find("InvisibleAgent");
        //Debug.Log(inviz);
    }


    void Update()
    {
    
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit)){
                //agent.Warp(keySegment.transform.position);
                //agent.updatePosition = false;
                //agent.SetDestination(keySegment.transform.position);
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(hit.point, path);
                pathCorners = path.corners;
                currentCornerIndex = 0;
            }
        }
    }
}
