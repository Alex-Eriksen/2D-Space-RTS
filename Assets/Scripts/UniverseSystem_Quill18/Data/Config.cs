using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starlight
{
    public class Config
    {
        // FOR NOW, the idea of this Config class is to be an index
        // of values that don't change at runtime.
        //      i.e. things like galaxy settings from the NEW GAME screen
        //              won't be saved here.
        //      THIS MAY CHANGE!

        // Values are coded here for now,
        // but the goal will be to load in from a config file that is modable.

        public static int GetInt(string Parameter)
        {
            switch (Parameter)
            {
                case "PLANET_MAX_POPULATION_Tiny":
                return 4;

                case "PLANET_MAX_POPULATION_Small":
                return 6;

                case "PLANET_MAX_POPULATION_Medium":
                return 10;

                case "PLANET_MAX_POPULATION_Large":
                return 12;

                case "PLANET_MAX_POPULATION_Huge":
                return 16;

                case "STAR_MAX_PLANETS":
                return 9;

                default:
                Debug.LogError("Config::GetInt -- No option for " + Parameter);
                return 0;
            }
        }
        public static float GetFloat(string Parameter)
        {
            switch (Parameter)
            {
                case "STAR_ORBIT_DISTANCE":
                return 150.0f;

                case "STAR_CLASS_M_WEIGHT":
                return 76.45f;

                case "STAR_CLASS_K_WEIGHT":
                return 12.1f;

                case "STAR_CLASS_G_WEIGHT":
                return 7.6f;

                case "STAR_CLASS_F_WEIGHT":
                return 3f;

                case "STAR_CLASS_A_WEIGHT":
                return 0.6f;

                case "STAR_CLASS_B_WEIGHT":
                return 0.13f;

                case "STAR_CLASS_O_WEIGHT":
                return 0.00003f;

                default:
                Debug.LogError("Config::GetFloat -- No option for " + Parameter);
                return 0;
            }
        }

        public static decimal GetDecimal(string Parameter)
        {
            switch (Parameter)
            {
                // Kilometers (km)
                case "SOL_RADIUS":
                return 695700m;

                // Kilometers (km)
                case "EARTH_RADIUS":
                return 6371m;

                // Kilograms (kg)
                case "EARTH_MASS":
                return 5.9722m * (decimal) System.Math.Pow( 10, 24 );

                default:
                Debug.LogError( "Config::GetDecimal -- No option for " + Parameter );
                return 0m;
            }
        }

        public static double GetDouble(string Parameter )
        {
            switch (Parameter)
            {
                // Kilograms (kg)
                case "SOL_MASS":
                return 1.98847d * System.Math.Pow( 10, 30 );

                default:
                Debug.LogError( "Config::GetDecimal -- No option for " + Parameter );
                return 0d;
            }
        }
    }
}
