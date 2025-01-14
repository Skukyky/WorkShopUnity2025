using Unity.VisualScripting;
using UnityEngine;

public class recupItem : MonoBehaviour
{
    public inventory inventory;
    [HideInInspector]
    public bool canRecupItem = false;
    
    public void recupItems()
    {
        if (inventory.items < 2 && canRecupItem)
        {
            ParticleSystem[] Obj = GetComponentsInChildren<ParticleSystem>();
            canRecupItem = false;
            inventory.Energize();
            print(inventory.items);
            foreach (ParticleSystem O in Obj)
            {
                Destroy(O);
            }
        }
    }
}
