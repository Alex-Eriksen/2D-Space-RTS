using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starlight
{
    public class Colony
    {
        private BuildingDatabaseObject m_database;

        public Colony( int colonyIndex )
        {
            ColonyIndex = colonyIndex;
            m_builtBuildings = new List<Building>();
            m_constructionQueue = new List<BuildingType>();
            m_database = Resources.Load<BuildingDatabaseObject>( "Objects/Buildings/Building Database 01" );
        }

        public readonly int ColonyIndex;

        public Planet Planet;

        public string ColonyName;

        public int Population => m_population;
        public int ResourceFlow => 0;
        public int CreditFlow => 0;

        //private int m_popFarmers;
        //private int m_popWorkers;
        //private int m_popScientists;

        private int m_population;

        // TODO: Cap the maximum amount of buildings you can build.
        private List<Building> m_builtBuildings;
        private List<BuildingType> m_constructionQueue;

        public List<Building> BuiltBuildings => m_builtBuildings;

        public void Generate( Planet planet )
        {
            Planet = planet;
            ColonyName = GenerateColonyName();
        }

        private string GenerateColonyName()
        {
            return Planet.planetName + " - Colony " + ColonyIndex + 1;
        }

        public int GetMaxBuildingNumber() => m_population + 1;

        private void GetListOfValidBuildings()
        {
            // Return an array of all buildings that can be built on this planet.

            // METHOD 1:
            //      For each building in our master building list,
            //      look up if the player has unlocked the corresponding technology.
        
            // METHOD 2:
            //      Whenever a technology is unlocked that enables a building,
            //      then that building is added to the list of legal buildings
            //      for that player.

            // In either case, filter out buildings already built here.
        }

        public void UpdateColony()
        {

        }

        // TODO: Calculate the production.
        private int GetTotalResourceProduction()
        {
            return 0;
        }

        // TODO: Make a method that adds a building to a building queue of sort.
        public void AddBuildingToQueue()
        {
            
        }
    }
}
