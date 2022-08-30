using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    GameObject MainSceneAngel;
    
    public void PushStartButton()
    {
        GameManager.Instance.isGameStart = true;
        HUD.Instance.OnGamePlayUI();
    }

    public void AppExit()
    {
        Application.Quit();
    }


}
