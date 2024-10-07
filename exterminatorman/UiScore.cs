using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class UiScore : Container
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	 	var ScoreLabel = GetNode<Label>("Score");
		var Level = GetNode<Level>("../../");
		ScoreLabel.Text = "Score: " + Level.pointsEarned;
	}
}
