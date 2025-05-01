using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    // Change to a scene by name
    public static void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Change to a scene by build index
    public static void ChangeSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
