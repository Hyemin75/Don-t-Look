using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    public float moveSpeed;
    public float runSpeed = 10f;

    [SerializeField]
    private float jumpForce = 2.5f;
    private float gravity = -9.8f;
    private Vector3 moveDirector;

    [SerializeField]
    private Transform cameraTransform;
    private CharacterController characterController;

    [SerializeField]
    private AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip walkSound;
    public AudioClip deadInWater;
    public AudioClip scream;
    public AudioClip jumpSound;

    private bool isMoving;

    [HideInInspector]
    public float WaterHeight = 15.0f;
    public bool IsDead = false; 


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (transform.position.y <= WaterHeight)
        {
            DeadInWater();
        }

        if (!IsDead)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            moveSpeed = 5.0f;
            Move(new Vector3(x, 0, z));

            if (characterController.velocity.x == 0)
            {
                PlaySound("WALK");
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = runSpeed;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PlaySound("RUN");
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                PlaySound("WALK");
            }

            if (characterController.isGrounded == false)
            {
                moveDirector.y += gravity * Time.deltaTime;
            }
            characterController.Move(moveDirector * moveSpeed * Time.deltaTime);

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
        }
    }

    public void Move(Vector3 direction)
    {
         Vector3 movedis = cameraTransform.rotation * direction;
        moveDirector = new Vector3(movedis.x, moveDirector.y, movedis.z);
    }

    public void Jump()
    {
        if (characterController.isGrounded == true)
        {
            PlaySound("JUMP");
            moveDirector.y = jumpForce;
        }
    }

    public void PlaySound(string action)
    {
        switch (action)
        {
            case "WALK":
                audioSource.clip = walkSound;
                break;
            case "RUN":
                audioSource.clip = runSound;
                break;
            case "JUMP":
                audioSource.clip = jumpSound;
                break;
            case "DEADINWATER":
                audioSource.clip = deadInWater;
                break;
            case "SCREAM":
                audioSource.clip = scream;
                break;

        }
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;
        PlaySound("SCREAM");
        GameManager.Instance.isGameOver = true;
    }

    public void DeadInWater()
    {
        IsDead = true;
        PlaySound("DEADINWATER");
        GameManager.Instance.isGameOver = true;

    }

}


