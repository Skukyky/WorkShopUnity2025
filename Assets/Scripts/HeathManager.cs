using System;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class HeathManager : MonoBehaviour
{
    public receiveDamagePlayer receiveDamagePlayer;
    public Slider slider;

    private void Update()
    {
            slider.value = receiveDamagePlayer.hitPoint / 100f;
    }
}
