using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader
{
    private const string SceneName = "MainMenu";
    private const string InstanceScene = "PreLoader";
    
    [RuntimeInitializeOnLoadMethod]
    private static void LoadGame()
    {
        ServiceLocator.Clear();
        SceneManager.LoadScene(SceneName);
        SceneManager.LoadScene(InstanceScene, LoadSceneMode.Additive);
    }

    public static void Reload()
    {
        LoadGame();
    }
}
