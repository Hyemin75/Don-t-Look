using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    GameObject MainSceneAngel;
    
    public void PushStartButton()
    {
        HUD.Instance.OnGamePlayUI();
        Destroy(MainSceneAngel);   
    }

    public void AppExit()
    {
        Application.Quit();
    }


}
