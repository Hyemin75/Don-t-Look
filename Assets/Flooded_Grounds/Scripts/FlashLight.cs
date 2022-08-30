using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Light Flashlight;

    private bool isActive = false;
    AudioSource audioSource;

    private void Awake()
    {
        Flashlight = gameObject.GetComponentInChildren<Light>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Flashlight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            audioSource.Play();
            isActive = !isActive;
            Flashlight.gameObject.SetActive(isActive);
        }
    }
}
