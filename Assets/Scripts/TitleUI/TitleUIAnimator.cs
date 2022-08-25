using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIAnimator : MonoBehaviour
{

    [SerializeField] private float maxHeightUp;
    [SerializeField] private float maxHeightDown;
    [SerializeField] private float zombTitleObjectMoveTime;
    [SerializeField] private GameObject zombTitleObject;
    private Vector2 currentPos;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentPos = new Vector2(0.0f, zombTitleObject.GetComponent<RectTransform>().anchoredPosition.y);
        Debug.Log(currentPos);
        Trigger();
        


    }

    public void StartGame()
    {
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
}
