using Godot;
using System;

public partial class BaseBullet : CharacterBody2D
{
	float speed = 100;
	Vector2 direction;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Spawned bullet at:" + GlobalPosition);
		direction = Vector2.One.Rotated(Rotation).Normalized();
		var a2D = GetNode<Area2D>("Area2D");
		a2D.BodyEntered += onBodyEnter;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Velocity = direction * speed;
		MoveAndSlide();
	}

	void onBodyEnter(Node collidedBody){
		GD.Print(this.Name + ": Colission detected");
		GD.Print(this.Name + ": Collided with: " + collidedBody.Name);
		
		
		QueueFree();
	}
	
	public void SetPos(Vector2 pos){Position = pos;}
}
