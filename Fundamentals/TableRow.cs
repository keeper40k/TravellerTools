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
		public string Text;
		public string UID;
	}

	// A single entry table row
	public class TableRowSingle : TableRow
	{
		// Constructor
		TableRowSingle(int number, string text, string uid)
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
		private string INVALID_RANGE = "The Start of the range is greater than the End.";

		// Constructor
		// Assumes that start must be <= end
		TableRowRange(int start, int end, string text, string uid)
		{
			Start = start;
			End = end;
			Text = text;
			UID = uid;
		}

		// Methods

		// Can throw ArgumentOutOfRangeException if the table row has a Start > End
		public override bool Matches(int result)
		{
			if (Start > End)
			{
				throw new ArgumentOutOfRangeException(INVALID_RANGE);
			}

			return ((Start <= result) && (result <= End));
		}

		// Can throw ArgumentOutOfRangeException if the table row has a Start > End
		public override List<int> FullRange()
		{
			if (Start > End)
			{
				throw new ArgumentOutOfRangeException(INVALID_RANGE);
			}

			List<int> result = new List<int>();
			for (int i = Start; i <= End; i++)
			{
				result.Add(i);
			}
			return result;
		}

		// Properties
		public int Start;
		public int End;
	}
}