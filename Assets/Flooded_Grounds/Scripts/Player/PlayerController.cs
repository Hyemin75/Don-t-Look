using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Stand,
    Walk,
    Run,
    Jump,
    Dead
}

public class PlayerController : MonoBehaviour
{

    public PlayerState state;
    public PlayerState prevState = PlayerState.Stand;

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
    public AudioClip runSound;
    public AudioClip walkSound;
    public AudioClip deadInWater;
    public AudioClip scream;
    public AudioClip jumpSound;
    
    AudioSource audioSource;

    private bool isMoving;

    [HideInInspector]
    public float WaterHeight = 15.0f;
    public bool IsDead = false; 


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ChangeState(PlayerState.Stand);
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        MoveCameraDirection(new Vector3(x, 0, z));
        characterController.Move(moveDirector * moveSpeed * Time.deltaTime);
        
        moveDirector.y += gravity * Time.deltaTime;

        switch (state)
        {
            case PlayerState.Stand: UpdateStand(); break;
            case PlayerState.Walk: UpdateWalk(); break;
            case PlayerState.Run: UpdateRun(); break;
            case PlayerState.Jump: UpdateJump(); break;
            case PlayerState.Dead: UpdateDead(); break;
        }

        if (transform.position.y <= WaterHeight)
        {
            ChangeState(PlayerState.Dead);
        }
    }


    public void MoveCameraDirection(Vector3 direction)
    {
        Vector3 movedis = cameraTransform.rotation * direction;
        moveDirector = new Vector3(movedis.x, moveDirector.y, movedis.z);
    }

    void UpdateStand()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            ChangeState(PlayerState.Walk);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            ChangeState(PlayerState.Jump);
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            ChangeState(PlayerState.Run);
        }

    }

    void UpdateWalk()
    {
        if(characterController.velocity.x == 0)
        {
            ChangeState(PlayerState.Stand);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(PlayerState.Jump);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            ChangeState(PlayerState.Run);
        }
    }

    void UpdateJump()
    {
        if(characterController.isGrounded)
        {
            moveDirector.y = jumpForce;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && characterController.isGrounded == true)
        {
            ChangeState(PlayerState.Run);
        }
        else if(characterController.isGrounded)
        {
            ChangeState(PlayerState.Walk);
        }
    }

    void UpdateRun()
    {
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            ChangeState(PlayerState.Walk);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ChangeState(PlayerState.Jump);
        }
    }

    void UpdateDead()
    { 
        IsDead = true;
        GameManager.Instance.isGameOver = true;
    }

    void ChangeState(PlayerState nextState)
    {
        if (state == nextState) return;

        prevState = state;
        state = nextState;

        switch (state)
        {
            case PlayerState.Stand:
                audioSource.Stop();
                break;
            case PlayerState.Walk:
                moveSpeed = 5.0f;
                audioSource.loop = true;
                PlaySound("WALK"); 
                break;
            case PlayerState.Run:
                moveSpeed = runSpeed;
                audioSource.loop = true;
                PlaySound("RUN");
                break;
            case PlayerState.Jump:
                PlaySound("JUMP");
                audioSource.loop = false;
                break;
            case PlayerState.Dead:
                PlaySound("DEADINWATER"); break;
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
        }
        audioSource.Play();
    }
}


