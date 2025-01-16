using UnityEngine;
using UnityEngine.UI; // Utilisation correcte pour UI classique

public class Fade : MonoBehaviour
{
    private Image image; // Image du composant
    private Color color; // Couleur de l'image
    private float fadeDirection = 1;
    public bool fadeIn;
    public float fadeSpeed;

    // Start est appelé une fois avant la première exécution de Update
    void Start()
    {
        image = GetComponent<Image>(); // Récupération de l'image
        if (image != null)
        {
            color = image.color; // Initialisation de la couleur
        }

        if (fadeIn)
        {
            fadeDirection = -1;
            color.a = 1;
        }
        else
        {
            fadeDirection = 1;
            color.a = 0;
        }
    }

    // Update est appelé une fois par frame
    void Update()
    {
        if (image != null)
        {
            // Augmentation de l'alpha avec une limite
            color.a = Mathf.Clamp(color.a + (Time.deltaTime * fadeSpeed)*fadeDirection, 0f, 1f);
            image.color = color; // Réaffectation de la couleur à l'image
        }
    }
}
