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
            RotateToTarget(player.transform);
            return false;
        }

        return true;
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