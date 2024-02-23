using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadNextScene : MonoBehaviour
{
    public void loadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 2;
        Debug.Log("Enter");
    }
}
