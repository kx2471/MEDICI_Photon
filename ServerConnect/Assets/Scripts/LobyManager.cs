using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//네트워크에 대한 기능을 사용하겠음
using Photon.Pun;//포톤 펀 영역에 있는 클래스, 메서드, 자료형 등을 사용하겠음
using Photon.Realtime; //포톤의 코어기능을 기반으로 만들겠음


//접속 상태에 따라서 상태를 체크 할 수 있는 상태변수를 준비하자 - enum타입으로 상태를 정의함
enum NetworkState
{
    none,
    connect,
    disconnect,
    makeroom,
    inroom,
    ingame
}





public class LobyManager : MonoBehaviourPunCallbacks //monobehaviour, photonnetwork를 다 쓰고싶다
{

    //PhotonNetwork
    string gamever = "0.1";
    public Button Login;//접속
    public Button Logout;//접속해지
    public Button Join;//방생성 혹은 참여(방이 있을 때)

    public Text info_txt;//인포메이션 텍스트

    //일반 변수들
    NetworkState netState = NetworkState.none;


    // Start is called before the first frame update
    void Start()
    {
        //버튼이 처음부터 다 활성화 되는것이 아닌 접속만 활성화 시킨다.
        Login.interactable = true;
        Logout.interactable = false;
        Join.interactable = false;

        PhotonNetwork.GameVersion = gamever;
        //포톤서버에 접속 -> 세팅한 정보 기반
    }

    public void Connect_Server()
    {
        info_txt.text = "마스터 서버에 접속중";
        PhotonNetwork.ConnectUsingSettings();
        //접속 여부에 따라 다양한 함수가 호출된다(콜백함수)

        Login.interactable = false;
        Logout.interactable = Join.interactable = true;
    }

    public void DisConnect_Server()
    {
        //접속해지 함수를 찾아서 쓰면 된다
        PhotonNetwork.Disconnect();
        //버튼이 초기화가 이루어 져야 한다(logout, join은 비활성화가 되어야 한다)

        Login.interactable = true;
        Logout.interactable = Join.interactable = false;
        netState = NetworkState.disconnect;

    }

    public void Connect_Room()
    {
        switch (netState)
        {
            case NetworkState.connect:
                info_txt.text = "방 생성 혹은 참여 중";
                //방생성
                //PhotonNetwork.CreateRoom()
                //PhotonNetwork.JoinRoom(); 방이름 몰라서 패스
                PhotonNetwork.JoinRandomRoom(); //방에 조인되면 참여이벤트가 발생
                break;
        }
            
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        info_txt.text = "방이 없습니다 \n 새로 방을 만들겠습니다";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 8 });//방만들기
    }

    public override void OnJoinedRoom()
    {
        info_txt.text = "방이 만들어졌습니다" + PhotonNetwork.CurrentRoom.Name;

        print(info_txt.text + PhotonNetwork.CurrentRoom.Name+" "
            +PhotonNetwork.CurrentRoom.PlayerCount);

        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnConnectedToMaster()//서버 접속시 호출되는 함수의 재정의
    {//접속 성공시 처리
        netState = NetworkState.connect;
        info_txt.text = "접속 성공";
        Login.interactable = false;
        Logout.interactable = Join.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        switch (netState)
        {
            case NetworkState.none:
                info_txt.text = "접속 실패 오프라인입니다 \n 재접속합니다";
                PhotonNetwork.ConnectUsingSettings();//재접속시도
                break;
            case NetworkState.disconnect:
                info_txt.text = "접속해지 상황입니다";
                break;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
