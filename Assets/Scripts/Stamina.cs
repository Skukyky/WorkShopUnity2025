using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public PlayerFPS player;
    public Slider slider;
    private float overHeat;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        overHeat = player.stamina/100;
        slider.value = overHeat;
    }
}