using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 2f;
    [SerializeField] private GameObject[] lightController;
    private NavMeshAgent agent;

    private enum State { Patrol, Chase }
    private State currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrol;
    }
    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }
    void Patrol()
    {
        Debug.Log("Patrol");
        for (int i = 0; i < lightController.Length; i++)
        {
            if (lightController[i].gameObject.GetComponent<LightController>().isActive)
            {
                agent.SetDestination(lightController[i].gameObject.transform.position);
            }
        }

        // Check for player detection
        if (PlayerDetected())
        {
            currentState = State.Chase;
        }
    }
    void Chase()
    {
        Debug.Log("Chase");
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

        // Check if player is out of chase distance
        if (!PlayerDetected())
        {
            currentState = State.Patrol;
        }
    }
    bool PlayerDetected()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Raycast to check for obstacles between the enemy and the player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Player is in line of sight
                    return true;
                }
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level1");
        }
    }
}