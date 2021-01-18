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
		RPGTable()
		{
			rows = new List<TableRow>();
		}

		// Methods

		// Returns true if the row is added
		// Returns false if the row already existings in the table.
		//   This is checked my matching the UID
		public bool AddRow(TableRow row)
		{
			bool exists = false;
			foreach (TableRow tableRow in rows)
			{
				exists = exists && (row.UID == tableRow.UID);
			}

			if (!exists)
			{
				rows.Add(row);
			}

			return exists;
		}

		// Returns the row that matches diceRoll, or null if no matches are found
		public TableRow RollOnTable(int diceRoll)
		{
			TableRow result = null;
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

			bool result = true;
			for (int i = 0; (i < (fullRange.Count - 1)) && (result = true); i++)
			{
				result = result && (fullRange[i] == fullRange[i + 1]);
			}
			return result;
		}

	}
}