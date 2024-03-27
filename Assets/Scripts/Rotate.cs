using UnityEngine;
public class Rotate : MonoBehaviour
{
    private void FixedUpdate()
    {
        this.gameObject.transform.Rotate(1 * Vector3.down, Space.World);
    }
}
