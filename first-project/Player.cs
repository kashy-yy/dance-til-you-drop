using Godot;
using System;

public partial class Player : Node2D
{
	private int currentRow;
	private int currentCol;
	
	private Vector2 tileSize;
	private Vector2 gridStart;
	
	private const int Rows = 9;
	private const int Columns = 10;
	
	private ColorRect rect;
	
	// DanceFloor gives the player the grid information
	public void SetGridData(Vector2 tileSize, Vector2 gridStart, int startRow, int startCol)
	{
		this.tileSize = tileSize;
		this.gridStart = gridStart;
		currentRow = startRow;
		currentCol = startCol;
		
		UpdatePosition();
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rect = GetNode<ColorRect>("ColorRect");
		rect.Size = new Vector2(60, 45);
		rect.Color = new Color(1f, 1f, 1f, 1f);
		
		UpdatePosition();
	}
	
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_right"))
			Move(0,1);
		if (Input.IsActionJustPressed("ui_left"))
			Move(0,-1);
		if(Input.IsActionJustPressed("ui_up"))
			Move(-1,0);
		if(Input.IsActionJustPressed("ui_down"))
			Move(1,0);
	}
	
	private void Move(int rowChange, int colChange)
	{
		int newRow = currentRow + rowChange;
		int newCol = currentCol + colChange;
		
		// Stop player moving outside 9x10 floor
		if (newRow < 0 || newRow >= 9 || newCol < 0 || newCol >=10)
			return;
			
		currentRow = newRow;
		currentCol = newCol;
		
		UpdatePosition();
	}
	
	private void UpdatePosition()
	{
		if (rect == null)
			return;
			
		// Centre the player inside the tile
		float offsetX = (tileSize.X - rect.Size.X) / 2;
		float offsetY = (tileSize.Y - rect.Size.Y) / 2;
		
		Position = new Vector2(
			gridStart.X + currentCol * tileSize.X + offsetX,
			gridStart.Y + currentRow * tileSize.Y + offsetY
		);
	}
}
