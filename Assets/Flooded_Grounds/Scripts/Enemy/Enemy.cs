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

        //�÷��̾ �Ĵٺ�����
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

        //    //���ɿ� ������
            
        //}
        //else if(isPlayerSeeing)
        //{
        //    ChangeState(EnemyState.Stop);
        //}

        //transform.GetChild(0).gameObject�� �ݶ��̴��� player ������ �����Ҹ� ��� 
        
    }

    void UpdateAttack()
    {
        
        //�Ҹ� ����

    }

    void ChangeState(EnemyState nextState)
    {
        Debug.Log("ü���� �Լ� ����");

        if (state == nextState)
            return;
        Debug.Log("���� �ȵ�");

        StopAllCoroutines();

        prevState = state;
        state = nextState;

        switch (state)
        {
            case EnemyState.None: StartCoroutine(CoroutineNone()); break;
            case EnemyState.Stop:

                StartCoroutine(CoroutineStop()); break;
            case EnemyState.Move:
                Debug.Log("���� ���� ����");
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
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        Debug.Log("��� ���� ����");
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            yield break;
        }
    }
    IEnumerator CoroutineStop()
    {
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        Debug.Log("��������");
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // �ð����� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        }
    }
    IEnumerator CoroutineMove()
    {
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
       

        while (true)
        {
            yield return new WaitForSeconds(1f);
            // �ð����� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        }
    }
    IEnumerator CoroutineAttack()
    {
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
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
