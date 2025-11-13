using UnityEngine;
using UnityEngine.AI;

public class SimpleGhostController : MonoBehaviour
{
    [SerializeField] private Transform[] pivotPoints;
    [SerializeField] private NavMeshAgent agent;

    [Header("Raycasts")]
    [SerializeField] private Transform origin;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 direction;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Color detectTrue = Color.green;
    [SerializeField] private Color detectFalse = Color.red;

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Gradient detectGradientTrue;
    [SerializeField] private Gradient detectGradientFalse;

    private bool checkRaycast;
    private int _pivotCounts = -1;

    private void Start()
    {
        ChangeCurrentPivot();

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (CheckDestination())
        {
            ChangeCurrentPivot();
        }
    }

    private void FixedUpdate()
    {
        if (checkRaycast)
        {
            DrawRaycast();
        }
    }

    private void DrawRaycast()
    {
        RaycastHit hit;

        direction = (target.position - origin.position).normalized;

        Physics.Raycast(origin.position, direction, out hit, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Walls"))
            {
                Debug.DrawRay(origin.position, direction * hit.distance, detectFalse);

                lineRenderer.SetPosition(0, origin.position);
                lineRenderer.SetPosition(1, origin.position + direction * hit.distance);

                lineRenderer.colorGradient = detectGradientFalse;
                ReturneCurrentPivot();
            }
            else if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawRay(origin.position, direction * hit.distance, detectTrue);

                lineRenderer.SetPosition(0, origin.position);
                lineRenderer.SetPosition(1, origin.position + direction * hit.distance);

                lineRenderer.colorGradient = detectGradientTrue;
                UpdateDestination(target.position);
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //UpdateDestination(other.transform.position);

            checkRaycast = true;

            lineRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //ReturneCurrentPivot();

            checkRaycast = false;

            lineRenderer.enabled = false;
        }
    }

    private void ReturneCurrentPivot()
    {
        UpdateDestination(pivotPoints[_pivotCounts].position);
    }

    private void ChangeCurrentPivot()
    {
        _pivotCounts = (_pivotCounts + 1) % pivotPoints.Length;

        UpdateDestination(pivotPoints[_pivotCounts].position);
    }

    private void UpdateDestination(Vector3 newDestination)
    {
        agent.SetDestination(newDestination);
    }

    private bool CheckDestination()
    {
        return agent.remainingDistance < 0.5f;
    }

}
