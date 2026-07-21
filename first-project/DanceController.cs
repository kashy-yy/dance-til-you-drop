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
	
	// Moves the player has performed
	private List<MoveDirection> playerMoves = new();
	
	// Lets us tell the display to display the dance
	private DanceDisplay danceDisplay;
	
	//floor
	private DanceFloor danceFloor;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Find the floor and display
		danceFloor= GetNode<DanceFloor>("../DanceFloor");
		danceDisplay = GetNode<DanceDisplay>("../DanceDisplay");
		
		//Get the player DanceFloor created
		player = danceFloor.GetPlayer();
		
		// Listen for player movement
		player.MovePerformed += CheckMove;
		
		//Show the first dance
		StartNewPattern();
	}
	
	//This is for the dancemoves that can start anywhere (square move)
	private bool MatchesLoop(List<MoveDirection> playerMoves)
	{
		List<MoveDirection> pattern = currentPattern.Moves;
		
		for (int offset = 0; offset < pattern.Count; offset++)
		{
			bool matches = true;
			
			for (int i = 0; i < playerMoves.Count; i++)
			{
				if (playerMoves[i] != pattern[(i + offset) % pattern.Count])
				{
					matches = false;
					break;
				}
			}
			if (matches)
				return true;
		}
		return false;
	}
	
	private void StartNewPattern()
	{
		currentPattern = danceManager.GetRandomPattern();
		
		currentMoveIndex = 0;
		playerMoves.Clear();
		
		danceFloor.ClearHighlights();
		
		// Highlight where the player currently starts
		Vector2I pos = player.GetGridPosition();
		danceFloor.HighlightTile(pos.X, pos.Y);
		
		danceDisplay.ShowPattern(currentPattern);
	}
	
	private async void CheckMove(MoveDirection move)
	{
		playerMoves.Add(move);
		
		
		if (currentPattern.IsLoop)
		{
			if (!MatchesLoop(playerMoves))
			{
				playerMoves.Clear();
				danceFloor.ClearHighlights();
				return;
			}
			
			// Player made a correct step
			Vector2I pos = player.GetGridPosition();
			danceFloor.HighlightTile(pos.X, pos.Y);
			
			if (playerMoves.Count == currentPattern.Moves.Count)
			{
				danceFloor.ToggleHighlights(false);
				await ToSignal(GetTree().CreateTimer(0.25f), SceneTreeTimer.SignalName.Timeout);
				
				danceFloor.ToggleHighlights(true);
				await ToSignal(GetTree().CreateTimer(0.25f), SceneTreeTimer.SignalName.Timeout);
				
				StartNewPattern();
			}
		}
		else
		{
			if (move == currentPattern.Moves[currentMoveIndex])
			{
				currentMoveIndex++;
				
				Vector2I pos = player.GetGridPosition();
				danceFloor.HighlightTile(pos.X, pos.Y);
			
				if (currentMoveIndex>= currentPattern.Moves.Count)
				{
					danceFloor.ToggleHighlights(false);
					await ToSignal(GetTree().CreateTimer(0.25f), SceneTreeTimer.SignalName.Timeout);
				
					danceFloor.ToggleHighlights(true);
					await ToSignal(GetTree().CreateTimer(0.25f), SceneTreeTimer.SignalName.Timeout);
					
					danceFloor.ToggleHighlights(false);
					StartNewPattern();
				}
			}
			else {
				currentMoveIndex = 0;
				playerMoves.Clear();
				danceFloor.ClearHighlights();
				
				Vector2I pos = player.GetGridPosition();
				danceFloor.HighlightTile(pos.X, pos.Y);
				}
		}
	}
}
