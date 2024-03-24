using UnityEngine;
public class LaserController : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    [SerializeField] private Color rayColor;
    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, raycastDistance))
            Gizmos.DrawLine(origin, hit.point);
        else
            Gizmos.DrawRay(origin, direction * raycastDistance);
    }
}
