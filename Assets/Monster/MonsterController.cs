using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public GameObject player;
    public MeleeWeapon meleeWeapon;
    NavMeshAgent navMeshAgent;
    Animator animator;

    // Actions possibles
    const string TAKE_DAMAGE_STATE = "Take Damage";
    public const string DEFEATED_STATE = "Die";
    public const string WALK_STATE = "Crawl Forward Fast";
    public const string ATTACK_STATE = "Stomp Attack";

    public string currentAction;
    
    //Scanner pour trouver des cibles
    public TargetScanner targetScanner;
 
    //Cible
    public GameObject currentTarget;
 
    //Temps avant de perdre la cible
    public float delayLostTarget = 10f;
 
    private float timeLostTarget = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerFPS>().gameObject;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator MovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        MovingToTarget();
        Walk();
    }

    private void Update()
    {
        if (currentAction == DEFEATED_STATE)
        {
            navMeshAgent.ResetPath();
            return;
        }

        if (currentAction == TAKE_DAMAGE_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }

        if (player != null)
        {
            if (!MovingToTarget())
            {
                if (currentAction != ATTACK_STATE)
                {
                    Attack();
                }
            }
        }
        //Détection
        FindingTarget();
 
        //Si pas de cible, ne fait rien
        if (currentTarget == null)
        {
            //Defaut
            navMeshAgent.ResetPath();
            animator.SetBool(WALK_STATE, false);
            return;
        }
 
 
        //Est-ce que l'IA se déplace vers le joueur ?
        if (MovingToTarget())
        {
            //En train de marcher
            return;
        }
 
 
        //Attaque
        if (currentAction != ATTACK_STATE && currentAction != TAKE_DAMAGE_STATE)
        {
            Attack();
            return;
        }

        if (currentAction == ATTACK_STATE)
        {
            Attack();
            return;
        }
    }
    

    public void TakeDamage()
    {
        SetActionState(TAKE_DAMAGE_STATE);
        StartCoroutine(MovingAfterDelay(1.0f));
    }

    public void Defeated()
    {
        SetActionState(DEFEATED_STATE);
        StartCoroutine(DestroyAfterDelay(1.2f));
    }

    private void TakingDamage()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(TAKE_DAMAGE_STATE))
        {
            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }

    

    private bool MovingToTarget()
    {
        if (navMeshAgent.destination != player.transform.position)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }

        if (navMeshAgent.remainingDistance == 0)
            return true;

        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            if (currentAction != WALK_STATE)
                Walk();
        }
        else
        {
            RotateToTarget(currentTarget.transform);
            return false;
        }

        return true;
    }

    //Cherche une cible
    private void FindingTarget()
    {
        //Si le joueur est détecté
        if (targetScanner.Detect(transform, player))
        {
            currentTarget = player;
            timeLostTarget = 0;
            return;
        }
 
        //Si le joueur était détecté
        //Calcule le temps avant d'abandonner
        if (currentTarget != null)
        {
            timeLostTarget += Time.deltaTime;
 
            if (timeLostTarget > delayLostTarget)
            {
                timeLostTarget = 0;
                currentTarget = null;
            }
 
 
            return;
        }
 
 
        currentTarget = null;
 
    }
    
    private void Walk()
    {
        SetActionState(WALK_STATE);
    }

    private void Attack()
    {
        SetActionState(ATTACK_STATE);
    }

    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }

    private void ResetAnimation()
    {
        animator.SetBool(TAKE_DAMAGE_STATE, false);
        animator.SetBool(DEFEATED_STATE, false);
        animator.SetBool(WALK_STATE, false);
        animator.SetBool(ATTACK_STATE, false);
    }

    private void SetActionState(string actionState)
    {
        ResetAnimation(); // Réinitialise tous les booléens
        currentAction = actionState;

        if (actionState != "Idle")
        {
            animator.SetBool(actionState, true);
        }
    }
}