using UnityEngine;
using System.Collections;
public class AudioMonsterManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] idleSound;
    public AudioClip attack;
    public AudioClip die;
    
    void Start()
    {
        // Récupère l'AudioSource automatiquement
        audioSource = GetComponent<AudioSource>();
    }

    public void Sound()
    {
        print("caca");
         int indexAleatoire = Random.Range(0, idleSound.Length);
        audioSource.PlayOneShot(idleSound[indexAleatoire]);
        StartCoroutine(relance(5.0f));
    }

    IEnumerator relance(float delay)
    {
        yield return new WaitForSeconds(delay);
        Sound();
    }

    public void attackSound()
    {
        audioSource.PlayOneShot(attack);
    }

    public void dieSound()
    {
        audioSource.PlayOneShot(die);
    }
}
