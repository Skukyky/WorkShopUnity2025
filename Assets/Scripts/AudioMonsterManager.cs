using UnityEngine;
using System.Collections;
public class AudioMonsterManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] idleSound;
    
    void Start()
    {
        // Récupère l'AudioSource automatiquement
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator Sound(float delay)
    {
        yield return new WaitForSeconds(delay);
        int indexAleatoire = Random.Range(0, idleSound.Length);
        audioSource.PlayOneShot(idleSound[indexAleatoire]);
        StartCoroutine(Sound(5.0f));
    }
    
}
