using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 15.0f;
    public float zoomSpeed = 1.0f;

    public float minZoom = 0.5f;
    public float maxZoom = 30.0f;

    private Camera m_camera;
    private Camera m_ppCamera;
    private PlayerController m_playerController;
    private Vector2 m_panVector;
    private float m_zoomAxis;
    private Transform m_transform;

    private void Awake()
    {
        m_camera = GetComponent<Camera>();
        m_transform = transform;
        m_ppCamera = m_transform.GetChild(0).GetComponent<Camera>();
        m_playerController = PlayerController.Instance;
    }

    private void Start()
    {
        m_playerController.OnPan.performed += Pan;
        m_playerController.OnScroll.performed += Zoom;
    }

    private void LateUpdate()
    {
        m_transform.Translate(panSpeed * Time.deltaTime * m_panVector);
    }

    public void Pan(InputAction.CallbackContext context)
    {
        m_panVector = context.ReadValue<Vector2>();
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        m_zoomAxis = context.ReadValue<float>() / 120 * -1;
        m_camera.orthographicSize += m_zoomAxis * zoomSpeed;
        m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize, minZoom, maxZoom);
        m_ppCamera.orthographicSize = m_camera.orthographicSize;
    }
}
