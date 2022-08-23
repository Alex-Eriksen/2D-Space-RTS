using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Crafting/New Building Database" )]
public class BuildingDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private BuildingObject[] m_buildingObjects;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        if (m_buildingObjects.Length == 0)
        {
            return;
        }

        for (int i = 0; i < m_buildingObjects.Length; i++)
        {
            m_buildingObjects[ i ].buildingID = i;
        }
    }

    public BuildingObject GetItemByID( int id )
    {
        foreach (BuildingObject building in m_buildingObjects)
        {
            if (building.buildingID == id)
            {
                return building;
            }
        }

        return null;
    }

    public BuildingObject GetItemByName( string buildingName )
    {
        foreach (BuildingObject building in m_buildingObjects)
        {
            if (building.buildingName != buildingName)
            {
                continue;
            }

            return building;
        }

        return null;
    }

    public int GetIndexOf( BuildingObject building )
    {
        for (int i = 0; i < m_buildingObjects.Length; i++)
        {
            if (building == m_buildingObjects[ i ])
            {
                return i;
            }
        }
        return -1;
    }
}
