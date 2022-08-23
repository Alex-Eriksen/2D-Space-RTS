using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class ViewManager : MonoBehaviour
{
    private static ViewManager m_instance;
    public static ViewManager Instance { get { return m_instance; } }

    private PlayerController m_playerController;

    public GalaxyVisuals GalaxyVisuals;
    public PlanetView PlanetView;

    public Transform UIViewCanvasTransform;

    public GameObject UIViewPrefab;

    public List<UIView> UIViews = new List<UIView>();
    

    private void Awake()
    {
        m_playerController = PlayerController.Instance;
    }

    private void Start()
    {
        //m_playerController.PlayerInput.SwitchCurrentActionMap("System");
        //m_playerController.PlayerInput.actions[ "Escape" ].performed += Escape_performed;
        //m_playerController.PlayerInput.SwitchCurrentActionMap("Galaxy");
    }

    private void OnEnable()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this);
        }
        else
        {
            m_instance = this;
        }
    }

    private void Escape_performed(InputAction.CallbackContext context)
    {
        // Back out of view one step at a time.
        // If in galaxy view, open the game menu instead.

    }

    public void Open( GameObject viewContent, string tabName = "New Tab" )
    {
        UIView viewScript;
        if (UIViews.Count > 0)
        {
            viewScript = UIViews[0].GetComponent<UIView>();
        }
        else
        {
            var viewObj = Instantiate(UIViewPrefab, UIViewCanvasTransform);
            viewScript = viewObj.GetComponent<UIView>();
            UIViews.Add(viewScript);
        }
        viewScript.AddTab(tabName, viewContent);
    }

    public void Close ( UIView view )
    {
        UIViews.Remove(view);
        Destroy(view.gameObject);
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(ViewManager))]
public class ViewManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var obj = (ViewManager) target;

        if(GUILayout.Button("Open View"))
        {
            obj.Open(null);
        }
    }
}
#endif
