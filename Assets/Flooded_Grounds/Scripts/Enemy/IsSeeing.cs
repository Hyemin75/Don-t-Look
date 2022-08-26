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
        Debug.Log("¾Èº½");
        isPlayerSeeing = false;
    }

    private void OnBecameVisible()
    {
        Debug.Log("º½");
        isPlayerSeeing = true;
    }

}
