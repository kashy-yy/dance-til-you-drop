using Godot;
using System;

public partial class FloorTile : Node2D
{
	private ColorRect rect;
	
	// Stores the current state of the tile.
	// Other classes can read the tile's state, but only FloorTile can change it.
	// The tile starts in the Off state by default.
	public TileState State {get; private set;} = TileState.Off;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rect = GetNode<ColorRect>("ColorRect");
		UpdateVisual();
	}
	
	// Sets the size of this tile by resizing its ColorRect child.
	public void setTileSize(Vector2 size)
	{
		if (rect == null)
			rect = GetNode<ColorRect>("ColorRect");
			
		rect.Size = size;
	}
	
	// Changes the tile's state and updates its colour
	public void SetState(TileState newState)
	{
		State = newState;
		UpdateVisual();
	}
	
	// Updates the tile's colour based on its current state.
	private void UpdateVisual()
	{
		if (rect == null) return;
		
		switch (State)
		{
			case TileState.Off:
				rect.Color = new Color(0.816f, 0.0f, 1.0f, 1.0f);
				break;
			case TileState.Safe:
				rect.Color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
				break;
			case TileState.Warning:
				rect.Color = new Color(1.0f, 0.361f, 0.043f, 1.0f);
				break;
			case TileState.Danger:
				rect.Color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
