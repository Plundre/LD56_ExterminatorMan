using Godot;
using System;
using System.Security.Cryptography;

public partial class Basecreature : RigidBody2D
{
	Vector2 creatureDirection = new Vector2();
	Random rnd = new Random();
	float creatureSpeed = 70;

	public int Health = 100;

	DateTime lastDirectionChange = DateTime.MinValue;
	TimeSpan cooldown = TimeSpan.FromSeconds(0.2);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ContactMonitor = true;
		MaxContactsReported = 5;
		var rb = GetNode<Area2D>("Area2D");
		rb.BodyEntered += onBodyEnter;



	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Health <= 0){
			QueueFree();
			return;
		}
		if(Input.IsKeyPressed(Key.L)){
			LinearVelocity = Vector2.Down*100;
		}else if(Input.IsKeyPressed(Key.O)){
			LinearVelocity = Vector2.Up*100;
		}else if(Input.IsKeyPressed(Key.K)){
			LinearVelocity = Vector2.Zero;
		}

		if(DateTime.Now-lastDirectionChange >= cooldown){
			creatureDirection.X = rnd.Next(-5,5);
			creatureDirection.Y = rnd.Next(-5,5);
			LinearVelocity = creatureDirection.Normalized() * creatureSpeed;
			lastDirectionChange = DateTime.Now;
		}

	}

	public void TakeDamage(int damage){
		GD.Print("Damage received (" + damage + ")");
		Health -= damage;
	}

	void onBodyEnter(Node collidedBody){
		GD.Print(this.Name + ": Colission detected");
		GD.Print(this.Name + ": Collided with: " + collidedBody.Name);
		GD.Print("Health: " + Health);
	}

}
