using System;
using System.Collections.Generic;

namespace TravellerTools.Fundamentals
{
	// A table row definition
	public abstract class TableRow
	{
		// Methods
		public abstract bool Matches(int result);
		public abstract List<int> FullRange();

		// Properties
		public string Text = string.Empty;
		public string UID = string.Empty;
	}

	// A single entry table row
	public class TableRowSingle : TableRow
	{
		// Constructor
		public TableRowSingle(int number, string text, string uid)
		{
			Number = number;
			Text = text;
			UID = uid;
		}

		// Methods
		public override bool Matches(int result)
		{
			return (result == Number);
		}

		public override List<int> FullRange()
		{
			List<int> result = new List<int>();
			result.Add(Number);
			return result;
		}

		// Properties
		public int Number;
	}

	//A table row with a value range
	public class TableRowRange : TableRow
	{
		private const string InvalidRangeMessage = "The Start of the range is greater than the End.";

		// Constructor
		// Assumes that start must be <= end
		public TableRowRange(int start, int end, string text, string uid)
		{
			Start = start;
			End = end;
			Text = text;
			UID = uid;
		}

		// Methods

		/// <summary>Checks whether a value lies within the inclusive row range.</summary>
		/// <param name="result">The value to match.</param>
		/// <returns>True if the value is between Start and End, inclusive.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Start exceeds End.</exception>
		public override bool Matches(int result)
		{
			if (Start > End)
			{
				throw new ArgumentOutOfRangeException(nameof(Start), Start, InvalidRangeMessage);
			}

			return ((Start <= result) && (result <= End));
		}

		/// <summary>Enumerates the inclusive row range into a new list.</summary>
		/// <returns>Every integer from Start through End in ascending order.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Start exceeds End, or the range contains more than <see cref="int.MaxValue"/> values.</exception>
		/// <remarks>Large ranges require memory proportional to their number of values.</remarks>
		public override List<int> FullRange()
		{
			if (Start > End)
			{
				throw new ArgumentOutOfRangeException(nameof(Start), Start, InvalidRangeMessage);
			}

			if ((long)End - Start + 1 > int.MaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(End), End, "The range contains too many values for a list.");
			}

			List<int> result = new List<int>();
			// A wider counter can advance beyond Int32.MaxValue without wrapping.
			for (long i = Start; i <= End; i++)
			{
				result.Add((int)i);
			}
			return result;
		}

		// Properties
		public int Start;
		public int End;
	}
}
