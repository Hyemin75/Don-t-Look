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
        //�÷��̾ �Ĵٺ�����
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
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        animator.SetTrigger("isAttack");

        yield return new WaitForSeconds(1f);
        ChangeState(EnemyState.Attack);
        yield break;
    }




    //�÷��̾ �Ĵٺ��� �÷��̾ ������ x => �÷��̾ ���鼭 �ε����� ���� x  
    //�÷��̾ ���� ���� �� 0.5�ʸ��� �÷��̾� �������� �÷��̾�� �Ÿ���  1/8 ��ŭ �ٰ���
    //�÷��̾���� �Ÿ��� ����� �������� �� ���� ���� �� 7�� �ӷ����� �ٰ���
    //�÷��̾ �� �� ��Ȱ��ȭ

    bool IsFindEnemy()
    {
        if(!target.activeSelf) return false;

        Bounds targetBounds = target.GetComponentInChildren<MeshRenderer>().bounds;
        eyePlanes = GeometryUtility.CalculateFrustumPlanes(eye);
        isFindEnemy = GeometryUtility.TestPlanesAABB(eyePlanes, targetBounds);

        return isFindEnemy; 
    }


}
