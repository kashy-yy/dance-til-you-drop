using Godot;
using System;

public partial class DanceFloor : Node2D
{
	private PackedScene floorTileScene = GD.Load<PackedScene>("res://FloorTile.tscn");
	private PackedScene playerScene = GD.Load<PackedScene>("res://Player.tscn");
	
	// Generates a random number for the tiles colour
	private readonly RandomNumberGenerator rng = new RandomNumberGenerator();
	
	// Numbers of rows and columns in the dance floor grid
	private const int Rows = 9;
	private const int Columns = 10;
	
	//2D array storing all floor tiles
	private FloorTile[,] tiles = new FloorTile[Rows, Columns];
	
	//Size of each tile in pixels
	private readonly Vector2 tileSize = new Vector2(125, 100);
	
	// Stores the colour index of each tile
	private int[,] colourGrid = new int[Rows, Columns];
	
	// Array of possible tile colours
	private readonly Color[] danceColours =
	{
		new Color(1f, 0f, 1f), // pink
		new Color(0f, 1f, 1f), // cyan
		new Color(1f, 1f, 0f), // yellow
		new Color(0.5f, 0f, 1f), // purple
	};
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rng.Randomize();
		GenerateFloor();
		SpawnPlayer();
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
				tile.Position = new Vector2(
					col * tileSize.X,
					row * tileSize.Y
				);
				
				// Set the visible size of the tile
				tile.SetTileSize(tileSize);
				
				// Set the colour of the tile ensuring no adjacent tiles are the same
				Color colour = ChooseColour(row, col);
				tile.SetColour(colour);
				
				// Add tile to the scene tree and store it in the array
				AddChild(tile);
				tiles[row,col] = tile;
			}
		}
	}
	
	private void SpawnPlayer()
	{
		Player player = playerScene.Instantiate<Player>();
		AddChild(player);
		
		// Start player on tile row 0, column 0
		player.SetGridData(tileSize, 0, 0);
	}
	
	// Chooses a random colour for a tile while making sure it
	// does not match the tile above or to the left
	private Color ChooseColour(int row, int col)
	{
		int colourIndex;
		
		// Keep choosing a random colour until we find one that is
		// different from the neighbouring tiles
		do
		{
			colourIndex = rng.RandiRange(0, danceColours.Length -1);
		}
		while (
			// Check the tile to the left (if it exists)
			(col > 0 && colourIndex == colourGrid[row, col-1]) ||
			
			// Check the tile above (if it exists)
			(row > 0 && colourIndex == colourGrid[row-1, col])
		);
		
		// Store the chosen colour so future tiles can compare against it
		colourGrid[row,col] = colourIndex;
		
		// return the actual colour from the colour array
		return danceColours[colourIndex];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
