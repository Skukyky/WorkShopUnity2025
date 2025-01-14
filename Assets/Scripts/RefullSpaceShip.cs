using UnityEngine;
using UnityEngine.SceneManagement;

public class RefullSpaceShip : MonoBehaviour
{
    private inventory _inventory;
    public int _numberOfEnergy;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void AddEnergy(GameObject player)
    {
        _inventory = player.GetComponent<inventory>();
        _numberOfEnergy += _inventory.items;
        _inventory.items = 0;
        print(_numberOfEnergy);
        if (_numberOfEnergy == 4)
        {
            Quit();
        }
    }

    private void Quit()
    {
        Application.Quit();
        print("Quit");
    }
    
}
