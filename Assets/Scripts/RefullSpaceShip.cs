using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class RefullSpaceShip : MonoBehaviour
{
    private inventory _inventory;
    private bool _isValid = false;
    private float _timer;
    public Fade timelineOut;
    public int _numberOfEnergy;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void AddEnergy(GameObject player)
    {
        _inventory = player.GetComponent<inventory>();
        _numberOfEnergy += _inventory.items;
        _inventory.items = 0;
        print(_numberOfEnergy);
        if (_numberOfEnergy == 4)
        {
            Quit();
        }
    }

    private void Quit()
    {
        timelineOut.gameObject.SetActive(true);
        _isValid = true;
        _timer = Time.time + 3;
    }

    private void Update()
    {
        if (_isValid && Time.time > _timer)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
