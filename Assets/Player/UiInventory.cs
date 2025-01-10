using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UiInventory : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI valueText;

    void Start()
    {
        value = 0;
    }

    private void Update()
    {
        valueText.text = value + "/2";
    }
}
