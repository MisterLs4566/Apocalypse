using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    
    private static BackgroundMusicPlayer instance;
    private void OnEnable()

    {

        TitleUIManager.GameStarted += Destroy;

    }

    private void OnDisable()
    {

        TitleUIManager.GameStarted -= Destroy;

    }

    
    void Start()
    {

        if(instance != this && instance != null)
        {

            Destroy(gameObject);

        }
        else
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        
    }

    void Destroy()
    {

        Destroy(gameObject);

    }

}
