using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Level1");
    }
}
