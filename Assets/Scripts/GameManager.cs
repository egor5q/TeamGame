using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject PlayerPrefab;
    public GameObject cameraPrefab;
    public Vector3 playerpos = new Vector3(0f, 0f, 0f);

    private CameraFollowInGame script;

    private GameObject cameraObject;

    private GameObject spiderObject;

    public float gateMoveSpeed = 60f;

    private bool gameStarted = false;
    private bool needMoveGates = false;

    public GameObject staticCameraPrefab;
    public Vector3 staticCameraPos = new Vector3(329.7f, 118.9f, -77f);
    public Quaternion staticCameraRotation = new Quaternion(40.607f, 0f, 0f, 0f);
    private GameObject gate;
    private PhotonView photonView;


    [PunRPC]
    public void MoveGates(){
        needMoveGates = true;
        
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient){
            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add("GameStarted", gameStarted);
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }

        if (!PhotonNetwork.IsMasterClient){
            Debug.Log(PhotonNetwork.CurrentRoom);
            Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties);
            gameStarted = (bool)PhotonNetwork.CurrentRoom.CustomProperties["GameStarted"];
        }

        gate = GameObject.Find("Gates");
        string gs;
        if (gameStarted){
            gs = "True";
        }
        else{
            gs = "False";
        }

        Debug.Log("GameStarted: "+gs);
        if (!gameStarted){
            string objectName = "Caterpillar "+PhotonNetwork.NickName;
            spiderObject = PhotonNetwork.Instantiate(PlayerPrefab.name, playerpos, Quaternion.identity);
            spiderObject.name = objectName;
            spiderObject.GetComponent<CaterpillarControl>().playerid = photonView.Owner.UserId;
            photonView.Owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable(){
                {"MyCaterpillarName", spiderObject.name}
            });
            Transform secondSegment = spiderObject.transform.GetChild(1).transform.GetChild(0);
            //Quaternion addRotation = new Quaternion(23f, 90f, 0f, 0f);
            cameraObject = Instantiate(cameraPrefab, playerpos, Quaternion.identity);
            script = cameraObject.GetComponent<CameraFollowInGame>();
            script.target = secondSegment;
        }
        else{
            cameraObject = Instantiate(staticCameraPrefab, staticCameraPos, staticCameraRotation);
            gate.transform.position = new Vector3(gate.transform.position.x, -13f, gate.transform.position.z);
            
        }

    }

    public void Leave(){
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {   
        if (PhotonNetwork.IsMasterClient && gameStarted == false){
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
                StartRace();
            }
        }
        if (Input.GetButtonDown("q")){
            Debug.Log(("left"));
            Leave();
            Application.Quit();
        }
    }

    void FixedUpdate(){
        if (needMoveGates){
        if (gate.transform.position.y > -13){
            gate.transform.position -= new Vector3(0f, gateMoveSpeed*Time.deltaTime, 0f);
        }
        else{
            needMoveGates = false;
        }
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }

    public void StartRace(){
        gameStarted = true;
        photonView.RPC("MoveGates", RpcTarget.All);
        //photonView.RPC("SetGameStarted", RpcTarget.Others, true);
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("GameStarted", gameStarted);
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged){
        if (propertiesThatChanged.ContainsKey("GameStarted")){
            gameStarted = (bool)PhotonNetwork.CurrentRoom.CustomProperties["GameStarted"];
        }
    }

    
}
