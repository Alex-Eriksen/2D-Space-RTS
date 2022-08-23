using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Inventory
{
    private DatabaseObject m_database;
    public DatabaseObject Database => m_database;

    public Inventory( int inventorySize = 24 )
    {
        m_container = new InventorySlot[ inventorySize ];

        for (int i = 0; i < inventorySize; i++)
        {
            m_container[ i ] = new InventorySlot();
        }

        m_database = Resources.Load<DatabaseObject>( "Objects/Items/Item Database 01" );
    }

    private InventorySlot[] m_container;

    public InventorySlot[] GetSlots => m_container;

    public bool Contains( ItemObject item )
    {
        for (int i = 0; i < m_container.Length; i++)
        {
            if( m_container[i].itemID != item.itemID )
            {
                continue;
            }

            return true;
        }

        return false;
    }

    public int GetUsedSlotCount => m_container.Where( item => item.itemID != -1 ).Count();

    public InventorySlot GetFreeInventorySlot()
    {
        for (int i = 0; i < m_container.Length; i++ )
        {
            if( m_container[i].itemID != -1 )
            {
                continue;
            }

            return m_container[i];
        }

        return null;
    }

    public InventorySlot GetFirstValidInventorySlotContaining( ItemObject item )
    {
        for (int i = 0; i < m_container.Length; i++)
        {
            if (m_container[ i ].itemID == item.itemID && m_container[i].amount < item.itemStacksize)
            {
                return m_container[ i ];
            }
        }

        return null;
    }

    public InventorySlot GetFirstInventorySlotContaining( ItemObject item )
    {
        for (int i = 0; i < m_container.Length; i++)
        {
            if (m_container[ i ].itemID == item.itemID)
            {
                return m_container[ i ];
            }
        }

        return null;
    }

    public void AddItem( ItemObject item, int amount )
    {
        InventorySlot slot;

        // Check if we already have an instance of the item stored.
        if (Contains( item ))
        {
            // Get the first inventory slot with the item.
            slot = GetFirstValidInventorySlotContaining( item );

            if(slot == null)
            {
                slot = GetFreeInventorySlot();

                slot.itemID = item.itemID;
            }
        }
        else
        {
            // Get the first free inventory slot.
            slot = GetFreeInventorySlot();
            
            slot.itemID = item.itemID;
        }

        slot.amount += amount;

        int overhead = slot.amount - item.itemStacksize;

        if(overhead <= 0)
        {
            return;
        }

        AddItem( item, overhead );
        slot.amount = item.itemStacksize;
    }

    public int RemoveItem( ItemObject item, int amount )
    {
        InventorySlot slot;

        if (!Contains( item ))
        {
            return 0;
        }

        slot = GetFirstInventorySlotContaining( item );

        if(slot.amount == amount)
        {
            slot.itemID = -1;
            slot.amount = 0;
            return amount;
        }

        if(slot.amount > amount)
        {
            slot.amount -= amount;
            return amount;
        }

        // TODO: This may or may not work.
        int remaining = amount - slot.amount;
        int removed = slot.amount;
        
        slot.amount = 0;
        slot.itemID = -1;
        
        return removed + RemoveItem( item, remaining );
        //
    }

    // This needs to save the inventory object
    // the method was called on.
    public void Save()
    {
        throw new System.NotImplementedException( "Inventory::Save() - Not Implemented." );
    }

    // This needs to load a saved inventory,
    // and output a new inventory object with the loaded values
    public static Inventory Load( string fileName )
    {
        throw new System.NotImplementedException( "Inventory::Load() - Not Implemented.");
    }
}

[System.Serializable]
public class InventorySlot
{
    public InventorySlot()
    {
        itemID = -1;
        amount = 0;
    }

    public InventorySlot( InventorySlot slot )
    {
        itemID = slot.itemID;
        amount = slot.amount;
    }

    public int itemID;
    public int amount;
}