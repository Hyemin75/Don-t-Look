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
    Collider targetCollider;

    bool isFindEnemy = false;

    NavMeshAgent navMeshAgent;
    SphereCollider sphereCollider;

    [SerializeField]
    AudioClip lookingSound;
    [SerializeField]
    AudioClip moveSound;
    [SerializeField]
    AudioClip screamSound;

    AudioSource audioSource;

    bool isPlayerSeeing = false;
    MeshRenderer meshRenderer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
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
        if (isPlayerSeeing)
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

    void ChangeState(EnemyState nextState)
    {

        if (state == nextState)
            return;

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
            case EnemyState.Attack:
                 GameManager.Instance.EventGameSequence(1);
                 audioSource.loop = false;
                 PlaySound("SCREAM");
                 break;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(isFindEnemy && other.tag == "Player" && state == EnemyState.Move)
        {
            ChangeState(EnemyState.Attack);
            GameManager.Instance.isGameOver = true;
        }
        else if (!isFindEnemy)
        {
            if (other.tag == "Figure" || other.tag == "Player")
            {
                isFindEnemy = true;
                transform.LookAt(target.transform.position);
                sphereCollider.radius = 0.6f;
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
            case "SCREAM":
                audioSource.clip = screamSound;
                break;
        }
        audioSource.Play();
    }


    private void OnBecameInvisible()
    {
        Debug.Log("안봄");
        isPlayerSeeing = false;
    }

    private void OnBecameVisible()
    {
        Debug.Log("봄");
        isPlayerSeeing = true;
    }

}
