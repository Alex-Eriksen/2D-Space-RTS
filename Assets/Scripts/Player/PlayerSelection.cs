using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelection : MonoBehaviour
{
    //public HashSet<Entity> selected = new HashSet<Entity>();
    //public List<Entity> availableEntities = new List<Entity>();

    private Vector2 m_mousePosition, m_dragBoxStart;
    private PlayerController m_playerController;
    private bool m_isDragging = false;
    private Camera m_camera;

    [SerializeField] private GameObject m_selectionObject;
    [SerializeField] private LayerMask m_entityLayer;

    private RectTransform m_selectionTransform;

    private void Awake()
    {
        m_playerController = PlayerController.Instance;
        m_selectionTransform = m_selectionObject.GetComponent<RectTransform>();
        m_camera = Camera.main;
    }

    private void Start()
    {
        m_playerController.OnDragSelect.started += DragSelectStarted;
        m_playerController.OnDragSelect.canceled += DragSelectCanceled;
        m_playerController.OnMousePosition.performed += MousePosition;
        m_playerController.OnLeftMouse.performed += LeftMouseButton;
        m_playerController.OnMultiSelect.performed += Multiselect;
    }

    private void Update()
    {
        //SelectEntities();
        //UpdateSelectionBox();
    }

    //public bool IsSelected(Entity entity)
    //{
    //    return selected.Contains(entity);
    //}

    public void Select(/*Entity entity*/)
    {
        //selected.Add(entity);
        //entity.OnSelected();
    }

    public void Deselect(/*Entity entity*/)
    {
        //entity.OnDeselected();
        //selected.Remove(entity);
    }

    public void DeselectAll()
    {
        //foreach (Entity entity in selected)
        //{
        //    entity.OnDeselected();
        //}
        //selected.Clear();
    }

    private void SelectEntities()
    {
        if (!m_isDragging)
        {
            return;
        }
    }

    private void UpdateSelectionBox()
    {
        if (!m_isDragging)
        {
            m_selectionObject.SetActive(false);
            return;
        }

        m_selectionObject.SetActive(true);

        float width = m_mousePosition.x - m_dragBoxStart.x;
        float height = m_mousePosition.y - m_dragBoxStart.y;

        m_selectionTransform.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        m_selectionTransform.anchoredPosition = m_dragBoxStart + new Vector2(width / 2, height / 2);

        Bounds bounds = new Bounds(m_selectionTransform.anchoredPosition, m_selectionTransform.sizeDelta);
        //for (int i = 0; i < availableEntities.Count; i++)
        //{
        //    if(IsEntityInSelectionBox(m_camera.WorldToScreenPoint(availableEntities[ i ].transform.position), bounds))
        //    {
        //        Select(availableEntities[ i ]);
        //    }
        //    else
        //    {
        //        Deselect(availableEntities[ i ]);
        //    }
        //}

    }

    private bool IsEntityInSelectionBox(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x && position.y > bounds.min.y && position.y < bounds.max.y;
    }

    public void LeftMouseButton(InputAction.CallbackContext context)
    {
        SelectionMiddleware();
    }

    public void Multiselect(InputAction.CallbackContext context)
    {
        SelectionMiddleware(true);
    }

    private void SelectionMiddleware(bool multiSelect = false)
    {
        //if (Physics.Raycast(m_camera.ScreenPointToRay(m_mousePosition), out RaycastHit hitInfo, m_entityLayer) && hitInfo.collider.TryGetComponent<Entity>(out Entity entity))
        //{
        //    if (multiSelect)
        //    {
        //        //if (IsSelected(entity))
        //        //{
        //        //    Deselect(entity);
        //        //}
        //        //else
        //        //{
        //        //    Select(entity);
        //        //}
        //    }
        //    else
        //    {
        //        DeselectAll();
        //        Select(entity);
        //    }
        //}
    }

    public void DragSelectStarted(InputAction.CallbackContext context)
    {
        m_isDragging = true;
        m_dragBoxStart = m_mousePosition;
    }    

    public void DragSelectCanceled(InputAction.CallbackContext context)
    {
        m_isDragging = false;
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        m_mousePosition = context.ReadValue<Vector2>();
    }
}
