using Godot;
using System;

public partial class Gem : Area2D
{
	[Export] float _falling_speed = 80.0f;
	[Export] float _rotation_speed = 0.01f;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(0, _falling_speed * (float)delta); // this will make the gem fall in its y value, remember that Y gets bigger the more below you are
		Rotation += _rotation_speed; // just a little rotation effect that I wanted to add
	}
}
