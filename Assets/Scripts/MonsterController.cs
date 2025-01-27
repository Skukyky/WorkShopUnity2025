using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public GameObject player;
    public MeleeWeapon meleeWeapon;
    NavMeshAgent navMeshAgent;
    Animator animator;
    private bool inBed = true;
    
    private AudioMonsterManager audioMonsterManager;

    // Actions possibles
    const string TAKE_DAMAGE_STATE = "Take Damage";
    public const string DEFEATED_STATE = "Die";
    public const string WALK_STATE = "Crawl Forward Fast";
    public const string ATTACK_STATE = "Stomp Attack";
    public string currentAction;
    private bool attacking = false;
    private float time;
    
    //Scanner pour trouver des cibles
    public TargetScanner targetScanner;
 
    //Cible
    public GameObject currentTarget;
 
    //Temps avant de perdre la cible
    public float delayLostTarget = 10f;
 
    private float timeLostTarget = 0;
    
    private Vector3 startLocation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerFPS>().gameObject;
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        // Attendre pendant le délai
        yield return new WaitForSeconds(delay);

        meleeWeapon.StartAttack();
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

    private void Start()
    {
        startLocation = transform.position;
        audioMonsterManager = GetComponent<AudioMonsterManager>();
        audioMonsterManager.Sound();
    }

    private void Update()
    {
        if (currentAction == DEFEATED_STATE || player == null)
        {
            navMeshAgent.ResetPath();
            animator.SetBool(WALK_STATE, false);
            return;
        }
        
        FindingTarget();

        if (currentAction == TAKE_DAMAGE_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }
  
        if (inBed == false && currentTarget == null)
        {
            if (navMeshAgent.destination != startLocation)
            {
                navMeshAgent.SetDestination(startLocation);
                animator.SetBool(WALK_STATE, true);
            }
    
            if (navMeshAgent.remainingDistance < 2.0 )
            {
                animator.SetBool(WALK_STATE, false);
                inBed = true;
            }
            return;
        }
        //Détection
        
        //Est-ce que l'IA se déplace vers le joueur ?
        if (MovingToTarget())
        {
            attacking = false;
            animator.SetBool(WALK_STATE, true);
            return;
            //En train de marcher
        } 
        animator.SetBool(WALK_STATE, false);
        

        //Attaque
        if (attacking && Time.time > time && Vector3.Distance(transform.position, currentTarget.transform.position) < 3.0f )
        {
            time = Time.time + 1.7f;
            Attack();
        }
    }
    

    public void TakeDamage()
    {
        audioMonsterManager.dieSound();
        SetActionState(TAKE_DAMAGE_STATE);
        StartCoroutine(MovingAfterDelay(1.0f));
    }

    public void Defeated()
    {
        audioMonsterManager.dieSound();
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
        if (currentTarget != null)
        {
            navMeshAgent.SetDestination(currentTarget.transform.position);

            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                if (currentAction != WALK_STATE)
                {
                    Walk();
                }

                return true;
            }
            
            attacking = true;
            RotateToTarget(currentTarget.transform);
        }
        return false;
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
                inBed = false;
                timeLostTarget = 0;
            }
            else return; 
        } 
        animator.SetBool(WALK_STATE, false);
        currentTarget = null;
    }
    
    private void Walk()
    {
        SetActionState(WALK_STATE);
    }

    private void Attack()
    {
        audioMonsterManager.attackSound();
        SetActionState(ATTACK_STATE);
        
        StartCoroutine(ExecuteAfterDelay(0.5f));
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