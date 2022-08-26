using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public GameObject target;
    Vector3 targetPos;

    bool isFindEnemy = false;

    bool isPlayerSeeing;

    NavMeshAgent navMeshAgent;

    [SerializeField]
    AudioClip lookingSound;
    [SerializeField]
    AudioClip moveSound;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    IsSeeing isSeeing;

    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }


    private void Start()
    {               
        ChangeState(EnemyState.None);
    }

    private void Update()
    {

        switch (state)
        {
            case EnemyState.None: UpdateNone(); break;
            case EnemyState.Stop: UpdateStop(); break;
            case EnemyState.Move: UpdateMove(); break;
            case EnemyState.Attack: UpdateAttack(); break;
        }
    }

    void UpdateNone()
    {
        if (isFindEnemy)
        {
            PlaySound("LOOKENEMY");
            ChangeState(EnemyState.Stop);
        }
    }


    void UpdateStop()
    {
        isPlayerSeeing = isSeeing.isPlayerSeeing;
        transform.GetChild(0).gameObject.tag = "Figure";
        navMeshAgent.enabled = false;

        //플레이어가 쳐다보는지
        if (!isPlayerSeeing)
        {
            ChangeState(EnemyState.Move);
        }
    }
    
    void UpdateMove()
    {
        if (isSeeing.isPlayerSeeing)
        {
            audioSource.Stop();
            ChangeState(EnemyState.Stop);

            return;
        }

        if (100f > Vector3.Distance(target.transform.position, transform.position))
        {

            navMeshAgent.speed = 8.5f;
        }
        navMeshAgent.SetDestination(target.transform.position);
        
        //if (!isPlayerSeeing)
        //{ 
        //    transform.GetChild(0).gameObject.tag = "Enemy";

        //    //성능에 안좋음
            
        //}
        //else if(isPlayerSeeing)
        //{
        //    ChangeState(EnemyState.Stop);
        //}

        //transform.GetChild(0).gameObject의 콜라이더에 player 닿으면 웃음소리 재생 
        
    }

    void UpdateAttack()
    {
        
        //소리 멈춤

    }

    void ChangeState(EnemyState nextState)
    {
        Debug.Log("체인지 함수 들어옴");

        if (state == nextState)
            return;
        Debug.Log("리턴 안됨");

        StopAllCoroutines();

        prevState = state;
        state = nextState;

        switch (state)
        {
            case EnemyState.None: StartCoroutine(CoroutineNone()); break;
            case EnemyState.Stop:

                StartCoroutine(CoroutineStop()); break;
            case EnemyState.Move:
                Debug.Log("추적 상태 시작");
                audioSource.loop = true;
                PlaySound("MOVETOENEMY");
                targetPos = target.transform.position;

                navMeshAgent.enabled = true;
                navMeshAgent.speed = 20f;
                //StartCoroutine(CoroutineMove()); break;
                break;
            case EnemyState.Attack: StartCoroutine(CoroutineAttack()); break;
        }

    }

    #region CoroutineDetail
    IEnumerator CoroutineNone()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("대기 상태 시작");
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            yield break;
        }
    }
    IEnumerator CoroutineStop()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("멈춰있음");
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
        }
    }
    IEnumerator CoroutineMove()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
       

        while (true)
        {
            yield return new WaitForSeconds(1f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
        }
    }
    IEnumerator CoroutineAttack()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        yield return new WaitForSeconds(1f);
        ChangeState(EnemyState.None);
        yield break;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (!isFindEnemy)
        {
            if (other.tag == "Figure" || other.tag == "Player")
            {
                isFindEnemy = true;
                transform.LookAt(target.transform.position);
            }
        }
    }


    void PlaySound(string action)
    {
        switch (action)
        {
            case "LOOKENEMY":
                audioSource.clip = lookingSound;
                break;
            case "MOVETOENEMY":
                audioSource.clip = moveSound;
                break;
        }
        audioSource.Play();
    }
}
