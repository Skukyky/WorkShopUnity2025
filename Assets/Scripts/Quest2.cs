using UnityEngine;

public class Game2 : MonoBehaviour
{
    private bool isValid;
    public OpenGate[] openGates;
    private AudioInteractionmanager audioInteractionmanager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Verify()
    {
        isValid = true;
        for (int i = 0; i < openGates.Length; i++)
        {
            if (!openGates[i].IsActive)
            {
                isValid = false;
            }
        }

        if (isValid)
        {
            GetComponent<recupItem>().canRecupItem = true;
            audioInteractionmanager.useCristal();
        }
    }
}
