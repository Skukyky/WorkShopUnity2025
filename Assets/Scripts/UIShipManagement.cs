using TMPro;
using UnityEngine;

public class UIShipManagement : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI valueText;
    public RefullSpaceShip ship;


    private void Update()
    {
        valueText.text = "Energy recovery:  " + ship._numberOfEnergy + "/4.";
    }
}
