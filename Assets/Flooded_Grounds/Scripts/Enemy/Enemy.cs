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

    NavMeshAgent agent;

    public GameObject target;
    Vector3 targetPos;

    bool isFindEnemy = false;

    bool isPlayerSeeing = false;

    [SerializeField]
    AudioClip lookingSound;
    [SerializeField]
    AudioClip moveSound;


    [SerializeField]
    AudioSource audioSource;
    

    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
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
        //agent.Stop();
        transform.GetChild(0).gameObject.tag = "Figure";
        
        //플레이어가 쳐다보는지
        if (!isPlayerSeeing)
        {
            ChangeState(EnemyState.Move);
        }
    }
    
    void UpdateMove()
    {
        
        if(!isPlayerSeeing)
        { 
            transform.GetChild(0).gameObject.tag = "Enemy";

            //거리가 멀면 속도 20f, 거리가 가까우면 7f
            if(Vector3.Distance(target.transform.position, gameObject.transform.position) > 100f)
            {
                agent.speed = 20f;
            }
            else
            {
                agent.speed = 8.5f;
            }
            agent.SetDestination(target.transform.position);
        }
        else
        {
            ChangeState(EnemyState.Stop);
        }

        //transform.GetChild(0).gameObject의 콜라이더에 player 닿으면 웃음소리 재생 
        
    }

    void UpdateAttack()
    {
        
        //소리 멈춤

    }

    void ChangeState(EnemyState nextState)
    {
        if (prevState == nextState) return;

        StopAllCoroutines();

        prevState = state;
        state = nextState;

        switch (state)
        {
            case EnemyState.None: StartCoroutine(CoroutineNone()); break;
            case EnemyState.Stop: StartCoroutine(CoroutineStop()); break;
            case EnemyState.Move: StartCoroutine(CoroutineMove()); break;
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
        Debug.Log("추적 상태 시작");
        audioSource.loop = true;
        PlaySound("MOVETOENEMY");

        targetPos = target.transform.position;

        while (true)
        {
            yield return new WaitForSeconds(5f);
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
            case "LOOKENMMY":
                audioSource.clip = lookingSound;
                break;
            case "MOVETOENEMY":
                audioSource.clip = moveSound;
                break;
        }
        audioSource.Play();
    }
}
