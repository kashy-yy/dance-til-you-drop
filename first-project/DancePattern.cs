using Godot;
using System;
using System.Collections.Generic;

public class DancePattern
{
	public List<MoveDirection> Moves = new();
	
	// True if this pattern can begin from any point
	public bool IsLoop = false;
}
