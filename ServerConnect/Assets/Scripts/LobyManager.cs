using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//네트워크에 대한 기능을 사용하겠음
using Photon.Pun;//포톤 펀 영역에 있는 클래스, 메서드, 자료형 등을 사용하겠음
using Photon.Realtime; //포톤의 코어기능을 기반으로 만들겠음

public class LobyManager : MonoBehaviourPunCallbacks //monibihaviour, photonnetwork를 다 쓰고싶다
{

    //PhotonNetwork
    string gamever = "0.1";

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = gamever;
        //포톤서버에 접속 -> 세팅한 정보 기반
        PhotonNetwork.ConnectUsingSettings();
    }

   void OnConnectedToMaster()
    {
        print("접속 성공");
    }

    void OnDisconnected()
    {
        print("접속 실패");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
