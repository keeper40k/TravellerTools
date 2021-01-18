using System;

namespace TravellerTools.Fundamentals
{

	// A dice definition for one dice, or a handful of dice of the same type
	public class Dice
	{

		// Constructors

		// sides must be 2 or more. If not, it will default to 6.
		Dice(int sides)
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
		Dice(int count, int sides)
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
		public int Sides;
		public int Count;
	}
}