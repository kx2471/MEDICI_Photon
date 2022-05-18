using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPos = Random.insideUnitCircle * 3f;
        //Instantiate(); //��Ʈ��ũ�� �ƴ� �Ϲ� ������ ����� ��ü
        PhotonNetwork.Instantiate("Player", new Vector3(randomPos.x, 0, randomPos.y),
            Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //��Ʈ��ũ �������� ������ ��ü�� �����.
        
    }
}
