using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    public AudioClip CarSound;

    AudioSource audioSource;

    Transform carTransform;

    bool CanMove = false;
    float speed = 3f;
    bool carMove = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        carTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveCar(CanMove);

    }

    public void CanMoveCar(bool arrived)
    {
        PlaySound("Car");
        CanMove = arrived;
    }

    void MoveCar(bool CanMove)
    {
        float Move = Time.deltaTime * speed;
        if (CanMove)
        {
            carMove = true;
            transform.Translate(Vector3.forward * Move);
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
