using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//��Ʈ��ũ�� ���� ����� ����ϰ���
using Photon.Pun;//���� �� ������ �ִ� Ŭ����, �޼���, �ڷ��� ���� ����ϰ���
using Photon.Realtime; //������ �ھ����� ������� �������

public class LobyManager : MonoBehaviourPunCallbacks //monibihaviour, photonnetwork�� �� ����ʹ�
{

    //PhotonNetwork
    string gamever = "0.1";

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = gamever;
        //���漭���� ���� -> ������ ���� ���
        PhotonNetwork.ConnectUsingSettings();
    }

   void OnConnectedToMaster()
    {
        print("���� ����");
    }

    void OnDisconnected()
    {
        print("���� ����");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
