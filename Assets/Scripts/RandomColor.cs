using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    public Material[] randomMaterials;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        
        // Choix du matériau
        Material randomMaterial = randomMaterials[Random.Range(0, randomMaterials.Length)];
        
        // Assignation du matériau au mesh
        renderer.material = randomMaterial;
    }
}