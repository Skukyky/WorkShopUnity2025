using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip gunShot;
    public AudioClip refresh;
    
    void Start()
    {
        // Récupère l'AudioSource automatiquement
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGunShot()
    {
        // Joue le son du tir
        audioSource.PlayOneShot(gunShot);
    }

    public void PlayReload()
    {
        // Joue le son du rechargement
        audioSource.PlayOneShot(refresh);
    }
}
