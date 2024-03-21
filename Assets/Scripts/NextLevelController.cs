using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    [SerializeField] private string levelName;
    private void OnTriggerEnter(Collider other) { SceneManager.LoadScene(levelName); }
}
