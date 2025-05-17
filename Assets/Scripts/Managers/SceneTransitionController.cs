using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    public IEnumerator Start()
    {
        //Get scene from the static load
        var target = MySceneManagement.TargetScene;

        //Wait a frame
        yield return 5;

        //Start loading target scene in the background
        AsyncOperation loadOp = SceneManager.LoadSceneAsync((int)target);
        loadOp.allowSceneActivation = false;

        //Monitor progress (from 0 to 0.9)
        while (loadOp.progress < 0.9f)
        {
            Debug.Log($"loading progress: {loadOp.progress * 100f:0.0}%");
        }

        Debug.Log("Target scene loaded. Activating...");

        //Allow the scene to change
        loadOp.allowSceneActivation = true;
        MySceneManagement.NotifyLoadFinished();
    }
}
