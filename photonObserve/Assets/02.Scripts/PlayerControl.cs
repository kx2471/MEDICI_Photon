using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed = 5.0f;
    public float rotSpeed = 120.0f;

    Vector3 getPos;                                                                 // 네트워크 상에 이동 데이터를 얻음(Remote 객체)
    Quaternion getRot;                                                              // 네트워크 상에 회전 데이터를 얻음(Remote 객체)

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dirRate = new Vector2(Input.GetAxisRaw("Horizontal"),
                                      Input.GetAxisRaw("Vertical"));
        Moving(dirRate);
        Rotating(dirRate);
    }

    private void Moving(Vector2 dir)
    {
        if (photonView.IsMine == true)
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * dir.y);
        }
        else
        {// this.getPos
            this.transform.position = Vector3.Lerp(this.transform.position,
                                                   this.getPos,
                                                   Time.deltaTime * 20f);
        }
    }
    private void Rotating(Vector2 dir)
    {
        if (photonView.IsMine == true)
        {
            this.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * dir.x);
        }
        else                                                                        // 내 객체이지만 네트워크에서는 Mine이 아닌 객체
        {// this.getRot
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation,
                                                      this.getRot,
                                                      Time.deltaTime * 20f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            // 이동
            stream.SendNext(this.transform.position);                               // 자료타입
            // 회전
            stream.SendNext(this.transform.rotation);
        }
        else if (stream.IsReading == true)
        {
            getPos = (Vector3)stream.ReceiveNext();                                 // objcet 타입 -> 어떤 자료형이 올지 모르니. 
                                                                                    // 박싱, 언박싱 게ㅐ념이 필요(형변환)을 하자 
            getRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
