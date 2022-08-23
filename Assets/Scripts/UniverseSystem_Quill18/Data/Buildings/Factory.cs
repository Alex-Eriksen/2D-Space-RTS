using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starlight
{
    public class Factory : Building
    {
        public Factory( int buildingIndex ) : base( buildingIndex )
        {
            BuildingType = BuildingType.Factory;
            m_inventory = new Inventory();
        }

        private Inventory m_inventory;

        // Add the factorys products, this could be a list of items or it could be a single item.
        // Maybe we want the factory to be able to produce different items at the same time.
        // How many items should it make from a single recipe?
        // What type of items can the factory produce?

        public void AddToProductionQueue( CraftableObject item )
        {
            
        }
    }
}
