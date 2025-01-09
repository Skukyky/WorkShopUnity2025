using Unity.VisualScripting;
using UnityEngine;

public class recupItem : MonoBehaviour
{
    public inventory inventory;
    public void recupItems()
    {
        print("recupItems");
        ParticleSystem[] Obj = GetComponentsInChildren<ParticleSystem>();
        if (inventory.items < 2)
        {
            inventory.Energize();
            print(inventory.items);
            foreach (ParticleSystem O in Obj)
            {
                Destroy(O);
            }
        }
    }
}
