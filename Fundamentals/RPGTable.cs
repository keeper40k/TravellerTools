using System;
using System.Collections.Generic;

namespace TravellerTools.Fundamentals;

/// <summary>Resolves integer roll totals against rows in insertion order.</summary>
/// <remarks>Rows are retained by reference and remain mutable. Overlapping ranges are allowed; lookup returns the first match. Call IsUniqueAndContiguous to check gaps and overlaps. Callers must synchronize concurrent mutation.</remarks>
public class RPGTable
{
    // Keep insertion order because the first matching row determines the outcome.
    private readonly List<TableRow> rows;

    //Constructor
    /// <summary>Creates an empty roll table.</summary>
    public RPGTable()
    {
        rows = new();
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

    /// <summary>Finds the first row whose matching rule accepts a roll total.</summary>
    /// <param name="diceRoll">The already-resolved integer total; this method does not roll dice.</param>
    /// <returns>The first matching row by reference, or null when no row matches.</returns>
    /// <remarks>Rows are evaluated in insertion order; row validation exceptions propagate.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">A range row encountered during lookup has Start greater than End.</exception>
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

    /// <summary>Checks whether the values enumerated by all rows form a gap-free sequence without duplicates.</summary>
    /// <returns>True for a unique contiguous sequence, including an empty table or a single value; otherwise false.</returns>
    /// <remarks>This checks continuity between the smallest and largest listed values, not coverage of an external dice range. It materializes and sorts all values, so large tables require proportional memory.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">A range row has reversed bounds or a range too large for its list representation.</exception>
    public bool IsUniqueAndContiguous()
    {
        List<int> fullRange = new();
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
