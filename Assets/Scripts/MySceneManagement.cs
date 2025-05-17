using UnityEngine;
using UnityEngine.SceneManagement;

public static class MySceneManagement
{
    public enum SceneNames
    {
        NONE = -1,
        INIT_SCENE = 0,
        TRANSITION_SCENE = 1,
        MAIN_MENU_SCENE = 2,
        GAME_SCENE = 3
    }
    private static bool m_isLoadingScene = false;

    public static SceneNames TargetScene { get; private set; }
    public static void LoadScene(SceneNames nextScene)
    {
        if (m_isLoadingScene) return;

        m_isLoadingScene = true;
        TargetScene = nextScene;

        SceneManager.LoadScene((int)SceneNames.TRANSITION_SCENE);

    }

    public static void NotifyLoadFinished()
    {
        m_isLoadingScene = false;
    }
}
