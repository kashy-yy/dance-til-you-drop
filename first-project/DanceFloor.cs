using Godot;
using System;

public partial class DanceFloor : Node2D
{
	private PackedScene floorTileScene = GD.Load<PackedScene>("res://FloorTile.tscn");
	
	// Numbers of rows and columns in the dance floor grid
	private const int Rows = 9;
	private const int Columns = 10;
	
	//2D array storing all floor tiles
	private FloorTile[,] tiles = new FloorTile[Rows, Columns];
	
	//Size of each tile in pixels
	public Vector2 tileSize = new Vector2(125, 100);
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GenerateFloor();
	}
	
	// Creates the dance floor grid
	private void GenerateFloor()
	{
		for (int row = 0; row < Rows; row++)
		{
			for (int col = 0; col < Columns; col++)
			{
				FloorTile tile = floorTileScene.Instantiate<FloorTile>();
				
				// Position tile in a grid layout
				tile.Position = new Vector2(col*tileSize.X, row*tileSize.Y);
				
				// Set the visible size of the tile
				tile.setTileSize(tileSize);
				
				// Add tile to the scene tree and store it in the array
				AddChild(tile);
				tiles[row,col] = tile;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
