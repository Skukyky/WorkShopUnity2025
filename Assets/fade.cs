using UnityEngine;
using UnityEngine.UI; // Utilisation correcte pour UI classique

public class Fade : MonoBehaviour
{
    private Image image; // Image du composant
    private Color color; // Couleur de l'image

    // Start est appelé une fois avant la première exécution de Update
    void Start()
    {
        image = GetComponent<Image>(); // Récupération de l'image
        if (image != null)
        {
            color = image.color; // Initialisation de la couleur
        }
        else
        {
            Debug.LogError("Aucun composant Image trouvé sur cet objet !");
        }
    }

    // Update est appelé une fois par frame
    void Update()
    {
        if (image != null)
        {
            // Augmentation de l'alpha avec une limite
            color.a = Mathf.Clamp(color.a + Time.deltaTime * 0.3f, 0f, 1f);
            image.color = color; // Réaffectation de la couleur à l'image
        }
    }
}
