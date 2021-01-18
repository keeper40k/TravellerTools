using System;
using System.Collections.Generic;


namespace TravellerTools.Fundamentals
{

	// A class for providing all dice-rolling needs
	public class DiceTools
	{
		private static Random m_rand;

		private static string NOT_ENOUGH_DICE = "Number of dice must be one or more.";
		private static string NOT_ENOUGH_SIDES = "Number of sides must be two or more.";
		private static string NOT_ENOUGH_DICE_MANY = "Number of dice must be one or more for all dice.";
		private static string NOT_ENOUGH_SIDES_MANY = "Number of sides must be two or more for all dice.";

		//Constructor
		static DiceTools()
		{
			m_rand = new Random();
		}

		// Assumes sides must be 2 or more.
		// Returns the result of one die roll
		private static int RollOneDieTrustedArgs(int sides)
		{
			return m_rand.Next(1, sides + 1);
		}


		// Assumes number must be 1 or more.
		// Assumes sides must be 2 or more.
		// Returns the total of all of the dice rolled
		private static int RollDiceTrustedArgs(int number, int sides)
		{
			// Parameter checking.

			int total = 0;
			for (int i = 1; i <= number; i++)
			{
				total += RollOneDieTrustedArgs(sides);
			}
			return total;
		}

		// sides must be 2 or more.
		// Can throw an ArgumentOutofRangeException
		// Returns the result of one die roll
		public static int RollOneDie(int sides)
		{
			// Parameter checking.
			if (sides <= 1)
			{
				throw new ArgumentOutOfRangeException(NOT_ENOUGH_SIDES);
			}

			return RollOneDieTrustedArgs(sides);
		}

		// number must be 1 or more.
		// sides must be 2 or more.
		// Can throw an ArgumentOutofRangeException
		// Returns the total of all of the dice rolled.
		public static int RollDice(int number, int sides)
		{
			// Parameter checking.
			if (number <= 0)
			{
				throw new ArgumentOutOfRangeException(NOT_ENOUGH_DICE);
			}
			if (sides <= 1)
			{
				throw new ArgumentOutOfRangeException(NOT_ENOUGH_SIDES);
			}
			int total = 0;
			for (int i = 1; i <= number; i++)
			{
				total += RollOneDieTrustedArgs(sides);
			}
			return total;
		}

		// For each element of Dice in handful, the following must apply:
		//   number must be 1 or more.
		//   sides must be 2 or more.
		// Can throw an ArgumentOutofRangeException
		// Returns the total of all of the dice rolled.
		public static int RollManyDice(IList<Dice> handful)
		{
			// Parameter checking
			for (int i = 1; i <= handful.Count; i++)
			{
				if (handful[i].Sides <= 1)
				{
					throw new ArgumentOutOfRangeException(NOT_ENOUGH_SIDES_MANY);
				}
				if (handful[i].Count <= 0)
				{
					throw new ArgumentOutOfRangeException(NOT_ENOUGH_DICE_MANY);
				}
			}

			int total = 0;
			for (int j = 1; j <= handful.Count; j++)
			{
				total += RollDiceTrustedArgs(handful[j].Sides, handful[j].Count);
			}
			return total;
		}

	}




}