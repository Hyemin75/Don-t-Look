using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneAngel : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.isGameRestart)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(GameManager.Instance.IsGameStart)
        {
            gameObject.SetActive(false);
        }
    }

}
