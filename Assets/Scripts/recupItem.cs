using Unity.VisualScripting;
using UnityEngine;

public class recupItem : MonoBehaviour
{
    public inventory inventory;
    private bool canRecupItem = true;
    
    public void recupItems()
    {
        print("recupItems");
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
