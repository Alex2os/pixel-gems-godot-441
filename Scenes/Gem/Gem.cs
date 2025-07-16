using Godot;
using System;

public partial class Gem : Area2D
{
	[Export] float _falling_speed = 80.0f;
	[Export] float _rotation_speed = 0.01f;
	// we can create a signal in this way:
	[Signal] public delegate void OnScoredEventHandler(); // we have to put the name of our signal and then EventHandler, so godot knows it's a signal


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// and we can connect signals inside of here, in the source code, like this:
		AreaEntered += OnAreaEntered; // we are "adding" our function to the AreaEntered list. you must not put ()
									  // should be better to connect the signals inside the code and not from godot itself.

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(0, _falling_speed * (float)delta); // this will make the gem fall in its y value, remember that Y gets bigger the more below you are
		Rotation += _rotation_speed; // just a little rotation effect that I wanted to add
	}

	// we can connect signals that are created from godot in this way:
	private void OnAreaEntered(Area2D area) // we have to manually create the function for our signals, and it must have the exact name that we put into our godot engine.
	{

		GD.Print("Scored!"); // checking if the signal works when the gem enters an area. this will print in godot, as we are using gdscript (GD.Print()).
		EmitSignal(SignalName.OnScored); // in this way we can emit our signal.
		QueueFree(); // this removes the node from the tree at the end of the current frame. so, basically, our gem will disappear once it gets to this function.

	}


}
