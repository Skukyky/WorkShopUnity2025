using UnityEngine;

public class RefullSpaceShip : MonoBehaviour
{
    private inventory _inventory;
    private float _numberofenergy;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void AddEnergy(GameObject player)
    {
        _inventory = player.GetComponent<inventory>();
        _numberofenergy += _inventory.items;
        _inventory.items = 0;
        print(_numberofenergy);
    }
    
    
}
