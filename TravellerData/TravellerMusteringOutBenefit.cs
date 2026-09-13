using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerTools.TravellerData
{
    /// <summary>Describes a named outcome on a service's non-cash mustering-out table.</summary>
    public class TravellerMusteringOutBenefit
    {
        // Public Constructor

        /// <summary>Creates an unnamed benefit with both classification flags false.</summary>
        public TravellerMusteringOutBenefit()
        {
            Name = string.Empty;
            IsAtt = false;
            IsGear = false;
        }

        // Public override methods

        /// <summary>Formats the benefit for selection lists.</summary>
        /// <returns>The benefit name.</returns>
        public override string ToString()
        {
            return Name;
        }

        // Public Properties

        /// <summary>Gets or sets the name used to resolve and display the benefit.</summary>
        public string Name { get; set; }
        /// <summary>Gets or sets whether the outcome is interpreted as a characteristic increase.</summary>
        public bool IsAtt { get; set; }
        /// <summary>Gets or sets whether the outcome is interpreted as an inventory benefit.</summary>
        /// <remarks>This flag is independent of IsAtt; setting either flag does not adjust the other.</remarks>
        public bool IsGear { get; set; }
    }
}
