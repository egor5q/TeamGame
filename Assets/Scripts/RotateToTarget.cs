using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotateToTarget : MonoBehaviour
{

    //[SerializeField] private Transform target;
    public Transform target;
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private Transform caterpillarTransform;

    public GameObject objWithScript;

    public GameObject keySegment;

    private NavMeshAgent agent;
    private Vector3[] pathCorners;
    private int currentCornerIndex;
    private Rigidbody mainspider;

    public GameObject inviz;

    void Start(){
        inviz = objWithScript.GetComponent<AutoNavigation_caterpillar>().inviz;
        agent = inviz.GetComponent<NavMeshAgent>();
        mainspider = transform.root.GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    void Awake(){
        List<GameObject> objectsWithScript = new List<GameObject>();
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent("TargetedBySpider") != null)
            {
                target = obj.transform;
                break;
            }
        }

        //target = GameObject.Find("Sphere").transform;
    }
    void FixedUpdate()

    {
        inviz.transform.position = keySegment.transform.position;
       /* if (target != null){
        Vector3 dirToPlane = caterpillarTransform.position - target.position;
        Vector3 tangent = Vector3.Cross(dirToPlane, Vector3.up);
        Vector3 direction = tangent.normalized*-1;
        //caterpillarTransform.forward =direction;

        //Vector3 ToTarget = target.position - caterpillarTransform.position;
        //Vector3 ToTargetXZ = new Vector3(ToTarget.x, 0f, ToTarget.z);
        //Quaternion rotation = Quaternion.LookRotation(ToTargetXZ);
        //joint.targetRotation = rotation;

        Quaternion rotation = Quaternion.LookRotation(direction);
        joint.targetRotation =Quaternion.Inverse(rotation);
        }
        */
        //agent.updatePosition = true;
        //mainspider.isKinematic = true;
        //mainspider.isKinematic = false;
        if (pathCorners == null){
            currentCornerIndex = objWithScript.GetComponent<AutoNavigation_caterpillar>().currentCornerIndex;
            pathCorners = objWithScript.GetComponent<AutoNavigation_caterpillar>().pathCorners;
        }
        else if (pathCorners.Length == 0){
            currentCornerIndex = objWithScript.GetComponent<AutoNavigation_caterpillar>().currentCornerIndex;
            pathCorners = objWithScript.GetComponent<AutoNavigation_caterpillar>().pathCorners;
        }
        if (pathCorners == null || pathCorners.Length == 0){
            return;
        }
        Vector3 targetpos = pathCorners[currentCornerIndex];
        //Debug.Log(targetpos);
        Vector3 dirToPlane = caterpillarTransform.position - targetpos;
        Vector3 tangent = Vector3.Cross(dirToPlane, Vector3.up);
        Vector3 direction = tangent.normalized*-1;
        Quaternion rotation = Quaternion.LookRotation(direction);
        joint.targetRotation =Quaternion.Inverse(rotation);

        Debug.Log(Vector3.Distance(keySegment.transform.position, targetpos));
        if (Vector3.Distance(keySegment.transform.position, targetpos) < 1.5f){
            currentCornerIndex++;
            if (currentCornerIndex >= pathCorners.Length){
                pathCorners = null;
                objWithScript.GetComponent<AutoNavigation_caterpillar>().pathCorners = null;
                objWithScript.GetComponent<AutoNavigation_caterpillar>().currentCornerIndex = 0;
                //agent.isStopped = false;
                //agent.isStopped = true;
                //agent.updatePosition = true;
                return;
            }
        }
        

        
    }

    
}
