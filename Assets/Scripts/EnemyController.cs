using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private LightController[] lightController;
    [SerializeField] private Transform[] destTransform;
    [SerializeField] private float velocity = 5f;
    private void Update()
    {
        for (int i = 0; i < lightController.Length; i++)
        {
            if (lightController[i].isActive)
            {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(destTransform[i].position.x, gameObject.transform.position.y, destTransform[i].position.z), velocity * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("LOSE");
        }
    }
}