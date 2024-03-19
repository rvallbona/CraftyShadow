using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 2f;
    [SerializeField] private float alertDuration = 3f;
    private float alertTimer = 0f;

    private bool isRotatingRight = true;
    private float rotationTimer = 0f;
    private float rotationDuration = 2f;

    [SerializeField] private GameObject[] lightController;
    private NavMeshAgent agent;

    private enum State { Patrol, Alert, Chase }
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
            case State.Alert:
                Alert();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }
    void Patrol()
    {
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
    void Alert()
    {
        Debug.Log("Alert");
        RotateAgentPeriodically();

        alertTimer += Time.deltaTime; // +Temp

        // Check for player detection
        if (PlayerDetected())
        {
            currentState = State.Chase;
        }

        // Check if the alert duration has passed
        if (alertTimer >= alertDuration)
        {
            currentState = State.Patrol;
            alertTimer = 0f; // Reset timer
        }
    }
    void Chase()
    {
        Debug.Log("Chase");
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

        // Check if player is out of chase distance
        if (!PlayerDetected())
        {
            currentState = State.Alert;
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
    void RotateAgentPeriodically()
    {
        rotationTimer += Time.deltaTime;

        if (rotationTimer >= rotationDuration)
        {
            // Change direction rotate
            isRotatingRight = !isRotatingRight;
            rotationTimer = 0f;

            // Apply rotation
            if (isRotatingRight)
            {
                RotateAgent(180f);  //  Rotate 180g right
            }
            else
            {
                RotateAgent(-180f);  // Rotate 180g left
            }
        }
    }
    void RotateAgent(float angle)
    {
        agent.transform.Rotate(Vector3.up, angle);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level1");
        }
    }
}