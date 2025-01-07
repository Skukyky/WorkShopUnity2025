using UnityEngine;
using UnityEngine.UI;

public class OverHeatingView : MonoBehaviour
{
    public ShootAction weapon;
    public Slider slider;
    private float overHeat;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        overHeat = weapon.overHeating / 100;
        slider.value = overHeat;
        print(overHeat);
    }
}
