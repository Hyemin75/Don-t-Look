using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //�⺻ ��Ȱ��ȭ ���� + �±� figure
    //Ȱ��ȭ �� �� or �÷��̾��� �ݶ��̴��� �ε����� Ȱ��ȭ �±� ���� figure -> enemy   
    
    public GameObject ActiveCollider;

    bool isActive = false; // Ȱ��ȭ �Ǵ�
    bool CanMove = false; // �÷��̾ �Ĵٺ����� �Ǵ�
    bool isClose = false;

    [SerializeField]
    GameObject Player;

    Vector3 distance;



    private void Update()
    {
        distance = gameObject.transform.position - Player.transform.position;
        //Ȱ��ȭ ��������
        if(isActive)
        {
            EnemyMove();
        }
    }

    //��Ȱ��ȭ -> Ȱ��ȭ + �±� ����
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ld");
        if(other.tag == "View" || other.tag == "Enemy")
        {
            isActive = true;
        }
    }

    //�ױ� ����� �̵� �޼ҵ�
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

    //�÷��̾� �þ߿� �浹 �ߴ��� �Ǻ��ϴ� �޼ҵ�
    bool IsInPlayerView()
    {
        //

        return CanMove;
    }

    //�� ������
    void EnemyMovement()
    {
      //  if()
    }



    //�÷��̾ �Ĵٺ��� �÷��̾ ������ x => �÷��̾ ���鼭 �ε����� ���� x  
    //�÷��̾ ���� ���� �� 0.5�ʸ��� �÷��̾� �������� �÷��̾�� �Ÿ���  1/8 ��ŭ �ٰ���
    //�÷��̾���� �Ÿ��� ����� �������� �� ���� ���� �� 7�� �ӷ����� �ٰ���
    //�÷��̾ �� �� ��Ȱ��ȭ




}
