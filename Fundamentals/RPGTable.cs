using System;
using System.Collections.Generic;
using System.Linq;

namespace TravellerTools.Fundamentals
{

	// A table definition
	public class RPGTable
	{

		private List<TableRow> rows;

		//Constructor
		public RPGTable()
		{
			rows = new List<TableRow>();
		}

		// Methods

		/// <summary>Adds a row unless its UID is already present.</summary>
		/// <param name="row">The row to add.</param>
		/// <returns>True if added; false if the UID already exists.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="row"/> is null.</exception>
		public bool AddRow(TableRow row)
		{
			ArgumentNullException.ThrowIfNull(row);
			bool exists = rows.Exists(tableRow => row.UID == tableRow.UID);

			if (!exists)
			{
				rows.Add(row);
			}

			return !exists;
		}

		// Returns the row that matches diceRoll, or null if no matches are found
		public TableRow? RollOnTable(int diceRoll)
		{
			TableRow? result = null;
			foreach (TableRow row in rows)
			{
				if (row.Matches(diceRoll))
				{
					result = row;
					break;
				}
			}
			return result;
		}

		// Can throw ArgumentOutofRangeException if any of the RangeRows in rows are not configured correctly.
		// Returns true if there are no number gaps in the table
		// Returns false if there are gaps.
		public bool IsUniqueAndContiguous()
		{
			List<int> fullRange = new List<int>();
			foreach (TableRow row in rows)
			{
				List<int> rowRange = row.FullRange();
				foreach (int entry in rowRange)
				{
					fullRange.Add(entry);
				}
			}
			fullRange.Sort();

			for (int i = 0; i < fullRange.Count - 1; i++)
			{
				if (fullRange[i + 1] != fullRange[i] + 1)
				{
					return false;
				}
			}
			return true;
		}

	}
}
