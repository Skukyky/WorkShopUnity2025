using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public PlayableDirector timeline;
    public bool autoActivate;
    public void QuitGame()
    {
        Application.Quit();
        print("Quit");
    }
    
    public void PlayGame()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        timeline.Play();
    }

    private void Start()
    {
        if (autoActivate)
        {
            timeline.Play();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Update()
    {
        if (timeline.time >= timeline.duration-0.1)
        {
            timeline.Stop();
            if (SceneManager.GetActiveScene().buildIndex == 3) SceneManager.LoadScene(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
