using Godot;
using System.Collections.Generic;

public partial class DanceController:Node2D
{
	private Player player;
	
	//Knows every possible dance
	private DanceManager danceManager = new DanceManager();
	
	// Dance the player is supposed to currently perform
	private DancePattern currentPattern;
	
	//Which move of the currentPattern we're waiting for
	private int currentMoveIndex = 0;
	
	// Lets us tell the display to display the dance
	private DanceDisplay danceDisplay;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Find the floor and display
		DanceFloor floor = GetNode<DanceFloor>("../DanceFloor");
		danceDisplay = GetNode<DanceDisplay>("../DanceDisplay");
		
		//Get the player DanceFloor created
		player = floor.GetPlayer();
		
		// Listen for player movement
		player.MovePerformed += CheckMove;
		
		//Show the first dance
		StartNewPattern();
	}
	
	private void StartNewPattern()
	{
		currentPattern = danceManager.GetRandomPattern();
		currentMoveIndex = 0;
		danceDisplay.ShowPattern(currentPattern);
	}
	
	private void CheckMove(MoveDirection move)
	{
		if (move == currentPattern.Moves[currentMoveIndex])
		{
			currentMoveIndex++;
			
			if (currentMoveIndex>= currentPattern.Moves.Count)
			{
				StartNewPattern();
			}
		}
		else {
			currentMoveIndex = 0;
		}
	}
}
