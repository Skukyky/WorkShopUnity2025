using UnityEngine;

public class Killzone : MonoBehaviour
{
    public LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }
        Destroy(other.gameObject);
    }
}
