using Unity.VisualScripting;
using UnityEngine;

public class recupItem : MonoBehaviour
{
    public inventory inventory;
    public void recupItems()
    {
        print("recupItems");
        inventory.Energize();
        ParticleSystem[] Obj = GetComponentsInChildren<ParticleSystem>();
        if (inventory.items < 2)
        {
            foreach (ParticleSystem O in Obj)
            {
                Destroy(O);
            }
        }
    }
}
