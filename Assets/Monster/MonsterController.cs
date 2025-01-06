using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
 
public class MonsterController : MonoBehaviour
{
    public GameObject player;
 
 
    //Agent de Navigation
    NavMeshAgent navMeshAgent;
 
 
    //Composants
    Animator animator;
  
    //Actions possibles
  
    //Stand ou Idle = attendre
    const string STAND_STATE = "Idle";
  
    //Reçoit des dommages
    const string TAKE_DAMAGE_STATE = "Take Damage";
  
    //Est vaincu
    public const string DEFEATED_STATE = "Die";
 
    //Est en train de marcher
    public const string WALK_STATE = "Crawl Forward Fast";
 
 
 
    //Mémorise l'action actuelle
    public string currentAction;
  
    private void Awake()
    {
        //Au départ, la créature attend en restant debout
        currentAction = STAND_STATE;
  
        //Référence vers l'Animator
        animator = GetComponent<Animator>();
 
        //Référence NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        //Référence de Player
        player = FindObjectOfType<PlayerFPS>().gameObject;
    }
  
    private void Update()
    {
 
        //si la créature est défaite
        //Elle ne peut rien faire d'autres
        if (currentAction == DEFEATED_STATE)
        {
            navMeshAgent.ResetPath();
            return;
        }
 
 
        //Si la créature reçoit des dommages:
        //Elle ne peut rien faire d'autres.
        //Cela servira quand on améliorera ce script.
        if (currentAction == TAKE_DAMAGE_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }
 
        if (player != null)
        {
            //Est-ce que l'IA se déplace vers le joueur ?
            if (MovingToTarget())
            {
                //En train de marcher
                return;
            }
            
        }
    }
  
    //La créature attend
    private void Stand()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Stand"
        currentAction = STAND_STATE;
        //Le paramètre "Stand" de l'animator = true
        animator.SetBool("Idle", true);
    }
  
    public void TakeDamage()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Damage"
        currentAction = TAKE_DAMAGE_STATE;
        //Le paramètre "Damage" de l'animator = true
        animator.SetBool("Take Damage", true);
    }
  
    public void Defeated()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Defeated"  
        currentAction = DEFEATED_STATE;
        //Le paramètre "Defeated" de l'animator = true
        animator.SetBool(DEFEATED_STATE, true);
    }
  
  
    //Permet de surveiller l'animation lorsque l'on prend un dégât
    private void TakingDamage()
    {
  
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(TAKE_DAMAGE_STATE))
        {
            //Compte le temps de l'animation
            //normalizedTime : temps écoulé nomralisé (de 0 à 1).
            //Si normalizedTime = 0 => C'est le début.
            //Si normalizedTime = 0.5 => C'est la moitié.
            //Si normalizedTime = 1 => C'est la fin.
  
  
            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
  
  
            //Fin de l'animation
            if (normalizedTime > 1)
            {
                Stand();
            }
  
        }
  
    }
 
 
    private bool MovingToTarget()
    {
         
        //Assigne la destination : le joueur
        navMeshAgent.SetDestination(player.transform.position);
 
         
        // navMeshAgent.remainingDistance = distance restante pour atteindre la cible (Player)
        // navMeshAgent.stoppingDistance = à quelle distance de la cible l'IA doit s'arrêter 
        // (exemple 2 m pour le corps à sorps) 
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
 
            if (currentAction != WALK_STATE)
                Walk();
 
        }
        else
        {
            //Si arrivé à bonne distance, regarde vers le joueur
            RotateToTarget(player.transform);
            return false;
        }
 
        return true;
    }
 
 
    //Walk = Marcher
    private void Walk()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Walk"
        currentAction = WALK_STATE;
        //Le paramètre "Walk" de l'animator = true
        animator.SetBool(WALK_STATE, true);
    }
 
    //Permet de tout le temps regarder en direction de la cible
    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }
 
    //Réinitialise les paramètres de l'animator
    private void ResetAnimation()
    {
        animator.SetBool(STAND_STATE, false);
        animator.SetBool(TAKE_DAMAGE_STATE, false);
        animator.SetBool(DEFEATED_STATE, false);
        animator.SetBool(WALK_STATE, false);
    }
  
}