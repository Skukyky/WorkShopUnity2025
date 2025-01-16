using UnityEngine;
using UnityEngine.UI;

public class canvas : MonoBehaviour
{
    public GameObject image;
    public PlayerFPS Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            Player.enabled = false;
            Cursor.visible = true;
            image.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
