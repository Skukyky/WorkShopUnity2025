using UnityEngine;

public class recupItem : MonoBehaviour
{
    private inventory inventory;
    
    void recupItems()
    {
        inventory.Energize();
        Destroy(gameObject);
    }
}
