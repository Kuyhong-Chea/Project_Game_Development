using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public Transform player;
    public Transform[] waypoints;
    public float seeDistance = 8f;
    public float roamSpeed = 1.5f;
    public float chaseSpeed = 2.3f;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToRandomWaypoint();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.speed = roamSpeed;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToRandomWaypoint();
            }
        }
    }

    void GoToRandomWaypoint()
    {
        int r = Random.Range(0, waypoints.Length);
        agent.SetDestination(waypoints[r].position);
    }

    bool CanSeePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > seeDistance) return false;

        Vector3 ghostEyes = transform.position + Vector3.up;
        Vector3 playerBody = player.position + Vector3.up;

        if (Physics.Linecast(ghostEyes, playerBody, out RaycastHit hit, ~0, QueryTriggerInteraction.Ignore))
        {
            return hit.transform == player;
        }
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().PlayerCaught();
        }
    }
}