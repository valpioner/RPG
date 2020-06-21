using UnityEngine;
using UnityEngine.AI;

// Controls the Enemy AI

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f; // Detection range for player
    Transform target;   // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent
    CharacterCombat combat;

    // Start is called before the first frame update
    // Use this for initialization
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            // Move towards the target
            agent.SetDestination(target.position);

            // IUf within attacking distance
            if (distance <= agent.stoppingDistance)
            {
                // Attack the target
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }

                // Face the target
                FaceTarget();
            }
        }
    }

    // Rotate to face the target
    void FaceTarget()
    {
        // Get direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Get Rotation where we point to that target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        
        // Update own rotation to point in that direction (Slar for smoothing)
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
