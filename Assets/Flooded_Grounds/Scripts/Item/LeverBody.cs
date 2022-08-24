using System;
using UnityEngine;

public class LeverBody : MonoBehaviour
{
    [System.Serializable]
    public class GateOpen : UnityEngine.Events.UnityEvent<bool> { }

    [HideInInspector]
    public GateOpen gateOpenEvent = new GateOpen();

    [SerializeField]
    GameObject ElectricEffect;
    [SerializeField]
    GameObject Audio;


    private bool isGateOpen = false;

    private void Awake()
    {

    }


    public void IsGateOpen(bool GateOpen)
    {
        isGateOpen = GateOpen;
        gateOpenEvent.Invoke(isGateOpen);

        if(isGateOpen)
        {
            ElectricEffect.SetActive(false);
            Audio.SetActive(false);
        }
    }

}
