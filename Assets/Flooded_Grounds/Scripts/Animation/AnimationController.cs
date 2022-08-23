using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animation anim;
    
    [SerializeField]
    private ActionController actionController;

    [SerializeField]
    GameObject gate;

    private bool isGateOpen = false;

    private void Awake()
    {
    }

    private void Start()
    {
        anim = gate.GetComponent<Animation>();
    }

    private void Update()
    {
        if(isGateOpen)
        {
            anim.Play();
        }
    }

    void CheckOpen(bool isOpen)
    {
       if(isOpen)
       {
           isGateOpen = true;
       }
    }

}
