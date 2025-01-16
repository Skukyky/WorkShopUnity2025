using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public PlayableDirector timeline;
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

    void Update()
    {
        if (timeline.time >= 29.9)
        {
            timeline.Stop();
            SceneManager.LoadScene(2);
        }
    }
}
