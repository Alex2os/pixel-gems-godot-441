using Godot;
using System;

public partial class Game : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnScored() // in this way we can connect to a signal that we made. remember to put parameters if something is being sended from the signal
	{

		GD.Print("Entered Game.cs!");

	}

}
