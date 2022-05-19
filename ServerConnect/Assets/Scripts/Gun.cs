using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //총관련소리들
    public AudioClip shotClip;
    public AudioClip reloadClip;
    AudioSource audio; //총알 재생을 위한 객체
    LineRenderer lr; //총알이 날아가는 효과(궤적표시)
    //RayCast를 사용할 예정 -->
    float fireDistance = 50f;

    //내부의 자료타입 공격력, 탄약, 탄창
    public float damage = 25;
    public int ArmorRemain = 100; //총 탄환
    public int mag_capacity = 25; //탄창당 탄환

    public enum State
    {
        Ready, Empty, Reloading
    //준비,  총알없음,  장전중
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
        //총을 쏘는 상태를 정의
        //기본적으로 Ready상태
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
            //맞았다 처리
            //hit 다양한 정보가 있음
            hit.point = hitPosition;

        }
        else
        {
            //안맞았을떄처리   현재발사점       +     앞방향             *   유효사거리
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        //이후에는 맞았던, 안맞았던 이펙트가 보여야 한다.
        shotEffectProcess(hitPosition);
    }



    void shotEffectProcess(Vector3 pos)
    {
        StartCoroutine(ShotEffect(pos));
    }

    IEnumerator ShotEffect(Vector3 hitpos)
    {

        //소리를 내고 
        this.audio.PlayOneShot(shotClip);
        //라인렌더러를 만들고
        this.lr.enabled = true;
        lr.SetPosition(0, fireTransform.position);
        lr.SetPosition(1, hitpos);
        yield return new WaitForSeconds(0.05f);

        lr.enabled = false;

    }

    public void Reload()
    {
       if(state == State.Reloading)
        {//리로드 상황일 때 다양한 처리
            return;


        }

       //키가 눌리면 다음 코루틴 실행
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
