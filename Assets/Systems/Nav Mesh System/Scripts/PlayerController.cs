using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent), typeof(PlayerInput), typeof(LineRenderer))]
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;
    private PlayerInput playerInput;
    private Vector2 mousePosition = Vector2.zero;
    [SerializeField]
    private GameObject clickMarkerPrefab;
    private GameObject clickMarker;
    [SerializeField]
    private float clickMarkerVanishingDistance;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerInput = GetComponent<PlayerInput>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    private void OnEnable()
    {
        playerInput.actions.FindAction("Point").performed += OnPoint;
        playerInput.actions.FindAction("Left Click").performed += OnLeftClick;
        playerInput.actions.FindAction("Right Click").performed += OnRightClick;
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance <= clickMarkerVanishingDistance)
        {
            if (clickMarker)
            {
                Destroy(clickMarker.gameObject);
                lineRenderer.positionCount = 0;
                lineRenderer.enabled = false;
            }
        }
        else
        {
            DrawPath();
        }
    }

    private void OnPoint(InputAction.CallbackContext obj)
    {
        mousePosition = obj.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        playerInput.actions.FindAction("Point").performed -= OnPoint;
        playerInput.actions.FindAction("Left Click").performed -= OnLeftClick;
        playerInput.actions.FindAction("Right Click").performed -= OnRightClick;
    }

    private void OnLeftClick(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit hit))
        {
            SetDestination(hit.point);
        }
    }

    private void OnRightClick(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit hit))
        {
            SetDestination(hit.point);
        }
    }

    private void SetDestination(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
        if (!clickMarker)
            clickMarker = Instantiate(clickMarkerPrefab);
        clickMarker.SetActive(true);
        clickMarker.transform.position = target;
        DrawPath();
        lineRenderer.enabled = true;
    }

    private void DrawPath()
    {
        int pathCorners = navMeshAgent.path.corners.Length;
        lineRenderer.positionCount = pathCorners;
        lineRenderer.SetPosition(0, transform.position);

        if (navMeshAgent.path.corners.Length < 2)
            return;

        for (int i = 1; i < pathCorners; i++)
        {
            lineRenderer.SetPosition(i, navMeshAgent.path.corners[i]);
        }
    }
}
