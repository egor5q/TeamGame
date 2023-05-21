using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject PlayerPrefab;
    public GameObject cameraPrefab;
    public Vector3 playerpos = new Vector3(0f, 0f, 0f);

    private CameraFollowInGame script;

    private GameObject cameraObject;

    private GameObject spiderObject;

    void Start()
    {
        spiderObject = PhotonNetwork.Instantiate(PlayerPrefab.name, playerpos, Quaternion.identity);
        Transform secondSegment = spiderObject.transform.GetChild(1).transform.GetChild(0);
        //Quaternion addRotation = new Quaternion(23f, 90f, 0f, 0f);
        cameraObject = Instantiate(cameraPrefab, playerpos, Quaternion.identity);
        script = cameraObject.GetComponent<CameraFollowInGame>();
        script.target = secondSegment;
        

    }

    public void Leave(){
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("q")){
            Debug.Log(("left"));
            Leave();
            Application.Quit();
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
}
