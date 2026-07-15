using Godot;
using System;

public partial class Player : Node2D
{
	private int currentRow;
	private int currentCol;
	
	private Vector2 tileSize;
	
	private const int Rows = 9;
	private const int Columns = 10;
	
	private ColorRect rect;
	
	// Delay before repeated movement starts when holding a key
	private const float InitialRepeatDelay = 0.50f;
	
	// Time between moves when holding a key
	private const float RepeatDelay = 0.12f;
	
	// Stores the direction currently being held
	private Vector2I heldDirection = Vector2I.Zero;
	
	// Timer used for repeated movement
	private float moveTimer = 0f;
	
	// True once the initial delay has passed
	private bool repeating = false;
	
	// DanceFloor gives the player the grid information
	public void SetGridData(Vector2 tileSize, int startRow, int startCol)
	{
		this.tileSize = tileSize;
		
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
		Vector2I direction = GetHeldDirection();
		
		// No key is being held
		if (direction == Vector2I.Zero)
		{
			heldDirection = Vector2I.Zero;
			moveTimer= 0;
			repeating = false;
			return;
		}
		
		// A new key has just been pressed
		if (direction != heldDirection)
		{
			heldDirection = direction;
			moveTimer = 0;
			repeating = false;
			
			Move(direction.Y, direction.X);
			return;
		}
		
		moveTimer += (float)delta;
		
		// Wait before repeating
		if (!repeating)
		{
			if (moveTimer >= InitialRepeatDelay)
			{
				Move(direction.Y, direction.X);
				moveTimer = 0;
				repeating = true;
			}
		}
		else
		{
			if (moveTimer >RepeatDelay)
			{
				Move(direction.Y, direction.X);
				moveTimer = 0;
			}
		}
	}
	
	// Returns the direction of the key currently being held
	private Vector2I GetHeldDirection()
	{
		if (Input.IsActionPressed("ui_right"))
			return Vector2I.Right;
		
		if (Input.IsActionPressed("ui_left"))
			return Vector2I.Left;
			
		if (Input.IsActionPressed("ui_up"))
			return Vector2I.Up;
		
		if (Input.IsActionPressed("ui_down"))
			return Vector2I.Down;
			
		return Vector2I.Zero;
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
			currentCol * tileSize.X + offsetX,
			currentRow * tileSize.Y + offsetY
		);
	}
}
