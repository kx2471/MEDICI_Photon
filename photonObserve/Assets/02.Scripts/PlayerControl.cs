using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed = 5.0f;
    public float rotSpeed = 120.0f;

    Vector3 getPos;                                                                 // ��Ʈ��ũ �� �̵� �����͸� ����(Remote ��ü)
    Quaternion getRot;                                                              // ��Ʈ��ũ �� ȸ�� �����͸� ����(Remote ��ü)

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
        else                                                                        // �� ��ü������ ��Ʈ��ũ������ Mine�� �ƴ� ��ü
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
            // �̵�
            stream.SendNext(this.transform.position);                               // �ڷ�Ÿ��
            // ȸ��
            stream.SendNext(this.transform.rotation);
        }
        else if (stream.IsReading == true)
        {
            getPos = (Vector3)stream.ReceiveNext();                                 // objcet Ÿ�� -> � �ڷ����� ���� �𸣴�. 
                                                                                    // �ڽ�, ��ڽ� �Ԥ����� �ʿ�(����ȯ)�� ���� 
            getRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
