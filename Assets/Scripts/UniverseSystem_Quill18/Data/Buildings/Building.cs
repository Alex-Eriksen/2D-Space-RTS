using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Starlight
{
    public class BuildingGraphics
    {
        public BuildingGraphics( Building building )
        {
            m_building = building;
        }

        private readonly Building m_building;

        public string IconPath => $"BuildingIcons/{(int) m_building.BuildingType}";
    }
    public class Building
    {
        public Building( int buildingIndex )
        {
            BuildingIndex = buildingIndex;
            BuildingGraphics = new BuildingGraphics( this );
        }

        public readonly int BuildingIndex;

        public int Priority = 5;

        public BuildingType BuildingType;

        public BuildingGraphics BuildingGraphics;

        public int PopulationCount { get { return m_assignedPopulation; } }

        protected int m_assignedPopulation = 0;
    }

    public enum BuildingType
    {
        Factory,
        Farm,
        Housing,
        Mining,
        Garrison
    }
}
