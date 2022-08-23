using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraLayerManager : MonoBehaviour
{
    private static CameraLayerManager m_instance;
    public static CameraLayerManager Instance { get { return m_instance; } }

    public Dictionary<Camera, int> LayersUsed = new Dictionary<Camera, int>();

    [SerializeField] private Color m_backgroundColor;

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

    public int AddCamera( Camera camera )
    {
        int layer = 0;
        for(int i = 21; i < 31; i++)
        {
            if (LayersUsed.ContainsValue(i))
            {
                if(i == 30)
                {
                    return -1;
                }
                continue;
            }

            layer = i;
            break;
        }

        camera.backgroundColor = m_backgroundColor;
        camera.name = $"Camera (Layer:{layer})";
        camera.cullingMask = 1 << layer;
        camera.gameObject.layer = layer;
        LayersUsed.Add(camera, layer);
        return layer;
    }

    public void RemoveCamera( Camera camera )
    {
        LayersUsed.Remove(camera);
    }

    public void ClearCameras( Camera[] cameras )
    {
        foreach(Camera camera in cameras)
        {
            LayersUsed.Remove(camera);
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(CameraLayerManager))]
public class CameraLayerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var obj = (CameraLayerManager) target;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Layers Used Count");
        GUILayout.TextField(obj.LayersUsed.Count.ToString());
        GUILayout.EndHorizontal();
    }
}
#endif