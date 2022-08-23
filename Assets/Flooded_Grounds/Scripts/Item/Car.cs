using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    public AudioClip CarSound;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ArrivedCar(bool arrived)
    {
        if(arrived)
        {
            PlaySound("Car");
            GameManager.Instance.isGameEnd = true;
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Car":
                audioSource.clip = CarSound;
                break;
        }
        audioSource.Play();
    }
}
