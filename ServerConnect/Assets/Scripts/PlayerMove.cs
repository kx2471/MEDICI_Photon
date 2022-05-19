using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMove : MonoBehaviourPunCallbacks
{
    //�� ��ü�� ����Ǿ� �ִ� ��ü�� �����ϱ� ���� ����
    Rigidbody rb;//�� ������ü
    PlayerInput pi;//�� �̵����� �Է°�ü
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
        //Vector 3 Distance = Ű�Է¹��� * ������ * �ӵ� * Time.deltaTime;
        Vector3 Distance = pi.move * this.transform.forward * 5f * Time.deltaTime;
        
        rb.MovePosition(rb.position + Distance);
        //rb.MovePosition(rb.position + distance);//�������� �̾� �̵� �� ��ġ�� �������� �̵���
        //this.transform.Translate(Vector3.zero); //����ġ�� �������� �̵��� ��Ų��.
        //�������� ����ȵ� -> �浹�϶��� �������� ����
        //�ǵ�ġ���� ���� �̵� ȸ������ �߻� �� �� �ִ�.
    }
    void RotChar()
    {
        float turn = pi.rotate * 120 * Time.deltaTime;
        rb.rotation = rb.rotation * Quaternion.Euler(0,turn,0);
    }
}
