using System;
using System.Collections.Generic;


namespace TravellerTools.Fundamentals
{

	// A class for providing all dice-rolling needs
	/// <summary>Rolls individual dice or groups and returns their integer totals.</summary>
	/// <remarks>Default overloads use thread-safe system randomness. Overloads accepting IRandomSource support deterministic sources; callers manage those sources' concurrency. Percentile dice return 100 rather than zero. Dice-expression parsing and modifiers are not supported.</remarks>
	public class DiceTools
	{
		private static readonly IRandomSource randomSource = new SystemRandomSource();

		private const string NotEnoughDiceMessage = "Number of dice must be one or more.";
		private const string NotEnoughSidesMessage = "Number of sides must be between two and Int32.MaxValue minus one.";
		private const string NotEnoughDiceManyMessage = "Number of dice must be one or more for all dice.";
		private const string NotEnoughSidesManyMessage = "Number of sides must be between two and Int32.MaxValue minus one for all dice.";

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
			int total = 0;
			for (int i = 0; i < number; i++)
			{
				total = checked(total + RollOneDieTrustedArgs(sides, source));
			}
			return total;
		}

		/// <summary>Rolls one die using the default random source.</summary>
		/// <param name="sides">The number of sides, from 2 through <see cref="int.MaxValue"/> minus one.</param>
		/// <returns>The rolled value, from 1 through <paramref name="sides"/> inclusive.</returns>
		/// <exception cref="ArgumentOutOfRangeException">The side count is outside the supported range.</exception>
		public static int RollOneDie(int sides)
		{
			return RollOneDie(sides, randomSource);
		}

		/// <summary>
		/// Rolls one die using the supplied random source.
		/// </summary>
		/// <param name="sides">The number of sides, from 2 through <see cref="int.MaxValue"/> minus one.</param>
		/// <param name="source">The source of random values.</param>
		/// <returns>The rolled value, from 1 through <paramref name="sides"/> inclusive.</returns>
		/// <exception cref="ArgumentOutOfRangeException">The side count is outside the supported range.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
		public static int RollOneDie(int sides, IRandomSource source)
		{
			// Parameter checking.
			// The exclusive upper bound must fit in the random source's integer contract.
			if (sides <= 1 || sides == int.MaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(sides), sides, NotEnoughSidesMessage);
			}

			return RollOneDieTrustedArgs(sides, source ?? throw new ArgumentNullException(nameof(source)));
		}

		/// <summary>Rolls dice of the same type and returns their sum using the default random source.</summary>
		/// <param name="number">The positive number of dice to roll.</param>
		/// <param name="sides">The number of sides, from 2 through <see cref="int.MaxValue"/> minus one.</param>
		/// <returns>The sum of the individual rolls.</returns>
		/// <exception cref="ArgumentOutOfRangeException">A count is outside its supported range.</exception>
		/// <exception cref="OverflowException">The rolled total exceeds <see cref="int.MaxValue"/>.</exception>
		public static int RollDice(int number, int sides)
		{
			return RollDice(number, sides, randomSource);
		}

		/// <summary>
		/// Rolls multiple dice using the supplied random source.
		/// </summary>
		/// <param name="number">The positive number of dice to roll.</param>
		/// <param name="sides">The number of sides, from 2 through <see cref="int.MaxValue"/> minus one.</param>
		/// <param name="source">The source of random values.</param>
		/// <returns>The sum of the individual rolls.</returns>
		/// <exception cref="ArgumentOutOfRangeException">A count is outside its supported range.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
		/// <exception cref="OverflowException">The rolled total exceeds <see cref="int.MaxValue"/>.</exception>
		/// <remarks>Overflow is detected while rolling; values already consumed from the source are not restored.</remarks>
		public static int RollDice(int number, int sides, IRandomSource source)
		{
			// Parameter checking.
			if (number <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(number), number, NotEnoughDiceMessage);
			}
			if (sides <= 1 || sides == int.MaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(sides), sides, NotEnoughSidesMessage);
			}
			return RollDiceTrustedArgs(number, sides, source ?? throw new ArgumentNullException(nameof(source)));
		}

		/// <summary>Rolls all dice groups using the default random source.</summary>
		/// <param name="handful">Non-null groups with positive counts and side counts from 2 through <see cref="int.MaxValue"/> minus one.</param>
		/// <returns>The sum of all rolls, or zero for an empty collection.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="handful"/> is null.</exception>
		/// <exception cref="ArgumentException">The collection contains a null group.</exception>
		/// <exception cref="ArgumentOutOfRangeException">A group has an unsupported count or side count.</exception>
		/// <exception cref="OverflowException">The rolled total exceeds <see cref="int.MaxValue"/>.</exception>
		public static int RollManyDice(IList<Dice> handful)
		{
			return RollManyDice(handful, randomSource);
		}

		/// <summary>
		/// Rolls each dice group using the supplied random source.
		/// </summary>
		/// <param name="handful">Non-null groups with positive counts and side counts from 2 through <see cref="int.MaxValue"/> minus one.</param>
		/// <param name="source">The source of random values.</param>
		/// <returns>The sum of all rolls, or zero for an empty collection.</returns>
		/// <exception cref="ArgumentNullException">The collection or source is null.</exception>
		/// <exception cref="ArgumentException">The collection contains a null group.</exception>
		/// <exception cref="ArgumentOutOfRangeException">A group has an unsupported count or side count.</exception>
		/// <exception cref="OverflowException">The rolled total exceeds <see cref="int.MaxValue"/>.</exception>
		/// <remarks>All groups are validated before rolling. Overflow does not restore consumed random values.</remarks>
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
				if (handful[i] == null)
				{
					throw new ArgumentException("Dice groups must not be null.", nameof(handful));
				}
				if (handful[i].Sides <= 1 || handful[i].Sides == int.MaxValue)
				{
					throw new ArgumentOutOfRangeException(nameof(handful), NotEnoughSidesManyMessage);
				}
				if (handful[i].Count <= 0)
				{
					throw new ArgumentOutOfRangeException(nameof(handful), NotEnoughDiceManyMessage);
				}
			}

			int total = 0;
			for (int j = 0; j < handful.Count; j++)
			{
				total = checked(total + RollDiceTrustedArgs(handful[j].Count, handful[j].Sides, source));
			}
			return total;
		}

	}




}
