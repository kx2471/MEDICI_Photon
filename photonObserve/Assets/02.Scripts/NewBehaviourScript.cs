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
        // �ڵ� ����ȭ �Ѵ�.
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();                                   // ���ӽõ�
    }
    // ���� ���� �� ���� �����
    public override void OnConnectedToMaster(){                                 // ���� ���ӽ� ȣ��Ǵ� �Լ��� ������
        PhotonNetwork.JoinRandomRoom();                                         // �� ���� �õ� ������ ����
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });     // �� �����
    }
    // �� ���� �������� ��
    public override void OnJoinedRoom()                                         // 1���� ȣ�� 
    {
        print("�� ���� �Ϸ�");
        // �÷��̾ ����� �ܰ� ����
        Vector2 pos = Random.insideUnitCircle * 3f;
        PhotonNetwork.Instantiate("Player", new Vector3(pos.x, 5.0f, pos.y), Quaternion.identity);
    }


}
