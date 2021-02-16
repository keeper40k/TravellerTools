using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    public class TravellerSkill
    {
        // Public Constructors
        public TravellerSkill()
        {
            InitialiseData();
        }

        public TravellerSkill(TravellerSkill source)
        {
            InitialiseData();
            Name = source.Name;
            Summary = source.Summary;
            Description = source.Description;
            Referee = source.Referee;
            HasSpecialisations = source.HasSpecialisations;
            foreach( TravellerSkill specialisation in Specialisations )
            {
                Specialisations.Add(new TravellerSkill(specialisation));
            }
            
        }

        // Protected Methods

        protected void InitialiseData()
        {
            Name = string.Empty;
            Summary = string.Empty;
            Description = string.Empty;
            Referee = string.Empty;

            HasSpecialisations = false;
            Specialisations = new List<TravellerSkill>();
        }

        // Public Methods

        public override string ToString()
        {
            return Name;
        }

        // Public Properties

        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Referee { get; set; }
        public decimal Level { get; set; }
        public bool HasSpecialisations { get; set; }
        public List<TravellerSkill> Specialisations { get; set; }
    }
}
