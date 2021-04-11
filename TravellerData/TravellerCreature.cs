using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerCreature
    {
        // Local Public Enums

        public enum CreatureType
        {
            Undefined = -1,
            Filter = 1,
            Intermittent = 2,
            Grazer = 3,
            Gatherer = 104,
            Hunter = 105,
            Eater = 106,
            Pouncer = 207,
            Chaser = 208,
            Trapper = 209,
            Siren = 210,
            Killer = 211,
            Hijacker = 312,
            Intimidator = 313,
            Carrion_Eater = 314,
            Reducer = 315
        }

        // protected constants

        protected int HERBIVORE_MIN = 1;
        protected int HERBIVORE_MAX = 99;
        protected int OMNIVORE_MIN = 101;
        protected int OMNIVORE_MAX = 199;
        protected int CARNIVORE_MIN = 201;
        protected int CARNIVORE_MAX = 299;
        protected int SCAVENGER_MIN = 301;
        protected int SCAVENGER_MAX = 399;

        protected const string CREATURE_TYPE_Undefined = "Undefined";
        protected const string CREATURE_TYPE_Filter = "Filter";
        protected const string CREATURE_TYPE_Intermittent = "Intermittent";
        protected const string CREATURE_TYPE_Grazer = "Grazer";
        protected const string CREATURE_TYPE_Gatherer = "Gatherer";
        protected const string CREATURE_TYPE_Hunter = "Hunter";
        protected const string CREATURE_TYPE_Eater = "Eater";
        protected const string CREATURE_TYPE_Pouncer = "Pouncer";
        protected const string CREATURE_TYPE_Chaser = "Chaser";
        protected const string CREATURE_TYPE_Trapper = "Trapper";
        protected const string CREATURE_TYPE_Siren = "Siren";
        protected const string CREATURE_TYPE_Killer = "Killer";
        protected const string CREATURE_TYPE_Hijacker = "Hijacker";
        protected const string CREATURE_TYPE_Intimidator = "Intimidator";
        protected const string CREATURE_TYPE_Carrion_Eater = "Carrion Eater";
        protected const string CREATURE_TYPE_Reducer = "Reducer";

        // Constructors

        public TravellerCreature()
        {
            Type = CreatureType.Undefined;
        }

        // Public Methods

        public bool IsHerbivore()
        {
            return ((int)Type >= HERBIVORE_MIN) && ((int)Type <= HERBIVORE_MAX);
        }

        public bool IsOmnivore()
        {
            return ((int)Type >= OMNIVORE_MIN) && ((int)Type <= OMNIVORE_MAX);
        }

        public bool IsCarnivore()
        {
            return ((int)Type >= CARNIVORE_MIN) && ((int)Type <= CARNIVORE_MAX);
        }

        public bool IsScavenger()
        {
            return ((int)Type >= SCAVENGER_MIN) && ((int)Type <= SCAVENGER_MAX);
        }

        // public Static methods

        public static string NameOfType( CreatureType type )
        {
            string result = string.Empty;
            switch (type)
            {
                case CreatureType.Undefined:
                {
                    result = CREATURE_TYPE_Undefined;
                    break;
                }
                case CreatureType.Filter:
                {
                    result = CREATURE_TYPE_Filter;
                    break;
                }
                case CreatureType.Intermittent:
                {
                    result = CREATURE_TYPE_Intermittent;
                    break;
                }
                case CreatureType.Grazer:
                {
                    result = CREATURE_TYPE_Grazer;
                    break;
                }
                case CreatureType.Gatherer:
                {
                    result = CREATURE_TYPE_Gatherer;
                    break;
                }
                case CreatureType.Hunter:
                {
                    result = CREATURE_TYPE_Hunter;
                    break;
                }
                case CreatureType.Eater:
                {
                    result = CREATURE_TYPE_Eater;
                    break;
                }
                case CreatureType.Pouncer:
                {
                    result = CREATURE_TYPE_Pouncer;
                    break;
                }
                case CreatureType.Chaser:
                {
                    result = CREATURE_TYPE_Chaser;
                    break;
                }
                case CreatureType.Trapper:
                {
                    result = CREATURE_TYPE_Trapper;
                    break;
                }
                case CreatureType.Siren:
                {
                    result = CREATURE_TYPE_Siren;
                    break;
                }
                case CreatureType.Killer:
                {
                    result = CREATURE_TYPE_Killer;
                    break;
                }
                case CreatureType.Hijacker:
                {
                    result = CREATURE_TYPE_Hijacker;
                    break;
                }
                case CreatureType.Intimidator:
                {
                    result = CREATURE_TYPE_Intimidator;
                    break;
                }
                case CreatureType.Carrion_Eater:
                {
                    result = CREATURE_TYPE_Carrion_Eater;
                    break;
                }
                case CreatureType.Reducer:
                {
                    result = CREATURE_TYPE_Reducer;
                    break;
                }
                default:
                {
                    // Do nothing
                    break;
                }
            }
            return result;
        }

        // Public Properties

        public CreatureType Type;

        public string TypeName
        {
            get
            {
                return TravellerCreature.NameOfType( Type );
            }
        }

        // Creture weight is given in kg
        public int Weight;

        // HitsToUnconcious and TotalHits are the capacity of the creature to take damage
        public int HitsToUnconcious;
        public int TotalHits;

        public string Armour;

        // This is the caracity of the create to deal out damage
        public int Wounds;
        public string Weapons;

        public decimal AttackPredisposition;
        public decimal FleeDisposition;
        public decimal Speed;
    }
}
