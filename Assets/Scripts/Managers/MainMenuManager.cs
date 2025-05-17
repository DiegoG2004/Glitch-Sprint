using UnityEngine;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu References")]
    public GameObject m_MainMenu;
    public GameObject m_OptionsMenu;
    private void Start()
    {
        m_MainMenu.SetActive(true);
        m_OptionsMenu.SetActive(false);
    }
    public void PlayButton()
    {
        MySceneManagement.LoadScene(MySceneManagement.SceneNames.GAME_SCENE);
    }

    public void OptionsButton()
    {
        m_MainMenu.SetActive(false);
        m_OptionsMenu.SetActive(true);
    }

    public void ShopButton()
    {
        //m_MainMenu.SetActive(false);
        Debug.Log("Here's where i'd put a shop... if i had one!");
        Invoke("ShopToMainMenu", 2);
    }
    public void ExitButton()
    {
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
    
    public void OptionsToMainMenu()
    {
        m_OptionsMenu.SetActive(false);
        m_MainMenu.SetActive(true);
    }
    public void ShopToMainMenu()
    {
        Debug.Log("Have your menu back");
        m_MainMenu.SetActive(true);
    }
}
