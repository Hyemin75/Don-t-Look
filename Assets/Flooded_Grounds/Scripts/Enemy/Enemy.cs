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

        //transform.GetChild(0).gameObject의 콜라이더에 player 닿으면 웃음소리 재생 
        
    }

    void UpdateAttack()
    {
        
        //소리 멈춤

    }

    void ChangeState(EnemyState nextState)
    {

        if (state == nextState)
            return;

        StopAllCoroutines();

        prevState = state;
        state = nextState;

        switch (state)
        {
            case EnemyState.None: break;
            case EnemyState.Stop: break;
            case EnemyState.Move:
                Debug.Log("추적 상태 시작");
                audioSource.loop = true;
                PlaySound("MOVETOENEMY");
                targetPos = target.transform.position;

                navMeshAgent.enabled = true;
                navMeshAgent.speed = 20f;
                break;
        }

    }


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
