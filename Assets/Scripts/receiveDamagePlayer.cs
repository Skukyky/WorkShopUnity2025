using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiveDamagePlayer : MonoBehaviour

{
    public Pause pause;
    PlayerFPS player;

//Maximum de points de vie
    public int maxHitPoint = 5;

//Points de vie actuels
    public int hitPoint = 0;

    public int healthRegen = 2;

//Après avoir reçu un dégât :
//La créature est invulnérable quelques instants
    public bool isInvulnerable;

//Temps d'invulnérabilité
    public float invulnerabiltyTime;

//Temps depuis le dernier dégât
    private float timeSinceLastHit = 0.0f;

    private PlayerFPS PlayerFPS;

    private void Start()
    {
//Au début : Points de vie actuels = Maximum de points de vie
        PlayerFPS = GetComponent<PlayerFPS>();
        isInvulnerable = false;
        StartCoroutine(RegenAfterDelay(1.0f));
        hitPoint = maxHitPoint;
        player = GetComponent<PlayerFPS>();
    }

    private void Update()
    {
//Si invulnérable
        if (isInvulnerable)
        {
//Compte le temps depuis le dernier dégât
//timeSinceLastHit = temps depuis le dernier dégât
//Time.deltaTime = temps écoulé depuis la dernière frame
            timeSinceLastHit += Time.deltaTime;

            if (timeSinceLastHit > invulnerabiltyTime)
            {
//Le temps est écoulé, il n'est plus invulnérable
                timeSinceLastHit = 0.0f;
                isInvulnerable = false;

            }
        }
    }

//Permet de recevoir des dommages
    public void GetDamage(int damage)
    {
        if (isInvulnerable)
            return;

        isInvulnerable = true;

//Applique les dommages aux points de vies actuels
        hitPoint -= damage;

//S'il reste des points de vie
        if (hitPoint <= 0)
        {
            player.enabled = false;
            Cursor.visible = true;
            pause.gameObject.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private IEnumerator RegenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hitPoint += healthRegen;
        hitPoint = Mathf.Clamp(hitPoint, 0, 100);
        StartCoroutine(RegenAfterDelay(2.0f));
    }
}
