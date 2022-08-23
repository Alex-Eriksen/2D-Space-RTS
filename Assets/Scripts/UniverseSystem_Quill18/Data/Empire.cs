using UnityEngine;
using System.Collections.Generic;

namespace Starlight
{
    public abstract class Empire
    {
        public Empire( int planetIndex )
        {
            EmpireIndex = planetIndex;
            m_colonies = new List<Colony>();
        }

        public readonly int EmpireIndex;

        protected int m_race;   // TODO: Define the race and their values in a file. JSON? Text?
        protected int m_money;

        protected int m_bonusFoodProduction = 0;

        protected bool[] m_unlockedTechnologies;    // Not how we're going to do it.
                                                    // TODO: Revise the unlocked technology handling.

        protected List<Colony> m_colonies;

        public virtual void UpdateEmpire()
        {
            foreach (Colony colony in m_colonies)
            {
                colony.UpdateColony();
            }
        }
    }

    public class Empire_Human : Empire
    {
        public Empire_Human( int planetIndex ) : base( planetIndex )
        {
            
        }
    }    

    public class Empire_AI : Empire
    {
        public Empire_AI( int planetIndex ) : base( planetIndex )
        {

        }
    }
}
