using Godot;
using System;
using System.Collections.Generic;

public class DanceManager
{
	private readonly RandomNumberGenerator rng = new();
	
	private List<DancePattern> dancePatterns = new()
	{
		new DancePattern
		{
			IsLoop = true,
			Moves = new()
			{
				MoveDirection.Right,
				MoveDirection.Down,
				MoveDirection.Left,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			IsLoop = true,
			Moves = new()
			{
				MoveDirection.Left,
				MoveDirection.Down,
				MoveDirection.Right,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Right,
				MoveDirection.Up,
				MoveDirection.Right
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Left,
				MoveDirection.Down,
				MoveDirection.Left,
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Right,
				MoveDirection.Down,
				MoveDirection.Right
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Left,
				MoveDirection.Up,
				MoveDirection.Left,
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Down,
				MoveDirection.Down,
				MoveDirection.Down,
				MoveDirection.Right
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Left,
				MoveDirection.Up,
				MoveDirection.Up,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Right,
				MoveDirection.Up,
				MoveDirection.Up,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Down,
				MoveDirection.Down,
				MoveDirection.Down,
				MoveDirection.Left
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Right,
				MoveDirection.Down,
				MoveDirection.Up,
				MoveDirection.Right
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Left,
				MoveDirection.Down,
				MoveDirection.Up,
				MoveDirection.Left
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Right,
				MoveDirection.Right,
				MoveDirection.Right,
				MoveDirection.Right
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Left,
				MoveDirection.Left,
				MoveDirection.Left,
				MoveDirection.Left
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Down,
				MoveDirection.Right,
				MoveDirection.Down
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Up,
				MoveDirection.Left,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Down,
				MoveDirection.Left,
				MoveDirection.Down,
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Up,
				MoveDirection.Right,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Right,
				MoveDirection.Right,
				MoveDirection.Right,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Down,
				MoveDirection.Left,
				MoveDirection.Left,
				MoveDirection.Left
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Down,
				MoveDirection.Right,
				MoveDirection.Right,
				MoveDirection.Right
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Left,
				MoveDirection.Left,
				MoveDirection.Left,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Down,
				MoveDirection.Left,
				MoveDirection.Right,
				MoveDirection.Down
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Up,
				MoveDirection.Left,
				MoveDirection.Right,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Down,
				MoveDirection.Right,
				MoveDirection.Left,
				MoveDirection.Down
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Up,
				MoveDirection.Right,
				MoveDirection.Left,
				MoveDirection.Up
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Left,
				MoveDirection.Up,
				MoveDirection.Down,
				MoveDirection.Left
			}
		},
		new DancePattern
		{
			Moves = new()
			{
				MoveDirection.Right,
				MoveDirection.Up,
				MoveDirection.Down,
				MoveDirection.Right
			}
		},
	};
	
	public DanceManager()
	{
		rng.Randomize();
	}
	
	public DancePattern GetRandomPattern()
	{
		return dancePatterns[rng.RandiRange(0, dancePatterns.Count-1)];
	}
}
