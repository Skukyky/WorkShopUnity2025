using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public PlayerFPS canvas;
    
    public void QuitGame()
    {
        Application.Quit();
        print("Quit");
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        print("Play Game");
    }
    
    public void ResumeGame()
    {
        
        Time.timeScale = 1;
        canvas.enabled = true;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}