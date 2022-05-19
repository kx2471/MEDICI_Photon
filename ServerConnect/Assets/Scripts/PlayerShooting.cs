using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerShooting : MonoBehaviourPun
{
    //�Է°��� �ޱ����� ��ü
    PlayerInput pl;
    Animator anim;
    //���� ��� ���� �ִϸ��̼�
    bool firepress, relodepress;//���� �ѹ��� ������ ���ؼ� üũ�ϴ� ����
    public Transform Gun_Pivot; //�߻���
    Gun gun;//��

    // Start is called before the first frame update
    void Start()
    {
        pl = GetComponent<PlayerInput>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //�߻��Ҷ�
        if(pl.fire == 1)
        {
            if(firepress == false)
            {
                firepress = true;
                print("�߻�");
                gun.GetComponent<Gun>().Fire();
                anim.SetTrigger("Fire");
            }
        }
        else//��ư�� �������� ��
        {
            firepress = false;
        }

        if(pl.relode == 1)
        {
            if(relodepress == false)
            {
                relodepress = true;
                print("���ε�");
                anim.SetTrigger("Reload");
            }
        }


    }
}
