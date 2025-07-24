using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Game Scene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
