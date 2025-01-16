using UnityEngine;

public class AudioInteractionmanager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip levier;
    public AudioClip cristal;
    public AudioClip interact;
    
    
    void Start()
    {
        // Récupère l'AudioSource automatiquement
        audioSource = GetComponent<AudioSource>();
    }

    public void useLeviert()
    {
        // Joue le son du tir
        audioSource.PlayOneShot(levier);
    }

    public void useCristal()
    {
        audioSource.PlayOneShot(cristal);
    }

    public void useInteract()
    {
        audioSource.PlayOneShot(interact);
    }
    
    
}
