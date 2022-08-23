using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionController : MonoBehaviour
{

    [SerializeField]
    private float range;
    private bool pickupActivated = false;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private TextMeshProUGUI actionText;

    [SerializeField]
    private Inventory inventory;

    public AudioClip getItemSound;
    AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        actionText.gameObject.SetActive(false);
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
            CanPickUp();
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

            }
        }
    }


    private void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "Item")
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
        Debug.Log("³ªÅ¸³²");
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + "(E)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "GETITEM":
                audioSource.clip = getItemSound;
                break;
        }
        audioSource.Play();
    }

}
