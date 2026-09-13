using System;
using System.Collections.Generic;


namespace TravellerTools.Fundamentals
{

	// A class for providing all dice-rolling needs
	public class DiceTools
	{
		private static readonly IRandomSource randomSource = new SystemRandomSource();

		private const string NotEnoughDiceMessage = "Number of dice must be one or more.";
		private const string NotEnoughSidesMessage = "Number of sides must be two or more.";
		private const string NotEnoughDiceManyMessage = "Number of dice must be one or more for all dice.";
		private const string NotEnoughSidesManyMessage = "Number of sides must be two or more for all dice.";

		// Assumes sides must be 2 or more.
		// Returns the result of one die roll
		private static int RollOneDieTrustedArgs(int sides, IRandomSource source)
		{
			return source.Next(1, sides + 1);
		}


		// Assumes number must be 1 or more.
		// Assumes sides must be 2 or more.
		// Returns the total of all of the dice rolled
		private static int RollDiceTrustedArgs(int number, int sides, IRandomSource source)
		{
			// Parameter checking.

			int total = 0;
			for (int i = 1; i <= number; i++)
			{
				total += RollOneDieTrustedArgs(sides, source);
			}
			return total;
		}

		// sides must be 2 or more.
		// Can throw an ArgumentOutofRangeException
		// Returns the result of one die roll
		public static int RollOneDie(int sides)
		{
			return RollOneDie(sides, randomSource);
		}

		/// <summary>
		/// Rolls one die using the supplied random source.
		/// </summary>
		public static int RollOneDie(int sides, IRandomSource source)
		{
			// Parameter checking.
			if (sides <= 1)
			{
				throw new ArgumentOutOfRangeException(NotEnoughSidesMessage);
			}

			return RollOneDieTrustedArgs(sides, source ?? throw new ArgumentNullException(nameof(source)));
		}

		// number must be 1 or more.
		// sides must be 2 or more.
		// Can throw an ArgumentOutofRangeException
		// Returns the total of all of the dice rolled.
		public static int RollDice(int number, int sides)
		{
			return RollDice(number, sides, randomSource);
		}

		/// <summary>
		/// Rolls multiple dice using the supplied random source.
		/// </summary>
		public static int RollDice(int number, int sides, IRandomSource source)
		{
			// Parameter checking.
			if (number <= 0)
			{
				throw new ArgumentOutOfRangeException(NotEnoughDiceMessage);
			}
			if (sides <= 1)
			{
				throw new ArgumentOutOfRangeException(NotEnoughSidesMessage);
			}
			return RollDiceTrustedArgs(number, sides, source ?? throw new ArgumentNullException(nameof(source)));
		}

		// For each element of Dice in handful, the following must apply:
		//   number must be 1 or more.
		//   sides must be 2 or more.
		// Can throw an ArgumentOutofRangeException
		// Returns the total of all of the dice rolled.
		public static int RollManyDice(IList<Dice> handful)
		{
			return RollManyDice(handful, randomSource);
		}

		/// <summary>
		/// Rolls each dice group using the supplied random source.
		/// </summary>
		public static int RollManyDice(IList<Dice> handful, IRandomSource source)
		{
			if (handful == null)
			{
				throw new ArgumentNullException(nameof(handful));
			}

			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}
			// Parameter checking
			for (int i = 0; i < handful.Count; i++)
			{
				if (handful[i].Sides <= 1)
				{
					throw new ArgumentOutOfRangeException(NotEnoughSidesManyMessage);
				}
				if (handful[i].Count <= 0)
				{
					throw new ArgumentOutOfRangeException(NotEnoughDiceManyMessage);
				}
			}

			int total = 0;
			for (int j = 0; j < handful.Count; j++)
			{
				total += RollDiceTrustedArgs(handful[j].Count, handful[j].Sides, source);
			}
			return total;
		}

	}




}