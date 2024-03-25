using UnityEngine;
public class LaserController : MonoBehaviour
{
    [SerializeField] private float maxLength = 100f;
    private Transform startPoint;
    private LineRenderer lineRenderer;
    private RaycastHit hit;
    [SerializeField] private Color lineRendererColor;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); 
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        startPoint = this.transform;

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = lineRendererColor;
    }
    void Update()
    {
        Vector3 origin = startPoint.position;

        if (Physics.Raycast(origin, startPoint.forward, out hit, maxLength))
        {
            HitLogic();

            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Vector3 endPoint = origin + startPoint.forward * maxLength;
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
    private void HitLogic()
    {
        if (hit.collider.tag == "Player")
        {
            Debug.Log("player");
        }
    }
    private void OnDrawGizmos()
    {
        if (startPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint.position, startPoint.position + startPoint.forward * maxLength);
    }
}