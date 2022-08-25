using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    None,
    Stop,
    Move,
    Attack
}

public class Enemy : MonoBehaviour
{
    public EnemyState state;
    public EnemyState prevState = EnemyState.None;

    Animator animator;

    Vector3 targetPos;
    float moveSpeed = 7f;

    public GameObject target;
    bool isFindEnemy = false;
    Plane[] eyePlanes;
    Camera eye;

    GameObject awakeCollider;
    GameObject attackCollider;


    private void Awake()
    {
    }


    private void Start()
    {        
        attackCollider.SetActive(false);
        awakeCollider.SetActive(false);

        ChangeState(EnemyState.None);
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.None: UpdateNone(); break;
            case EnemyState.Stop: UpdateStop(); break;


        }
    }

    void UpdateNone()
    {
        if (IsFindEnemy())
        {
            ChangeState(EnemyState.Stop);
        }
    }

    void UpdateStop()
    {
        //플레이어가 쳐다보는지
        //if(CanMove())
        //{
        //    ChangeState(EnemyState.Move);
        //}

    }
    
    void UpdateMove()
    {
        //if(!CanMove())
        //{
        //    ChangeState(EnemyState.Stop);
        //}
    }

    void UpdateAttack()
    {

    }

    void ChangeState(EnemyState nextState)
    {
        if (prevState == nextState) return;

        StopAllCoroutines();

        prevState = state;
        state = nextState;

        animator.SetBool("IsNone", false);
        animator.SetBool("IsAttack", false);

        switch(state)
        {
            case EnemyState.None: StartCoroutine(CoroutineNone()); break;
        }

    }


    IEnumerator CoroutineNone()
    {
        animator.SetBool("isNone", true);

        while (true)
        {
            yield return new WaitForSeconds(2f);
            yield break;
        }

    }
    IEnumerator CoroutineAttack()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        animator.SetTrigger("isAttack");

        yield return new WaitForSeconds(1f);
        ChangeState(EnemyState.Attack);
        yield break;
    }




    //플레이어가 쳐다볼땐 플레이어를 죽이지 x => 플레이어가 보면서 부딪히면 죽지 x  
    //플레이어가 보지 않을 때 0.5초마다 플레이어 방향으로 플레이어와 거리의  1/8 만큼 다가옴
    //플레이어와의 거리가 충분히 좁혀졌을 땐 보지 않을 때 7의 속력으로 다가옴
    //플레이어가 볼 때 비활성화

    bool IsFindEnemy()
    {
        if(!target.activeSelf) return false;

        Bounds targetBounds = target.GetComponentInChildren<MeshRenderer>().bounds;
        eyePlanes = GeometryUtility.CalculateFrustumPlanes(eye);
        isFindEnemy = GeometryUtility.TestPlanesAABB(eyePlanes, targetBounds);

        return isFindEnemy; 
    }


}
