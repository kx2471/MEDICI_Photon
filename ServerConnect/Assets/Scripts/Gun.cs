using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //�Ѱ��üҸ���
    public AudioClip shotClip;
    public AudioClip reloadClip;
    AudioSource audio; //�Ѿ� ����� ���� ��ü
    LineRenderer lr; //�Ѿ��� ���ư��� ȿ��(����ǥ��)
    //RayCast�� ����� ���� -->
    float fireDistance = 50f;

    //������ �ڷ�Ÿ�� ���ݷ�, ź��, źâ
    public float damage = 25;
    public int ArmorRemain = 100; //�� źȯ
    public int mag_capacity = 25; //źâ�� źȯ

    public enum State
    {
        Ready, Empty, Reloading
    //�غ�,  �Ѿ˾���,  ������
    }
    public State state {get; private set;}

    public Transform fireTransform;

    private void Awake()
    {
        audio = this.GetComponent<AudioSource>();
        lr = this.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.enabled = false;
    }

    public void Fire()
    {
        //���� ��� ���¸� ����
        //�⺻������ Ready����
        if (state == State.Ready)
        {
            Shot();
        }
    }

    public void Shot()
    {

        mag_capacity--;
        if(mag_capacity <= 0)
        {
            state = State.Empty;
        }
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;
        if(Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance)
            == true)
        {
            //�¾Ҵ� ó��
            //hit �پ��� ������ ����
            hit.point = hitPosition;

        }
        else
        {
            //�ȸ¾�����ó��   ����߻���       +     �չ���             *   ��ȿ��Ÿ�
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        //���Ŀ��� �¾Ҵ�, �ȸ¾Ҵ� ����Ʈ�� ������ �Ѵ�.
        shotEffectProcess(hitPosition);
    }



    void shotEffectProcess(Vector3 pos)
    {
        StartCoroutine(ShotEffect(pos));
    }

    IEnumerator ShotEffect(Vector3 hitpos)
    {

        //�Ҹ��� ���� 
        this.audio.PlayOneShot(shotClip);
        //���η������� �����
        this.lr.enabled = true;
        lr.SetPosition(0, fireTransform.position);
        lr.SetPosition(1, hitpos);
        yield return new WaitForSeconds(0.05f);

        lr.enabled = false;

    }

    public void Reload()
    {
       if(state == State.Reloading)
        {//���ε� ��Ȳ�� �� �پ��� ó��
            return;


        }

       //Ű�� ������ ���� �ڷ�ƾ ����
        StartCoroutine(ReloadProcess());
    }
    IEnumerator ReloadProcess()
    {
        state = State.Reloading;
        this.audio.PlayOneShot(reloadClip);
        yield return new WaitForSeconds(0.05f);
     

    state = State.Ready;
    }
   

    void Start()
    {
       state = State.Ready;
       
    }

    void Update()
    {
        UIManager.instance.UpdateArmoText
            
            (mag_capacity, ArmorRemain);
    }
}
