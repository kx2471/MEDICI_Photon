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
        //Instantiate(); //네트워크가 아닌 일반 씬에서 만드는 객체
        PhotonNetwork.Instantiate("Player", new Vector3(randomPos.x, 0, randomPos.y),
            Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //네트워크 기준으로 프리팹 객체를 만든다.
        
    }
}
