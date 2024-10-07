using Godot;
using System;
using System.Net;

public partial class GameOver : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Label>("Score").Text = "Final Score: " + GetNode<Global>("/root/Global").score;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsKeyPressed(Key.Enter)){
			GetTree().ChangeSceneToFile("res://level.tscn");
		}else if(Input.IsKeyPressed(Key.Escape)){
			GetTree().Quit();
		}
	}
}
