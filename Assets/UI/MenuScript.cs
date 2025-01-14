using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        print("Quit");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        print("Play Game");
    }
}
