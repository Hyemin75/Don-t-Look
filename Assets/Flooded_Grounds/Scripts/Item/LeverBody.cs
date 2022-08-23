using System;
using UnityEngine;

public class LeverBody : MonoBehaviour
{
    [System.Serializable]
    public class GateOpen : UnityEngine.Events.UnityEvent<bool> { }

    [HideInInspector]
    public GateOpen gateOpenEvent = new GateOpen();

    AudioSource audioSource;

    [SerializeField]
    AudioClip PullLeverSound;

    private bool isGateOpen = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void IsGateOpen(bool GateOpen)
    {
        isGateOpen = GateOpen;
        gateOpenEvent.Invoke(isGateOpen);

        if(isGateOpen)
        {
            PlaySound("PULLEVER");
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
          case "PULLLEVER":
                audioSource.clip = PullLeverSound;
                break;
        }
        audioSource.Play();
    }
}
