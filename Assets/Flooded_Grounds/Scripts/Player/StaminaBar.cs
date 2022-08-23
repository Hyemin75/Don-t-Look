using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField]
    float MaxStamina;
    
    [SerializeField]
    float stamina;

    [SerializeField]
    Slider L_staminaBar;
    [SerializeField]
    Slider R_staminaBar;

    [SerializeField]
    float decreaseValue;

    [SerializeField]
    float increaseValue;

    PlayerController playerController;
    

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        stamina = MaxStamina;
        L_staminaBar.maxValue = MaxStamina;
        R_staminaBar.maxValue = MaxStamina;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            DecreaseEnergy();
        }
        else if(stamina != MaxStamina)
        {
            IncreaseEnergy();
        }

        L_staminaBar.value = stamina;
        R_staminaBar.value = stamina;
    }


    private void DecreaseEnergy()
    {
        if(stamina > 0)
        {
            playerController.runSpeed = 10f;
            stamina -= decreaseValue * Time.deltaTime;
        }
        else
        {
            CantRun();
            IncreaseEnergy();
        }
    }

    private void IncreaseEnergy()
    {
        if(stamina < MaxStamina)
            stamina += increaseValue * Time.deltaTime;
    }

    public void CantRun()
    {
        if (stamina <= 0)
        {
            if (playerController.moveSpeed >= 10f)
            {
                playerController.runSpeed = 5f;
                playerController.PlaySound("WALK");
                
            }
        }
    }

}
