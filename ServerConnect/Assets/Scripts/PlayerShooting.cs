using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerShooting : MonoBehaviourPun
{
    //입력값을 받기위한 객체
    PlayerInput pl;
    Animator anim;
    //총을 쏘기 위한 애니메이션
    bool firepress, relodepress;//총이 한번만 눌리기 위해서 체크하는 변수
    public Transform Gun_Pivot; //발사점
    Gun gun;//총

    // Start is called before the first frame update
    void Start()
    {
        pl = GetComponent<PlayerInput>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //발사할때
        if(pl.fire == 1)
        {
            if(firepress == false)
            {
                firepress = true;
                print("발사");
                gun.GetComponent<Gun>().Fire();
                anim.SetTrigger("Fire");
            }
        }
        else//버튼이 떼어졌을 때
        {
            firepress = false;
        }

        if(pl.relode == 1)
        {
            if(relodepress == false)
            {
                relodepress = true;
                print("리로드");
                anim.SetTrigger("Reload");
            }
        }


    }
}
