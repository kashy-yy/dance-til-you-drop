using Godot;
using System.Collections.Generic;

public partial class DanceDisplay : Node2D
{
	private Node2D display;
		private readonly RandomNumberGenerator rng = new();
	
	// Size of every preview square
	private const int PreviewTileSize = 70;
	
	// Distance between the top-left corner of neighbouring preview squares
	private const int PreviewTileSpacing = 90;
	
	// Arrow image to show the direction between each square
	private readonly Texture2D arrowTexture = GD.Load<Texture2D>("res://assets/sprites/arrow.png");
	
	// Arrow size
	private static readonly Vector2 ArrowScale = new Vector2(0.09f, 0.09f);
	
	// Colours the dance preview can use
	private readonly Color[] previewColours =
	{
		new Color(0f, 1f, 1f), //Cyan
		new Color(1f, 1f, 0f), //Yellow
		new Color(0.5f, 0f, 1f), //Purple
		new Color(1f, 0f, 1f), //pink
	};
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		display = GetNode<Node2D>("Display");
		rng.Randomize();
		
		DanceManager manager = new DanceManager();
		ShowPattern(manager.GetRandomPattern());
	}
	
	public void ShowPattern(DancePattern pattern)
	{
		Color previewColour = previewColours[rng.RandiRange(0, previewColours.Length-1)];
		
		ClearDisplay();
		
		// Start in the centre of the preview
		Vector2I currentPos = Vector2I.Zero;
		
		// Stores every square that makes up the dance shape
		List<Vector2I> positions = new()
		{
			currentPos
		};
		
		// Follow each movement in the dance pattern
		foreach (MoveDirection move in pattern.Moves)
		{
			switch (move)
			{
				case MoveDirection.Up:
					currentPos += Vector2I.Up;
					break;
					
				case MoveDirection.Down:
					currentPos += Vector2I.Down;
					break;
					
				case MoveDirection.Left:
					currentPos += Vector2I.Left;
					break;
					
				case MoveDirection.Right:
					currentPos += Vector2I.Right;
					break;
			}
			// Store the new position after moving
			positions.Add(currentPos);
		}
		
		// Find the smallest X and Y values
		// This lets us shift the whole shape so it starts at (0,0)
		int minX = positions[0].X;
		int minY = positions[0].Y;
		
		foreach (Vector2I pos in positions)
		{
			if (pos.X < minX)
				minX = pos.X;
				
			if (pos.Y < minY)
				minY = pos.Y;
		}
		
		foreach (Vector2I pos in positions)
		{
			ColorRect square = new ColorRect();
			
			square.Size = new Vector2(PreviewTileSize, PreviewTileSize);
			square.Color = previewColour;
			
			// Shift every square so that the top-left one begins at (0,0)
			square.Position = new Vector2(
				(pos.X - minX)* PreviewTileSpacing,
				(pos.Y - minY)* PreviewTileSpacing
			);
			
			display.AddChild(square);
		}
		//Draw an arrow between every pair of neighbouring squares
		for (int i = 0; i < pattern.Moves.Count; i++)
		{
			DrawArrow(
				positions[i],
				positions[i+1],
				pattern.Moves[i],
				minX,
				minY
			);
		}
	}
	
	// Helper method to clear previous pattern
	private void ClearDisplay()
	{
		foreach (Node child in display.GetChildren())
		{
			child.QueueFree();
		}
	}
	
	//Draws arrows between two preview squares
	private void DrawArrow(
		Vector2I from,
		Vector2I to,
		MoveDirection direction,
		int minX,
		int minY
	)
	{
		Sprite2D arrow = new Sprite2D();
		
		// Give the sprite our arrow image
		arrow.Texture = arrowTexture;
		arrow.Scale = ArrowScale;
		arrow.Centered = true;
		
		// Work out where the two squares are on the screen
		Vector2 start = new Vector2(
			(from.X - minX) * PreviewTileSpacing + PreviewTileSize/ 2f,
			(from.Y - minY) * PreviewTileSpacing + PreviewTileSize/ 2f
		);
		
		Vector2 end = new Vector2(
			(to.X - minX) * PreviewTileSpacing +PreviewTileSize / 2f,
			(to.Y - minY) * PreviewTileSpacing + PreviewTileSize/ 2f
		);
		
		// Place an arrow halfway between the two squares
		arrow.Position = (start+end) /2;
		
		// Rotate the arrow so it points in the correct direction
		switch (direction)
		{
			case MoveDirection.Right:
				arrow.RotationDegrees = 0;
				break;
				
			case MoveDirection.Down:
				arrow.RotationDegrees=90;
				break;
			
			case MoveDirection.Left:
				arrow.RotationDegrees = 180;
				break;
				
			case MoveDirection.Up:
				arrow.RotationDegrees = 270;
				break;
		}
		// Make arrow appear above the squares
			arrow.ZIndex = 1;
			
			display.AddChild(arrow);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
