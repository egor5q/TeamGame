using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CaterpillarControl_fullphysics : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ConfigurableJoint joint;

    public float movementSpeed = 10;
    public float turningSpeed = 1;
    public static float vertical, horizontal;

    public GameObject rotationControl;
    public GameObject secondSegment;

    private PhotonView photonView;
    public string playerid;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame

    [PunRPC]
        void setRotation(Quaternion targetrotation){
            rotationControl.transform.rotation = targetrotation;
        }
        
    void Update()
    {
        if (!photonView.IsMine){
            return;
        }
        Vector3 newpos = new Vector3(secondSegment.transform.position.x, secondSegment.transform.position.y+0.5f, secondSegment.transform.position.z);
        rotationControl.transform.position = newpos;

        horizontal = Input.GetAxis("Horizontal") * turningSpeed*Time.deltaTime;
        //transform.Rotate(0, horizontal, 0);
        vertical = Input.GetAxis("Vertical")*movementSpeed*Time.deltaTime;
        //transform.Translate(0, 0, vertical);

        if (horizontal != 0){
            float angle;
            Vector3 axis = new Vector3(0f, 1f, 0f);
            if (horizontal > 0){
                angle = 1f;
            }
            else{
                angle = -1f;
            }
            Quaternion quaternionRotation = Quaternion.AngleAxis(angle*turningSpeed*Time.deltaTime, axis);
            //joint.targetRotation *= quaternionRotation;
            rotationControl.transform.rotation *= quaternionRotation;
            //joint.targetRotation = rotationControl.transform.rotation;
            photonView.RPC("setRotation", RpcTarget.Others, rotationControl.transform.rotation);
            
        }
    
    }
}
