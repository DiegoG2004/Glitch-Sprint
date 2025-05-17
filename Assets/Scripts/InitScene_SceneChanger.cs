using System.Collections;
using UnityEngine;

public class InitScene_SceneChanger : MonoBehaviour
{
    //Boolean to wait until the initializations are complete
    public bool IsAndroidInitialized { get; set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        while (!IsAndroidInitialized)
        {
            yield return null;
        }

        MySceneManagement.LoadScene(MySceneManagement.SceneNames.MAIN_MENU_SCENE);
    }
}
