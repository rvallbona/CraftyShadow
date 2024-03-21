using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 2f;
    [SerializeField] private float alertDuration = 10f;
    private float alertTimer = 0f;

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
                agent.SetDestination(lightController[i].gameObject.transform.position);
        }

        // Check for player detection
        if (PlayerDetected())
            currentState = State.Chase;
    }
    void Alert()
    {
        Debug.Log("Alert");

        alertTimer += Time.deltaTime; // +Temp
        RotateAgentPeriodically();

        // Check for player detection
        if (PlayerDetected())
            currentState = State.Chase;

        // Check if the alert duration has passed
        if (alertTimer >= alertDuration)
        {
            currentState = State.Patrol;
            alertTimer = 0f; // Reset timer
        }
    }
    void RotateAgentPeriodically()
    {
        agent.transform.Rotate(.2f * Vector3.up, Space.World);
    }
    void Chase()
    {
        Debug.Log("Chase");
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

        if (!PlayerDetected())
            currentState = State.Alert;
    }
    bool PlayerDetected()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, chaseDistance))
                if (hit.collider.CompareTag("Player")) { return true; }
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