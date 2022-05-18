using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//��Ʈ��ũ�� ���� ����� ����ϰ���
using Photon.Pun;//���� �� ������ �ִ� Ŭ����, �޼���, �ڷ��� ���� ����ϰ���
using Photon.Realtime; //������ �ھ����� ������� �������


//���� ���¿� ���� ���¸� üũ �� �� �ִ� ���º����� �غ����� - enumŸ������ ���¸� ������
enum NetworkState
{
    none,
    connect,
    disconnect,
    makeroom,
    inroom,
    ingame
}





public class LobyManager : MonoBehaviourPunCallbacks //monobehaviour, photonnetwork�� �� ����ʹ�
{

    //PhotonNetwork
    string gamever = "0.1";
    public Button Login;//����
    public Button Logout;//��������
    public Button Join;//����� Ȥ�� ����(���� ���� ��)

    public Text info_txt;//�������̼� �ؽ�Ʈ

    //�Ϲ� ������
    NetworkState netState = NetworkState.none;


    // Start is called before the first frame update
    void Start()
    {
        //��ư�� ó������ �� Ȱ��ȭ �Ǵ°��� �ƴ� ���Ӹ� Ȱ��ȭ ��Ų��.
        Login.interactable = true;
        Logout.interactable = false;
        Join.interactable = false;

        PhotonNetwork.GameVersion = gamever;
        //���漭���� ���� -> ������ ���� ���
    }

    public void Connect_Server()
    {
        info_txt.text = "������ ������ ������";
        PhotonNetwork.ConnectUsingSettings();
        //���� ���ο� ���� �پ��� �Լ��� ȣ��ȴ�(�ݹ��Լ�)

        Login.interactable = false;
        Logout.interactable = Join.interactable = true;
    }

    public void DisConnect_Server()
    {
        //�������� �Լ��� ã�Ƽ� ���� �ȴ�
        PhotonNetwork.Disconnect();
        //��ư�� �ʱ�ȭ�� �̷�� ���� �Ѵ�(logout, join�� ��Ȱ��ȭ�� �Ǿ�� �Ѵ�)

        Login.interactable = true;
        Logout.interactable = Join.interactable = false;
        netState = NetworkState.disconnect;

    }

    public void Connect_Room()
    {
        switch (netState)
        {
            case NetworkState.connect:
                info_txt.text = "�� ���� Ȥ�� ���� ��";
                //�����
                //PhotonNetwork.CreateRoom()
                //PhotonNetwork.JoinRoom(); ���̸� ���� �н�
                PhotonNetwork.JoinRandomRoom(); //�濡 ���εǸ� �����̺�Ʈ�� �߻�
                break;
        }
            
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        info_txt.text = "���� �����ϴ� \n ���� ���� ����ڽ��ϴ�";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 8 });//�游���
    }

    public override void OnJoinedRoom()
    {
        info_txt.text = "���� ����������ϴ�" + PhotonNetwork.CurrentRoom.Name;

        print(info_txt.text + PhotonNetwork.CurrentRoom.Name+" "
            +PhotonNetwork.CurrentRoom.PlayerCount);

        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnConnectedToMaster()//���� ���ӽ� ȣ��Ǵ� �Լ��� ������
    {//���� ������ ó��
        netState = NetworkState.connect;
        info_txt.text = "���� ����";
        Login.interactable = false;
        Logout.interactable = Join.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        switch (netState)
        {
            case NetworkState.none:
                info_txt.text = "���� ���� ���������Դϴ� \n �������մϴ�";
                PhotonNetwork.ConnectUsingSettings();//�����ӽõ�
                break;
            case NetworkState.disconnect:
                info_txt.text = "�������� ��Ȳ�Դϴ�";
                break;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
