using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class TitleUIManager : MonoBehaviour
{

    [SerializeField] private float maxHeightUp;
    [SerializeField] private float maxHeightDown;
    [SerializeField] private float zombTitleObjectMoveTime;
    [SerializeField] private GameObject zombTitleObject;



    public static event Action GameStarted;

    // Start is called before the first frame update
    void Start()
    {
        Trigger();
    }

    public void StartGame()
    {
        GameStarted?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    void Trigger()
    {
        StartCoroutine(MoveSequence());
    }

    IEnumerator MoveSequence()
    {
        LeanTween.moveLocal(zombTitleObject, new Vector2(0.0f, maxHeightDown), zombTitleObjectMoveTime).setIgnoreTimeScale(false).setLoopOnce();
        yield return new WaitForSeconds(zombTitleObjectMoveTime);
        LeanTween.moveLocal(zombTitleObject, new Vector2(0.0f, maxHeightUp), zombTitleObjectMoveTime).setIgnoreTimeScale(false).setOnComplete(Trigger).setLoopOnce();
        
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartSettings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }



}
