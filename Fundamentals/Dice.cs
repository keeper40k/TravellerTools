using System;

namespace TravellerTools.Fundamentals
{

	// A dice definition for one dice, or a handful of dice of the same type
	/// <summary>Describes a mutable group of dice with a common side count.</summary>
	/// <remarks>Constructors replace invalid counts with defaults. Later field assignments are not validated; dice-rolling methods validate the group when used. This type describes dice, not individual roll outcomes or notation with modifiers.</remarks>
	public class Dice
	{

		// Constructors

		// sides must be 2 or more. If not, it will default to 6.
		/// <summary>Creates a single-die group.</summary>
		/// <param name="sides">The side count; values below two are replaced with six.</param>
		public Dice(int sides)
		{
			Sides = sides;
			Count = 1;

			// Parameter checking/overrides
			if (sides <= 1)
			{
				Sides = 6;
			}

		}

		// sides must be 2 or more. If not, it will default to 6.
		// count must be 1 or more. If not, it will default to 1.
		/// <summary>Creates a group of dice of one type.</summary>
		/// <param name="count">The number of dice; values below one are replaced with one.</param>
		/// <param name="sides">The side count; values below two are replaced with six.</param>
		public Dice(int count, int sides)
		{
			Sides = sides;
			Count = count;

			// Parameter checking/overrides
			if (sides <= 1)
			{
				Sides = 6;
			}
			if (count <= 0)
			{
				Count = 1;
			}
		}

		// Properties
		/// <summary>The number of faces on each die; assignments are not validated.</summary>
		public int Sides;
		/// <summary>The number of dice in the group; assignments are not validated.</summary>
		public int Count;
	}
}
