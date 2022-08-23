using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static PlayerController m_instance;
    public static PlayerController Instance { get { return m_instance; } }

    public PlayerInput PlayerInput;

    public InputAction OnPan { get { return PlayerInput.actions[ "Pan" ]; } }
    public InputAction OnMousePosition { get { return PlayerInput.actions[ "MousePosition" ]; } }
    public InputAction OnMouseDelta { get { return PlayerInput.actions[ "MouseDelta" ]; } }
    public InputAction OnRightMouse { get { return PlayerInput.actions[ "RightMouse" ]; } }
    public InputAction OnLeftMouse { get { return PlayerInput.actions[ "LeftMouse" ]; } }
    public InputAction OnMiddleMouse { get { return PlayerInput.actions[ "MiddleMouse" ]; } }
    public InputAction OnScroll { get { return PlayerInput.actions[ "Scroll" ]; } }
    public InputAction OnDragSelect { get { return PlayerInput.actions[ "DragSelect" ]; } }
    public InputAction OnMultiSelect { get { return PlayerInput.actions[ "MultiSelect" ]; } }
    public InputAction OnEscape { get { return PlayerInput.actions[ "Escape" ]; } }

    private void Awake()
    {
        if(m_instance != null && m_instance != this)
        {
            Destroy(this);
        }
        else
        {
            m_instance = this;
        }

        PlayerInput = GetComponent<PlayerInput>();
    }
}
