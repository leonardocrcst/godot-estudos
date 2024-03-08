using Godot;
using System;

public partial class World : TileMap
{
	private Vector2 map;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GroundBuilder();
	}

	private void GroundBuilder()
	{
		var from = new Vector2I(-20, -20);
		var to = new Vector2I(20, 20);
		DrawTitle(0, from, to);

		DrawIsland();
	}

	private void DrawIsland()
	{
		var from = new Vector2I(-2, -2);
		var to = new Vector2I(2, 2);
		DrawTitle(42, from, to);
		for (var i = -2; i <= 2; i++) {
			var axis = new Vector2I(-2, i);
			DrawTopLeftBorder(42, axis);
		}
	}

	private void DrawTitle(int source, Vector2I from, Vector2I to)
	{
		var tile = new Vector2I(0, 1);
		for (var x = from.X; x <= to.X; x++) {
			for (var y = from.Y; y <= to.Y; y++) {
				var position = new Vector2I(x, y);
				SetCell(0, position, source, tile);
			}
		}
	}

	private void DrawTopLeftBorder(int source, Vector2I tile)
	{
		var common = new Vector2I(1, 1);
		var left = new Vector2I(2, 0);
		var right = new Vector2I(2, 3);
		var leftCell = new Vector2I(tile.X - 1, tile.Y + 1);
		var rightCell = new Vector2I(tile.X - 1, tile.Y + 1);
		var topCell = new Vector2I(tile.X - 1, tile.Y);
		if (GetCellSourceId(0, leftCell) != source && GetCellSourceId(0, rightCell) != source) {
			SetCell(0, topCell, source, common);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
