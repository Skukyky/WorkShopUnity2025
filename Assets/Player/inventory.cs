using UnityEngine;

public class inventory : MonoBehaviour
{
    public int items;

    public void Energize()
    {
        if (items < 2)
        {
            items++;
            print(items);
            return;
        } 
        print("Inventory full");
    }
}
