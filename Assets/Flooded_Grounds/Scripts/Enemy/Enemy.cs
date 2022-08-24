using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //기본 비활성화 상태 + 태그 figure
    //활성화 된 적 or 플레이어의 콜라이더와 부딪히면 활성화 태그 변경 figure -> enemy   
    
    public GameObject ActiveCollider;

    bool isActive = false; // 활성화 판단
    bool CanMove = false; // 플레이어가 쳐다보는지 판단
    bool isClose = false;

    [SerializeField]
    GameObject Player;

    Vector3 distance;



    private void Update()
    {
        distance = gameObject.transform.position - Player.transform.position;
        //활성화 상태인지
        if(isActive)
        {
            EnemyMove();
        }
    }

    //비활성화 -> 활성화 + 태그 변경
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ld");
        if(other.tag == "View" || other.tag == "Enemy")
        {
            isActive = true;
        }
    }

    //테그 변경과 이동 메소드
    void EnemyMove()
    {
        if(IsInPlayerView())
        {
            gameObject.tag = "Enemy";
            EnemyMovement();
        }
        else
        {
            gameObject.tag = "Figure";
        }
    }

    //bool IsClose()
    //{
    //    //if(distance <= 80f)
    //    //{
    //    //
    //    //}
    //    //return isClose;
    //}
    //

    //플레이어 시야와 충돌 했는지 판별하는 메소드
    bool IsInPlayerView()
    {
        //

        return CanMove;
    }

    //적 움직임
    void EnemyMovement()
    {
      //  if()
    }



    //플레이어가 쳐다볼땐 플레이어를 죽이지 x => 플레이어가 보면서 부딪히면 죽지 x  
    //플레이어가 보지 않을 때 0.5초마다 플레이어 방향으로 플레이어와 거리의  1/8 만큼 다가옴
    //플레이어와의 거리가 충분히 좁혀졌을 땐 보지 않을 때 7의 속력으로 다가옴
    //플레이어가 볼 때 비활성화




}
