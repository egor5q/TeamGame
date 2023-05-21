using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private bool roomCreated = false;
    void Start()
    {
        PhotonNetwork.NickName = "Player "+Random.Range(1, 1000);
        Log("Player's nickname set to "+PhotonNetwork.NickName);
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRoom(){
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions {MaxPlayers = 2});
        Log("Created room");
    }

    public void JoinRoom(){
        PhotonNetwork.JoinRandomRoom();
        //PhotonNetwork.JoinRandomRoom();
    }

    public void JoinOrCreate(){
        PhotonNetwork.JoinRandomOrCreateRoom();
        Log("Joined or created");
        //PhotonNetwork.JoinRandomRoom();
    }


    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        //SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        PhotonNetwork.LoadLevel("Game");
    }
    public override void OnConnectedToMaster()
    {
        JoinOrCreate();

 
    }

    private void Log(string message){
        Debug.Log(message);
    }
}
