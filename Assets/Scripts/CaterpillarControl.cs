using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CaterpillarControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ConfigurableJoint joint;
    public float movementSpeed = 10;
    public float turningSpeed = 1;
    public static float vertical, horizontal;

    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine){
            return;
        }
        horizontal = Input.GetAxis("Horizontal") * turningSpeed*Time.deltaTime;
        //transform.Rotate(0, horizontal, 0);
        vertical = Input.GetAxis("Vertical")*movementSpeed*Time.deltaTime;
        //transform.Translate(0, 0, vertical);

        if (horizontal != 0){
            float angle;
            Vector3 axis = new Vector3(0f, 1f, 0f);
            if (horizontal > 0){
                angle = -1f;
            }
            else{
                angle = 1f;
            }
            Quaternion quaternionRotation = Quaternion.AngleAxis(angle*turningSpeed*Time.deltaTime, axis);
            joint.targetRotation *= quaternionRotation;
            
        }
    }
}
