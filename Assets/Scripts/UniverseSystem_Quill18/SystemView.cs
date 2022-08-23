using Starlight;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemView : MonoBehaviour
{
    public StarSystem starSystem;
    public GameObject starSystem3DContainer;
    private Camera m_systemCamera, m_uiCamera;

    public event Action<float> ZoomEvent;

    public float zoomSpeed = 1.0f;

    public float minZoom = 0.5f;
    public float maxZoom = 30.0f;

    private float m_zoomAxis;

    private Vector2 m_mousePosition;
    private Vector3 m_dragOrigin, m_panVector;
    private bool m_isDragging = false;

    private void Start()
    {
        m_systemCamera = transform.GetChild( 0 ).GetChild( 0 ).GetComponent<Camera>();
        m_uiCamera = m_systemCamera.transform.GetChild( 0 ).GetComponent<Camera>();

        int layer = CameraLayerManager.Instance.AddCamera( m_systemCamera );
        
        if (layer == -1)
        {
            Destroy( gameObject );
        }

        starSystem3DContainer.transform.SetParent( null );

        SpawnRenderables( layer );
    }

    private void OnDestroy()
    {
        Destroy( starSystem3DContainer );
        CameraLayerManager.Instance.RemoveCamera( m_systemCamera );
    }

    // TODO: OnEnable and OnDisable should be swapped out for something better
    // what that is, I don't know.
    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Unintialize();
    }

    private void LateUpdate()
    {
        if (!m_isDragging)
        {
            return;
        }

        m_panVector = m_systemCamera.ScreenToWorldPoint( m_mousePosition ) - m_systemCamera.transform.position;

        Vector3 moveVector = new Vector3( m_dragOrigin.x - m_panVector.x, m_dragOrigin.y - m_panVector.y, m_systemCamera.transform.position.z );
        m_systemCamera.transform.position = moveVector;
    }

    private void Initialize()
    {
        PlayerController.Instance.PlayerInput.SwitchCurrentActionMap( "System" );
        PlayerController.Instance.OnScroll.performed += Zoom;
        PlayerController.Instance.OnMiddleMouse.started += MiddleClickDown;
        PlayerController.Instance.OnMiddleMouse.canceled += MiddleClickUp;
        PlayerController.Instance.OnMousePosition.performed += MousePosition;
        starSystem3DContainer.SetActive( true );
    }
    private void Unintialize()
    {
        PlayerController.Instance.PlayerInput.SwitchCurrentActionMap( "System" );
        m_isDragging = false;
        starSystem3DContainer.SetActive( false );
        PlayerController.Instance.OnScroll.performed -= Zoom;
        PlayerController.Instance.OnMiddleMouse.started -= MiddleClickDown;
        PlayerController.Instance.OnMiddleMouse.canceled -= MiddleClickUp;
        PlayerController.Instance.OnMousePosition.performed -= MousePosition;
        PlayerController.Instance.PlayerInput.SwitchCurrentActionMap( "Galaxy" );
    }

    private void SpawnRenderables( int layer )
    {
        GameObject go;
        GameObject prefab;

        // Spawn our star.
        prefab = Resources.Load<GameObject>( starSystem.StarSystemGraphic.CloseUpPath );
        go = Instantiate( prefab, starSystem3DContainer.transform );
        go.transform.SetLayerRecursively( layer );
        go.transform.localPosition = Vector3.zero;
        var starGraphics = go.GetComponentInChildren<StarGraphics>();
        starGraphics.Initialize( starSystem.MainSequenceStar, m_systemCamera.orthographicSize * Config.GetFloat( "STAR_ORBIT_DISTANCE" ), ref ZoomEvent );

        // Spawn all the planets.
        float orbitDistance = 0.0f;
        for (int i = 0; i < starSystem.GetMaxPlanets(); i++)
        {
            orbitDistance += Config.GetFloat( "STAR_ORBIT_DISTANCE" );
            Planet planet = starSystem.GetPlanetAtIndex( i );

            if (planet == null)
            {
                continue;
            }

            // If we get here, we have a valid planet.
            prefab = Resources.Load<GameObject>( planet.PlanetGraphic.PlanetPath );
            go = Instantiate( prefab, starSystem3DContainer.transform );
            go.transform.SetLayerRecursively( layer, 3 );
            go.transform.localPosition =
                Quaternion.Euler( 0, 0, UnityEngine.Random.Range( 0, 359 ) ) *
                new Vector3( orbitDistance, 0, 0 );

            var obj = Instantiate( Resources.Load<GameObject>( "OrbitPath" ), starSystem3DContainer.transform );
            var orbit = obj.GetComponent<OrbitPathGraphic>();
            orbit.radius = orbitDistance;
            obj.layer = layer;
            orbit.GeneratePath();

            var clickPlanet = go.GetComponent<ClickablePlanet>();
            clickPlanet.planet = planet;
            clickPlanet.Initialize( m_systemCamera.orthographicSize * Config.GetFloat( "STAR_ORBIT_DISTANCE" ), ref ZoomEvent );

            var planetGraphics = go.GetComponentInChildren<PlanetGraphics>();
            planetGraphics.Initialize( planet, m_systemCamera.orthographicSize * Config.GetFloat( "STAR_ORBIT_DISTANCE" ), ref ZoomEvent );
        }

        GetComponentInChildren<RenderTextureRaycast>().ClickablePlanetLayerMask = 1 << layer;
    }

    public void MousePosition( InputAction.CallbackContext context ) => m_mousePosition = context.ReadValue<Vector2>();
    private void Zoom( InputAction.CallbackContext context )
    {
        m_zoomAxis = context.ReadValue<float>() / 120 * -1;
        m_systemCamera.orthographicSize += m_zoomAxis * (zoomSpeed + m_systemCamera.orthographicSize / zoomSpeed);
        m_systemCamera.orthographicSize = Mathf.Clamp( m_systemCamera.orthographicSize, minZoom, maxZoom );
        m_uiCamera.orthographicSize = m_systemCamera.orthographicSize;
        ZoomEvent?.Invoke( m_systemCamera.orthographicSize * Config.GetFloat( "STAR_ORBIT_DISTANCE" ) );
    }
    public void MiddleClickDown( InputAction.CallbackContext context )
    {
        m_isDragging = true;
        m_dragOrigin = m_systemCamera.ScreenToWorldPoint( m_mousePosition );
    }
    public void MiddleClickUp( InputAction.CallbackContext context )
    {
        m_isDragging = false;
    }
}
