using Godot;
using System;

public partial class Game : Node2D
{

	// [Export] private Gem _gem; // we can export the variable to the godot ide so we can modify it inside of there.
	// [Export] private NodePath _gemPath; // or we can declare a path to our object, in this case the gem path. and we can put that in the "hard-coded" part instead of just putting a string inside the ().
	// private Gem _gem // we have to declare this too.
	// generally, should be better to use the nodepaths in case we change the type of node of one that we have inside godot. but just using the not-commented line should be fine
	[Export] private int _gem_margin = 30;
	[Export] private PackedScene _gemScene; // our gem scene, so this is where we can start generating the gems.
	[Export] private NodePath _timerPath; // path for the timer node
	private Timer _timer; // the node itself

	private int _score = 0; // we create a variable for the score in-game

	[Export] private NodePath _scoreLabelPath; // reference to our path to the label in godot
	private Label _scoreLabel; // variable for the label

	[Export] private NodePath _musicPath; // reference to the path of the music node
	private AudioStreamPlayer _music; // a variable for the music node

	[Export] private NodePath _effectPlayingPath; // assigns for the scoring effect path and variable. we can add more sounds using just one variable and / or nodepath, as we can see below in the code to write less lines and optimize the code / game. notice now it has a 2D at the end, so we know its positional in space (we can hear it from a direction with headphones)
	private AudioStreamPlayer2D _effectPlaying;

	// [Export] private AudioStream _gameOverEffect; // here we can import to the code the reference to another sound effect. see the gameover function to see how to use it.
	private static readonly AudioStream _gameOverEffect = GD.Load<AudioStream>("res://assets/explode.wav"); // another way to import assets or sources into the code is by doing this; using res:// as the dir, and then the path to it. similarly to how it's done in allegro 5.
	// the use of static and readonly is to better performance of the game, so that this asset, as soon as it's loaded, will not be called again, and we will have access to this resource.

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// it is not recommended to hard-code the line below this one, so that's why we export the variable outside to godot ide.
		// Gem gem = GetNode<Gem>("Gem"); // getting or referencing a node with a variable
		// _gem.OnScored += OnScored; // here we add the name of the function so we can "subscribe to it"

		_timer = GetNode<Timer>(_timerPath); // declaring the node
		_scoreLabel = GetNode<Label>(_scoreLabelPath); // assign the variable for the label
		_music = GetNode<AudioStreamPlayer>(_musicPath); // assign the variable for the music
		_effectPlaying = GetNode<AudioStreamPlayer2D>(_effectPlayingPath); // assign the variable for the scoring effect.
		_timer.Timeout += SpawnGem; // subscribing our function to the Timeout() function of the timer. so now, every x seconds the function will be called, as we are linked to our timer.

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{



	}

	private void SpawnGem()
	{
		Random rnd = new Random(); // rand class from c#

		// we create a new gem
		Gem gem = (Gem)_gemScene.Instantiate();
		AddChild(gem);
		int y_position = -60;
		int x_position = rnd.Next(_gem_margin, 900 - _gem_margin);// get a random number between 10 and 900 
		gem.Position = new Vector2(x_position, y_position);
		gem.OnScored += OnScored;
		gem.OnGemOffScreen += GameOver; // we have to subscribe every new gem to the offscreen signal. and then connect that to our function to be called.

	}

	private void OnScored() // in this way we can connect to a signal that we made. remember to put parameters if something is being sended from the signal
	{

		GD.Print("OnScored Game.cs!");
		_score++; // increment one to our score
		_scoreLabel.Text = _score.ToString(); // update our label with the .text 
		_effectPlaying.Play();

	}

	private void GameOver()
	{
		GD.Print("Game over :(");
		foreach (Node node in GetChildren()) // itarate through the list of the nodes that we have inside our game node. we get that list with GetChildren()
		{

			node.SetProcess(false); // we get the nodes and stop them

		}
		_timer.Stop(); // and, separately from the nodes, the timer has to be stopped this way, using stop(); function
		_music.Stop(); // stop the music when the game is over.
		_effectPlaying.Stop(); // stop another effect playing at the moment if there is one.
		_effectPlaying.Stream = _gameOverEffect; // we now assign the new effect to the variable that was already declared.
		_effectPlaying.Play(); // and then play that effect.
	}

}
