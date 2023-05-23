using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CaterpillarControl : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] private ConfigurableJoint joint;
    public float movementSpeed = 10;
    public float turningSpeed = 1;
    public static float vertical, horizontal;

    public string playerid;

    private bool needTakeRotation = false;
    private int secondsCount;

    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame

    [PunRPC]
        void setRotation(Quaternion targetrotation){
            joint.targetRotation = targetrotation;
        }

    /*void FixedUpdate(){
        if (needTakeRotation){
            secondsCount += 1;
            if (secondsCount >= 3){
                secondsCount = 0;
                needTakeRotation = false;
                Player[] players = PhotonNetwork.PlayerList;
                foreach(Player player in players){
                    string caterpillarName = (string)player.CustomProperties["MyCaterpillarName"];
                    Debug.Log(caterpillarName);
                    if (caterpillarName != null){
                        GameObject caterpillar = GameObject.Find(caterpillarName);
                        CaterpillarControl script = caterpillar.GetComponent<CaterpillarControl>();
                        Debug.Log(script.joint);
                        caterpillar.GetPhotonView().RPC("setRotation", RpcTarget.Others, script.joint.targetRotation);
                    }
                }
            }
        }
    }*/
        
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
            //photonView.RPC("setRotation", RpcTarget.Others, joint.targetRotation);

        }
        photonView.RPC("setRotation", RpcTarget.Others, joint.targetRotation);
    
    }

    public override void OnJoinedRoom()
    {   
        needTakeRotation = true;
    }

}
