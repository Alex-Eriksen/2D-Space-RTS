using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Items/New Database" )]
public class DatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    private static DatabaseObject m_instance;
    public static DatabaseObject Instance => m_instance;

    [SerializeField] private ItemObject[] m_itemObjects;
    
    public void OnAfterDeserialize()
    {
        if(m_instance != null && m_instance != this)
        {
            Destroy( this );
        }
        else
        {
            m_instance = this;
        }
    }

    public void OnBeforeSerialize()
    {
        if(m_itemObjects.Length == 0)
        {
            return;
        }

        for (int i = 0; i < m_itemObjects.Length; i++)
        {
            m_itemObjects[ i ].itemID = i;
        }
    }

    public ItemObject GetItemByID( int id )
    {
        foreach (ItemObject item in m_itemObjects)
        {
            if(item.itemID == id)
            {
                return item;
            }
        }

        return null;
    }

    public ItemObject GetItemByName( string itemName )
    {
        foreach (ItemObject item in m_itemObjects)
        {
            if(item.itemName != itemName)
            {
                continue;
            }

            return item;
        }

        return null;
    }

    public int GetIndexOf( ItemObject item )
    {
        for (int i = 0; i < m_itemObjects.Length; i++)
        {
            if (item == m_itemObjects[ i ])
            {
                return i;
            }
        }
        return -1;
    }
}
