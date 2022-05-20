using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NewBehaviourScript : MonoBehaviourPunCallbacks
{
    void Start()
    {
        string gameVer = "0.1";
        PhotonNetwork.GameVersion = gameVer;
        // 자동 동기화 한다.
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();                                   // 접속시도
    }
    // 접속 성공 시 방을 만든다
    public override void OnConnectedToMaster(){                                 // 서버 접속시 호출되는 함수의 재정의
        PhotonNetwork.JoinRandomRoom();                                         // 방 접속 시도 없으면 에러
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });     // 방 만들기
    }
    // 방 생성 성공했을 때
    public override void OnJoinedRoom()                                         // 1번씩 호출 
    {
        print("방 접속 완료");
        // 플레이어를 만드는 단계 시전
        Vector2 pos = Random.insideUnitCircle * 3f;
        PhotonNetwork.Instantiate("Player", new Vector3(pos.x, 5.0f, pos.y), Quaternion.identity);
    }


}
