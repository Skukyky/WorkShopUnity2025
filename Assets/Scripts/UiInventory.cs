using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UiInventory : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI valueText;
    public inventory inventory;


    private void Update()
    {
        valueText.text = "energy on yourself: " + inventory.items + "/2.";
    }

}
