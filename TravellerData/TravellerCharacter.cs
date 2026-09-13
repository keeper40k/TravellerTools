using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerTools.Fundamentals;

namespace TravellerTools.TravellerData
{
    /// <summary>Stores character characteristics, career history, skills, and inventory.</summary>
    /// <remarks>This mutable model uses default dice randomness for initial characteristics and aging. Collections and callbacks must remain valid for the operations that use them; instances are not synchronized for concurrent mutation.</remarks>
    public class TravellerCharacter
    {
        // Static Strings

        private static string SIR = "Sir";
        private static string LADY = "Lady";
        private static string KNIGHT = "Knight";
        private static string KNIGHTESS = "Knightess";
        private static string DAME = "Dame";
        private static string BARON = "Baron";
        private static string BARONET = "Baronet";
        private static string BARONESS = "Baroness";
        private static string MARQUIS = "Marquis";
        private static string MARQUESA = "Marquesa";
        private static string MARCHIONESS = "Marchioness";
        private static string COUNT = "Count";
        private static string VISCOUNT = "Viscount";
        private static string COUNTESS = "Countess";
        private static string DUKE = "Duke";
        private static string DUCHESS = "Duchess";
        private static string ARCHDUKE = "Archduke";
        private static string ARCHDUCHESS = "Archduchess";
        private static string PRINCE = "Prince";
        private static string PRINCESS = "Princess";
        private static string EMPEROR = "Emperor";
        private static string EMPERORESS = "Emperoress";

        private static string FAILED_AGING_CRISIS = "{0} suffered an Aging Crisis and passed away at the age of {1}.";
        private static string PASSED_AGING_CRISIS = "{0} passed through Aging Crisis at the age of {1} and aged a further {2} months due to Slow Drug recovery.";

        private static string ATT_SKILL_LOSS = "{0} lost {1} {2}\n";
        private static string ATT_SKILL_GAIN = "{0} gained {1} {2}\n";

        // Constructors

        /// <summary>Creates a character named Bob at age eighteen and rolls all six characteristics on 2d6.</summary>
        public TravellerCharacter()
        {
            // Temporary Name
            Name = "Bob";
            Reinitialise();
            RollRandomCharacteristics();
        }

        // Protected Methods

        /// <summary>Applies aging checks at eligible ages while advancing in one-year increments.</summary>
        /// <param name="start">The previous age in years.</param>
        /// <param name="end">The requested age in years.</param>
        /// <remarks>An end age no greater than the start performs no checks. Eligible increments exceed age 33 and have remainder two when divided by four; fractional ages are not rounded.</remarks>
        protected void ProcessAging(decimal start, decimal end)
        {
            // Doesn't handle negative aging at this time!
            if( end <= start)
            {
                return;
            }

            for( decimal i = start+1; i <= end; i++ )
            {
                // i mod 4 remainder 2 gives us aging years of 34, 38, 42, etc.
                if ( (i > 33) && (i % 4 == 2) )
                {
                    if( i < 50 )
                    {
                        Phase1AgeCheck(i);
                    }
                    else if( i < 66 )
                    {
                        Phase2AgeCheck(i);
                    }
                    else
                    {
                        Phase3AgeCheck(i);
                    }
                }
            }
        }

        // STR, DEX and END save on (8+, 7+, 8+)
        /// <summary>Applies STR, DEX, and END aging saves of 8+, 7+, and 8+, losing one point on failure.</summary>
        /// <param name="currentAge">The age used in any aging-crisis history entry.</param>
        /// <remarks>Uses default 2d6 randomness and then checks for an aging crisis.</remarks>
        protected void Phase1AgeCheck(decimal currentAge)
        {
            int strRoll = DiceTools.RollDice(2, 6);
            if( strRoll < 8 )
            {
                STR = STR - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "STR");
            }
            int dexRoll = DiceTools.RollDice(2, 6);
            if (dexRoll < 7)
            {
                DEX = DEX - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "DEX");
            }
            int endRoll = DiceTools.RollDice(2, 6);
            if (endRoll < 8)
            {
                END = END - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "END");
            }
            AgingCrisis(currentAge);
        }

        // STR, DEX and END save on (9+, 8+, 9+)
        /// <summary>Applies STR, DEX, and END aging saves of 9+, 8+, and 9+, losing one point on failure.</summary>
        /// <param name="currentAge">The age used in any aging-crisis history entry.</param>
        /// <remarks>Uses default 2d6 randomness and then checks for an aging crisis.</remarks>
        protected void Phase2AgeCheck(decimal currentAge)
        {
            int strRoll = DiceTools.RollDice(2, 6);
            if (strRoll < 9)
            {
                STR = STR - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "STR");
            }
            int dexRoll = DiceTools.RollDice(2, 6);
            if (dexRoll < 8)
            {
                DEX = DEX - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "DEX");
            }
            int endRoll = DiceTools.RollDice(2, 6);
            if (endRoll < 9)
            {
                END = END - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "END");
            }
            AgingCrisis(currentAge);
        }

        // STR, DEX, END and INT save on (9+, 9+, 9+, 9+)
        /// <summary>Applies 9+ aging saves, losing two STR, DEX, or END points and one INT point on failure.</summary>
        /// <param name="currentAge">The age used in any aging-crisis history entry.</param>
        /// <remarks>Uses default 2d6 randomness and then checks for an aging crisis.</remarks>
        protected void Phase3AgeCheck(decimal currentAge)
        {
            int strRoll = DiceTools.RollDice(2, 6);
            if (strRoll < 9)
            {
                STR = STR - 2;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 2, "STR");
            }
            int dexRoll = DiceTools.RollDice(2, 6);
            if (dexRoll < 9)
            {
                DEX = DEX - 2;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 2, "DEX");
            }
            int endRoll = DiceTools.RollDice(2, 6);
            if (endRoll < 9)
            {
                END = END - 2;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 2, "END");
            }
            int intRoll = DiceTools.RollDice(2, 6);
            if (intRoll < 9)
            {
                INT = INT - 1;
                CreationHistory += string.Format(ATT_SKILL_LOSS, Name, 1, "INT");
            }
            AgingCrisis(currentAge);
        }

        /// <summary>Resolves 8+ survival saves for STR, DEX, END, or INT that are exactly zero.</summary>
        /// <param name="currentAge">The age reported in the creation history.</param>
        /// <remarks>Failure marks the character dead. A successful save restores the characteristic to one and rolls recovery months for the history entry; those months are not added to Age.</remarks>
        protected void AgingCrisis(decimal currentAge)
        {
            int slowDrugAging = 0;
            if( STR == 0 )
            {
                int saveRoll = DiceTools.RollDice(2, 6);
                if( saveRoll < 8 )
                {
                    IsDead = true;
                }
                else
                {
                    STR = 1;
                    slowDrugAging += DiceTools.RollOneDie(6);
                }
            }
            if (DEX == 0)
            {
                int saveRoll = DiceTools.RollDice(2, 6);
                if (saveRoll < 8)
                {
                    IsDead = true;
                }
                else
                {
                    DEX = 1;
                    slowDrugAging += DiceTools.RollOneDie(6);
                }
            }
            if (END == 0)
            {
                int saveRoll = DiceTools.RollDice(2, 6);
                if (saveRoll < 8)
                {
                    IsDead = true;
                }
                else
                {
                    END = 1;
                    slowDrugAging += DiceTools.RollOneDie(6);
                }
            }
            if (INT == 0)
            {
                int saveRoll = DiceTools.RollDice(2, 6);
                if (saveRoll < 8)
                {
                    IsDead = true;
                }
                else
                {
                    INT = 1;
                    slowDrugAging += DiceTools.RollOneDie(6);
                }
            }
            if (IsDead)
            {
                CreationHistory += string.Format(FAILED_AGING_CRISIS, Name, currentAge) + "\n";
            }
            else if (slowDrugAging != 0)
            {
                CreationHistory += string.Format(PASSED_AGING_CRISIS, Name, currentAge, slowDrugAging) + "\n";
            }
        }

        // Public Methods

        /// <summary>Formats a characteristic as one Traveller extended-hexadecimal digit.</summary>
        /// <param name="value">The characteristic value.</param>
        /// <returns>A digit from 0 through Z, skipping I and O; non-positive values become 0 and values above 33 become Z.</returns>
        public string EHexCharacteristic(int value)
        {
            string result = "";
            if (value <= 0)
            {
                result = "0";
            }
            else if (value > 0 && value <= 9)
            {
                result = value.ToString();
            }
            else if (value <= 17) //17 is H and then we skip I
            {
                result += (char)(value + 55);
            }
            else if (value <= 22) //22 is N then we skip O
            {
                result += (char)(value + 56);
            }
            else if (value <= 33) //33 is the maximum.  Everything above resolves to 33.
            {
                result += (char)(value + 57);
            }
            else  //33 is the maximum.  Everything above resolves to 33 or Z.
            {
                result = "Z";
            }

            return result;
        }

        /// <summary>Resets career, inventory, cash, history, and survival state for creation at age eighteen.</summary>
        /// <remarks>Replaces Skills and Gear with new lists. Preserves Name, the six characteristic values, and the specialisation callback; does not reroll characteristics.</remarks>
        public void Reinitialise()
        {
            Age = 18;
            Title = string.Empty;
            UseTitle = true;
            Rank = string.Empty;
            UseRank = true;
            Service = string.Empty;
            Drafted = false;
            FailedService = string.Empty;
            TermsOfService = 0;
            InjuredDuringCreation = false;
            RankNumber = 0;
            Skills = new List<TravellerSkill>();

            CreationHistory = string.Empty;

            IsDead = false;

            Cash = 0;
            Gear = new List<TravellerGear>();
        }

        /// <summary>Replaces STR, DEX, END, INT, EDU, and SOC with separate default-source 2d6 totals.</summary>
        /// <remarks>Assigning SOC also updates UseTitle.</remarks>
        public void RollRandomCharacteristics()
        {
            STR = DiceTools.RollDice(2, 6);
            DEX = DiceTools.RollDice(2, 6);
            END = DiceTools.RollDice(2, 6);
            INT = DiceTools.RollDice(2, 6);
            EDU = DiceTools.RollDice(2, 6);
            SOC = DiceTools.RollDice(2, 6);
        }

        /// <summary>Creates the title choices for the current social standing.</summary>
        /// <returns>A new list for SOC 11 through 17, or a single empty string for any other value.</returns>
        public List<string> AvailableTitles()
        {
            List<string> titles = new List<string>();
            switch (SOC)
            {
                case 11:
                {
                    titles.Add(KNIGHT);
                    titles.Add(KNIGHTESS);
                    titles.Add(DAME);
                    titles.Add(SIR);
                    titles.Add(LADY);
                    break;
                }
                case 12:
                {
                    titles.Add(BARON);
                    titles.Add(BARONET);
                    titles.Add(BARONESS);
                    break;
                }
                case 13:
                    {
                    titles.Add(MARQUIS);
                    titles.Add(MARQUESA);
                    titles.Add(MARCHIONESS);
                    break;
                }
                case 14:
                {
                    titles.Add(COUNT);
                    titles.Add(VISCOUNT);
                    titles.Add(COUNTESS);
                    break;
                }
                case 15:
                {
                    titles.Add(DUKE);
                    titles.Add(DUCHESS);
                    break;
                }
                case 16:
                {
                    titles.Add(ARCHDUKE);
                    titles.Add(ARCHDUCHESS);
                    titles.Add(PRINCE);
                    titles.Add(PRINCESS);
                    break;
                }
                case 17:
                {
                    titles.Add(EMPEROR);
                    titles.Add(EMPERORESS);
                    break;
                }
                default:
                {
                    titles.Add(string.Empty);
                    break;
                }
            }
            return titles;
        }

        /// <summary>Applies a characteristic or skill adjustment and records the characteristic or resolved skill name in the creation history.</summary>
        /// <param name="newSkill">The non-null adjustment; characteristic names must use uppercase Traveller codes.</param>
        /// <param name="callback">The selector used when a matched skill has specialisations; not consulted for characteristic adjustments.</param>
        /// <remarks>Unknown characteristics or missing skill definitions have no effect. Existing skills are matched by name and incremented; new skills are copied from the shared definitions. A null selection cancels the skill gain. The callback field is assigned for the operation and cleared on normal completion, but an early cancellation or exception can leave it assigned.</remarks>
        /// <exception cref="TypeInitializationException">Loading shared skill definitions fails during a skill adjustment.</exception>
        public void AddSkill( TravellerSkillModifier newSkill, ISkillSpecialisationCollection callback )
        {
            SpecialisationSelectionCallback = callback;

            // TO DO
            if( newSkill.IsAttribute )
            {
                switch( newSkill.Name )
                {
                    case "STR":
                    {
                        STR += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "DEX":
                    {
                        DEX += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "END":
                    {
                        END += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "INT":
                    {
                        INT += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "EDU":
                    {
                        EDU += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    case "SOC":
                    {
                        SOC += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                    default:
                    {
                        // Do nothing
                        break;
                    }
                }
            }
            if( newSkill.IsSkill )
            {
                TravellerSkill? fullSkill = TravellerSkills.MatchSkill(newSkill.Name);
                if (fullSkill != null)
                {
                    // Resolve Specialisation. While loop for nesting
                    while( fullSkill.HasSpecialisations )
                    {
                        fullSkill = ChooseSpecialisation(fullSkill.Name, fullSkill.Specialisations);
                        if (fullSkill == null)
                        {
                            return;
                        }
                    }

                    bool found = false;
                    foreach (TravellerSkill existingSkill in Skills)
                    {
                        if( existingSkill.Name == fullSkill.Name )
                        {
                            found = true;
                            existingSkill.Level += newSkill.Level;
                            CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, fullSkill.Name);
                            break;
                        }
                    }

                    if (!found )
                    {
                        TravellerSkill localSkill = new TravellerSkill(fullSkill);
                        localSkill.Level = newSkill.Level;
                        Skills.Add( localSkill );
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, fullSkill.Name);
                    }
                }
            }

            SpecialisationSelectionCallback = null;
        }

        // Should only be used when the newSkill does not have further specialisation
        /// <summary>Adds an already-resolved skill or increases the level of the first skill with the same name.</summary>
        /// <param name="newSkill">The skill to add; resolve any specialisations before calling.</param>
        /// <remarks>A new entry retains the supplied object by reference. Existing entries gain its Level. Legacy null input is ignored.</remarks>
        public void AddSkill(TravellerSkill newSkill)
        {
            if (newSkill != null)
            {
                bool found = false;
                foreach (TravellerSkill existingSkill in Skills)
                {
                    if (existingSkill.Name == newSkill.Name)
                    {
                        found = true;
                        existingSkill.Level += newSkill.Level;
                        CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                        break;
                    }
                }

                if (!found)
                {
                    Skills.Add(newSkill);
                    CreationHistory += string.Format(ATT_SKILL_GAIN, Name, newSkill.Level, newSkill.Name);
                }
            }
        }

        // Assumes gear is not null.  Will throw ArgumentOutOfRangeException if it is null
        /// <summary>Adds inventory gear or increases the counts of existing entries with the same name.</summary>
        /// <param name="gear">The inventory entry to add; must not be null.</param>
        /// <remarks>A new entry retains the supplied object by reference. If duplicate names already exist, all matching entries gain its Count.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">The gear argument is null.</exception>
        public void AddGear( TravellerGear gear )
        {
            if (gear != null)
            {
                bool found = false;
                foreach (TravellerGear item in Gear)
                {
                    if (gear.Name == item.Name)
                    {
                        found = true;
                        item.Count += gear.Count;
                    }
                }
                if ( !found )
                {
                    Gear.Add(gear);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("gear", "gear parameter cannot be null");
            }
        }

        // Public Override Methods

        /// <summary>Formats the character summary followed by service and creation history.</summary>
        /// <returns>The summary and history separated by a line-feed character.</returns>
        public override string ToString()
        {
            return ShortStringFormat() + "\n" + CharacterHistory();
        }

        // Public Methods

        /// <summary>Formats the character identity, UPP, age, cash, skills, and gear.</summary>
        /// <returns>Display text using tabs and line feeds; title and rank are included when their flags are enabled.</returns>
        public string ShortStringFormat()
        {
            string result = "";
            if ((Title != string.Empty) && UseTitle)
            {
                result += Title + " ";
            }
            if ((Rank != string.Empty) && UseRank)
            {
                result += Rank + " ";
            }
            result += Name + "\t" + UPP + "\tAge " + Age + "\tCr" + Cash + "\n";

            for (int i = 0; i < Skills.Count; i++)
            {
                result += Skills[i].Name + "-" + Skills[i].Level;
                if (i < Skills.Count - 1)
                {
                    result += ", ";
                }
            }

            if (Gear.Count > 0)
            {
                result += "\n";
            }

            for (int i = 0; i < Gear.Count; i++)
            {
                result += Gear[i].DisplayString();
                if (i < Gear.Count - 1)
                {
                    result += ", ";
                }
            }
            return result;
        }

        /// <summary>Formats the service name and recorded creation events.</summary>
        /// <returns>The service and history text, or an empty string when both are empty.</returns>
        public string CharacterHistory()
        {
            string result = string.Empty;
            if (Service != string.Empty)
            {
                result += "Service: " + Service;
            }
            if (CreationHistory != string.Empty)
            {
                result += "\n\n" + CreationHistory;
            }
            return result;
        }

        /// <summary>Checks the character's top-level skill entries for an exact name.</summary>
        /// <param name="name">The case-sensitive name to find.</param>
        /// <returns>True if any skill name matches, regardless of its level; otherwise false.</returns>
        public bool HasSkill( string name )
        {
            bool result = false;
            foreach( TravellerSkill skill in Skills )
            {
                if( name == skill.Name )
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        // Property Backers complex properties

        private int socialStanding;
        private decimal age;

        // Public Properties

        /// <summary>Gets or sets the chosen social title.</summary>
        public string Title { get; set; } = string.Empty;
        // Doesn't need to be serialised
        /// <summary>Controls whether the title is included in the character summary; assigning SOC also updates this flag.</summary>
        public bool UseTitle; 
        /// <summary>Gets or sets the displayed service-rank name.</summary>
        public string Rank { get; set; } = string.Empty;
        /// <summary>Gets or sets whether the service rank appears in the character summary.</summary>
        public bool UseRank { get; set; }
        /// <summary>Gets or sets the character's display name.</summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>Gets or sets Strength.</summary>
        public int STR { get; set; }
        /// <summary>Gets or sets Dexterity.</summary>
        public int DEX { get; set; }
        /// <summary>Gets or sets Endurance.</summary>
        public int END { get; set; }
        /// <summary>Gets or sets Intelligence.</summary>
        public int INT { get; set; }
        /// <summary>Gets or sets Education.</summary>
        public int EDU { get; set; }
        /// <summary>Gets or sets Social Standing; setting a value of at least eleven enables UseTitle and lower values disable it.</summary>
        public int SOC
        {
            get
            {
                return socialStanding;
            }
            set
            {
                socialStanding = value;
                if (value >= 11)
                {
                    UseTitle = true;
                }
                else
                {
                    UseTitle = false;
                }
            }
        }

        // Doesn't need to be serialised
        /// <summary>Gets the six-digit extended-hexadecimal profile in STR, DEX, END, INT, EDU, SOC order.</summary>
        public string UPP
        {
            get
            {
                string upp = EHexCharacteristic(STR) +
                             EHexCharacteristic(DEX) +
                             EHexCharacteristic(END) +
                             EHexCharacteristic(INT) +
                             EHexCharacteristic(EDU) +
                             EHexCharacteristic(SOC);
                return upp;
            }
        }

        /// <summary>Gets whether RankNumber is greater than zero.</summary>
        public bool Commissioned
        {
            get
            {
                return RankNumber > 0;
            }
        }

        /// <summary>Gets or sets age in years; increasing the value may roll aging checks and mutate characteristics, history, and death status.</summary>
        public decimal Age
        {
            get
            {
                return age;
            }
            set
            {
                ProcessAging(age, value);
                age = value;
            }
        }
        /// <summary>Gets or sets the current or former service name.</summary>
        public string Service { get; set; } = string.Empty;
        /// <summary>Gets or sets whether the character entered service through the draft.</summary>
        public bool Drafted { get; set; }
        /// <summary>Gets or sets the service whose enlistment attempt failed.</summary>
        public string FailedService { get; set; } = string.Empty;
        /// <summary>Gets or sets the recorded number of service terms.</summary>
        public decimal TermsOfService { get; set; }
        /// <summary>Gets or sets whether an injury ended service during character creation.</summary>
        public bool InjuredDuringCreation { get; set; }
        /// <summary>Gets or sets the service-rank index; positive values indicate a commission.</summary>
        public decimal RankNumber { get; set; }
        /// <summary>Gets or sets the mutable list of character skills; consumers require a non-null list.</summary>
        public List<TravellerSkill> Skills { get; set; } = new();

        /// <summary>Gets or sets the accumulated character-creation narrative.</summary>
        public string CreationHistory { get; set; } = string.Empty;

        /// <summary>Gets or sets whether the character has died.</summary>
        public bool IsDead { get; set; }

        /// <summary>Gets or sets available cash in credits.</summary>
        public int Cash { get; set; }
        /// <summary>Gets or sets the mutable inventory; consumers require a non-null list.</summary>
        public List<TravellerGear> Gear { get; set; } = new();

        // Event Management

        // Not using { get; set; } here as this data is not for serialisation. Should only be one at once.
        /// <summary>The callback currently used to choose skill specialisations; may be null outside a selection operation.</summary>
        public ISkillSpecialisationCollection? SpecialisationSelectionCallback;

        /// <summary>Delegates a skill choice to the currently assigned specialisation callback.</summary>
        /// <param name="skillName">The parent skill name.</param>
        /// <param name="list">The available child skills.</param>
        /// <returns>The callback's selection, or null when it declines.</returns>
        /// <remarks>SpecialisationSelectionCallback must be assigned before calling.</remarks>
        protected TravellerSkill? ChooseSpecialisation( string skillName, List<TravellerSkill> list )
        {
            return SpecialisationSelectionCallback!.SelectSpecialisation(skillName, list);
        }
    }
}
