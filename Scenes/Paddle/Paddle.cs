using Godot;
using System;

public partial class Paddle : Area2D
{
	[Export] float _speed = 100.0f; // we use this [Export] thing to allow the inspector inside of Godot to see the variable, which in this case is _speed
	[Export] int left_margin = 40;
	[Export] int right_margin = 255;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{



	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) // main loop of the game
	{
		Rect2 vpr = GetViewportRect(); // we obtain the viewport and all of its properties inside of a variable.

		if (Input.IsActionPressed("right")) // we assign these in the projects options
		{ 
			if (Position.X >= vpr.End.X - right_margin) ; // limitation of the right side. in this case we use the viewport.End to get the end of it, and using its X value we can delimit the area.
			else Position += new Vector2(_speed * (float)delta, 0); // we use the Position relative to the node's parent, to increment or decrease it
		}

		if (Input.IsActionPressed("left")) // we assign these in the projects options
		{
			if (Position.X <= left_margin) ; // limitation of left side.
			else Position -= new Vector2(_speed * (float)delta, 0); // we do the same for the left.
		}


	}
}
