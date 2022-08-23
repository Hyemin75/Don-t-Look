using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public Item item;
    public bool isUsing;
    public Image itemImage;
    public Image usingImage; // 아이템 선택시 나타나는 이미지
    
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;   
        itemImage.color = color;
    }


    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.itemImage;
        isUsing = false;

        SetColor(1);

    }

    public void CheckUseAll()
    {
        if(isUsing == true)
        {
            ClearSlot();
        }
    }

    private void ClearSlot()
    {
        item = null; 
        isUsing = false;
        itemImage.sprite=null;
        SetColor(0);
    }
}
