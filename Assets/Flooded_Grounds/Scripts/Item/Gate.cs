using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private LeverBody leverbody;

    Animation OpenGateAnim;

    [SerializeField]
    public AudioClip openSound;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        OpenGateAnim = GetComponent<Animation>();
        leverbody.gateOpenEvent.AddListener(OnGateOpenAnimation);
    }

    void OnGateOpenAnimation(bool isOpen)
    {
        if(isOpen)
        {
            OpenGateAnim.Play();
            PlaySound("OPEN");
        }
    }

    void PlaySound(string action)
    {
        switch(action)
        {
            case "OPEN":
                audioSource.clip = openSound;
                break;
        }
        audioSource.Play();
    }

}
