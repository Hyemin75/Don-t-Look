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
    AudioClip moveSound;
    AudioClip stopSound;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
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
            ChangeState(EnemyState.Stop);
        }
    }

    void UpdateStop()
    {
        agent.Stop();
        transform.GetChild(0).gameObject.tag = "Figure";
        
        //한번만 재생하는지 체크
        PlaySound("STOP");

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
            PlaySound("MOVETOENEMY");
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Figure" || other.tag == "Player")
        {
            isFindEnemy = true;
            transform.LookAt(targetPos);
            PlaySound("LOOKENEMY");
            
            ChangeState(EnemyState.Stop);
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
            case "STOP":
                audioSource.clip = stopSound;
                break;
        }
        audioSource.Play();
    }
}
