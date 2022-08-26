using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSeeing : MonoBehaviour
{


    [HideInInspector]
    public bool isPlayerSeeing = false;

    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }


    private void OnBecameInvisible()
    {
        Debug.Log("�Ⱥ�");
        isPlayerSeeing = false;
    }

    private void OnBecameVisible()
    {
        Debug.Log("��");
        isPlayerSeeing = true;
    }

}
