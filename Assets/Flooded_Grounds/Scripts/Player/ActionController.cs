using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ActionController : MonoBehaviour
{
    [SerializeField]
    LeverBody leverBody;

    [SerializeField]
    Car car;

    [SerializeField]
    private float range;
    private bool pickupActivated = false;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    private TextMeshProUGUI actionText;
    private TextMeshProUGUI useText;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    public AudioClip failSound;

    AudioSource audioSource;
 
    [SerializeField]
    AudioClip getItemSound;

    private bool GetLever = false;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        GameObject ActionText = GameObject.Find("ActionText");
        actionText = ActionText.GetComponent<TextMeshProUGUI>();

        GameObject UseText = GameObject.Find("UseText");
        useText = UseText.GetComponent<TextMeshProUGUI>();

        actionText.gameObject.SetActive(false); // gameobhect.find �� ĵ���� ã��
        useText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TryAction();        
    }

    private void TryAction()
    {
        CheckItem();

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(hitInfo.transform.tag == "LeverBody")
            {
                UseLever();
            }
            else if(hitInfo.transform.tag == "Car")
            {
                UseCar();
            }
            else
            {
                CanPickUp();
            }
        }
    }

    private void CanPickUp()
    {
        if(pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
                inventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                PlaySound("GETITEM");
            }

            if(hitInfo.transform.GetComponent<ItemPickUp>().name == "Lever")
            {
                GetLever = true;
            }
        }
    }

    private void UseCar()
    {
        if (pickupActivated)
        {
            car.CanMoveCar(true);
        }
    }

    private void UseLever()
    {
        if (pickupActivated)
        {
            if(GetLever)
            {
                //���� ������ + effect ����� + ������ �ִϸ��̼� �۵�
                leverBody.IsGateOpen(true);
                InfoDisappear();
                
            }
            else
            {
                PlaySound("FAIL");
                useText.gameObject.SetActive(true);

            }
        }

    }


    private void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "Item" || hitInfo.transform.tag == "LeverBody" || hitInfo.transform.tag == "Car")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        if(hitInfo.transform.tag == "LeverBody")
        {
            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + "(E)" + "</color>";
        }
        else if(hitInfo.transform.tag == "Car")
        {
            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + "(E)" + "</color>";
        }
        else
        {
            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + "(E)" + "</color>";
        }
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
        useText.gameObject.SetActive(false);
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "GETITEM":
                audioSource.clip = getItemSound;
                break;
            case "FAIL":
                audioSource.clip = failSound;
                break;
        }
        audioSource.Play();
    }

}
