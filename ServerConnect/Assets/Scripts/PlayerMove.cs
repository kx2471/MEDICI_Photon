using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMove : MonoBehaviourPunCallbacks
{
    //내 객체에 연결되어 있는 객체를 제어하기 위한 변수
    Rigidbody rb;//내 물리객체
    PlayerInput pi;//내 이동관련 입력객체
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        pi = this.GetComponent<PlayerInput>();
        anim = this.GetComponentInChildren<Animator>();

        if (photonView.IsMine == true)
        {
            Camera.main.GetComponent<SmoothFollow>().target = this.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        MoveChar();
        RotChar();
        
    }
    void MoveChar()
    {
        //Vector 3 Distance = 키입력방향 * 내방향 * 속도 * Time.deltaTime;
        Vector3 Distance = pi.move * this.transform.forward * 5f * Time.deltaTime;
        
        rb.MovePosition(rb.position + Distance);
        //rb.MovePosition(rb.position + distance);//물리력이 이쓴 이동 현 위치를 기준으로 이동함
        //this.transform.Translate(Vector3.zero); //현위치를 기준으로 이동을 시킨다.
        //물리력이 적용안됨 -> 충돌일때만 물리력이 생김
        //의도치않은 많은 이동 회전값이 발생 될 수 있다.
    }
    void RotChar()
    {
        float turn = pi.rotate * 120 * Time.deltaTime;
        rb.rotation = rb.rotation * Quaternion.Euler(0,turn,0);
    }
}
