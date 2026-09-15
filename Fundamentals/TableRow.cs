using System;
using System.Collections.Generic;

namespace TravellerTools.Fundamentals;

/// <summary>Defines matching and value enumeration for an outcome in a roll table.</summary>
/// <remarks>Implementations should enumerate exactly the integer values accepted by Matches. Rows and their identifiers remain mutable after insertion.</remarks>
public abstract class TableRow
{
    // Methods
    /// <summary>Determines whether this row accepts a resolved roll total.</summary>
    /// <param name="result">The integer total to match.</param>
    /// <returns>True when this row accepts the value; otherwise false.</returns>
    public abstract bool Matches(int result);
    /// <summary>Enumerates the integer values represented by this row.</summary>
    /// <returns>A list of values accepted by the row's matching rule.</returns>
    public abstract List<int> FullRange();

    // Properties
    /// <summary>The outcome text associated with a matching roll; initially empty.</summary>
    public string Text = string.Empty;
    /// <summary>The identifier used by RPGTable to detect duplicates when adding a row; initially empty.</summary>
    /// <remarks>Changing the identifier after insertion does not trigger duplicate validation.</remarks>
    public string UID = string.Empty;
}

/// <summary>Represents a table outcome matched by exactly one integer.</summary>
public class TableRowSingle : TableRow
{
    // Constructor
    /// <summary>Creates an outcome for one integer roll total.</summary>
    /// <param name="number">The matching value; any integer is supported.</param>
    /// <param name="text">The outcome text.</param>
    /// <param name="uid">The identifier used for duplicate detection when inserted into a table.</param>
    public TableRowSingle(int number, string text, string uid)
    {
        Number = number;
        Text = text;
        UID = uid;
    }

    // Methods
    /// <summary>Checks equality with the row's current Number.</summary>
    /// <param name="result">The resolved roll total.</param>
    /// <returns>True if the value equals Number; otherwise false.</returns>
    public override bool Matches(int result)
    {
        return (result == Number);
    }

    /// <summary>Creates a list containing the row's matching value.</summary>
    /// <returns>A new single-element list containing Number.</returns>
    public override List<int> FullRange()
    {
        List<int> result = new();
        result.Add(Number);
        return result;
    }

    // Properties
    /// <summary>The integer roll total that matches this row.</summary>
    public int Number;
}

/// <summary>Represents a table outcome matched by an inclusive integer range.</summary>
/// <remarks>Bounds are mutable and validated when matching or enumerating, rather than during construction.</remarks>
public class TableRowRange : TableRow
{
    // Matching and enumeration report the same invalid-bound contract.
    private const string InvalidRangeMessage = "The Start of the range is greater than the End.";

    // Constructor
    // Assumes that start must be <= end
    /// <summary>Creates an outcome for an inclusive range without immediately validating its bounds.</summary>
    /// <param name="start">The inclusive lower bound; must not exceed end when the row is used.</param>
    /// <param name="end">The inclusive upper bound.</param>
    /// <param name="text">The outcome text.</param>
    /// <param name="uid">The identifier used for duplicate detection when inserted into a table.</param>
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

        List<int> result = new();
        // A wider counter can advance beyond Int32.MaxValue without wrapping.
        for (long i = Start; i <= End; i++)
        {
            result.Add((int)i);
        }
        return result;
    }

    // Properties
    /// <summary>The inclusive lower bound; must not exceed End when matching or enumerating.</summary>
    public int Start;
    /// <summary>The inclusive upper bound; enumeration additionally requires a representable list length.</summary>
    public int End;
}
