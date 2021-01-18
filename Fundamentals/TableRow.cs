using System;

// A table row definition
public abstract class TableRow
{
	// Methods
	public abstract bool Matches( int result );
	public abstract List<int> FullRange();

	// Properties
	string Text;
	string UID;
}

// A single entry table row
public class TableRowSingle : TableRow
{
	TableRow( int number, string text, string uid )
	{
		Number = number;
		Text = text;
		UID = uid;
	}

	// Methods
	public bool Matches( int result )
	{
		return (result == Number);
	}

	public List<int> FullRange()
	{
		List<int> result = new List<int>();
		result.add( Number );
		return result;
	}

	// Properties
	int Number;
}

A table row with a value range
public class TableRowRange : TableRow
{
	private string INVALID_RANGE = "The Start of the range is greater than the End.";

	// Constructor
	// Assumes that start must be <= end
	TableRowRange( int start, int end, string text, string uid )
	{
		Start = start;
		End = end;
		Text = text;
		UID = uid;
	}

	// Methods

	// Can throw ArgumentOutofRangeException if the table row has a Start > End
	public bool Matches( int result )
	{
		if( start > end )
		{
			throw new ArgumentOutofRangeException( INVALID_RANGE );
		}

		return ( (start <= result) && ( result <= end ) ); 
	}

	// Can throw ArgumentOutofRangeException if the table row has a Start > End
	public List<int> FullRange()
	{
		if( start > end )
		{
			throw new ArgumentOutofRangeException( INVALID_RANGE );
		}

		List<int> result = new List<int>();
		for( int i = Start ; i <= End ; i++ )
		{
			result.add( i );
		}
		return result;
	}

	// Properties
	int Start;
	int End;
}