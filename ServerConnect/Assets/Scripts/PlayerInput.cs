#define PC_Platform
//#define VR_Platform

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerInput : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public float move       { get; set; }
    public float rotate     { get; set; }
    public float fire       { get; set; }
    public float relode     { get; set; }

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
        {
            return;
        }

#if PC_Platform
            move = Input.GetAxis("Vertical");
            rotate = Input.GetAxis("Horizontal");
            fire = Input.GetAxisRaw("Fire1");
            relode = Input.GetAxisRaw("Fire2");
#endif
        
    }
}
