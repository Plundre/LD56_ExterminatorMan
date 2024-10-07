using Godot;
using System;

public partial class Level : Node2D
{
	public int pointsEarned = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var player = GetNode<Player>("/root/Level/Player");
		player.PlayerDied += GameOver;
		GetNode<AudioStreamPlayer>("Music").Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	internal void GivePoints(int points){
		pointsEarned += points;
		GD.Print("Earned points: " + points + ", " + pointsEarned);
	}

	internal void GameOver(){
		GetNode<Global>("/root/Global").score = pointsEarned;
		GetNode("Player").QueueFree();
		GetTree().ChangeSceneToFile("res://game_over.tscn");

		//Load end-screen w score
		//Restart game button


	}

	internal void StartGame(){
		// Spawn player
		// Start spawning shit
	}
}
