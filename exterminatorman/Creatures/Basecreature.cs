using Godot;
using System;
using System.Security.Cryptography;

public partial class Basecreature : RigidBody2D
{
	//[Signal]
	//public delegate void CreatureDiedEventHandler(int points);
	Vector2 creatureDirection = new Vector2();
	Random rnd = new Random();
	float creatureSpeed = 70;

	public int Health = 100;

	public int pointValue = 1;

	public int Damage = 10;

	public int rndMoveTowardsPlayerWeight = 7;

	Vector2 movementDirection;

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
			
			var level = GetNode("/root/Level") as Level;
			if(level != null){
				level.GivePoints(pointValue);
			} 
			//EmitSignal(SignalName.CreatureDied, pointValue);
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
			if(rnd.Next(0, 10) < rndMoveTowardsPlayerWeight){
				var player = GetNode<Player>("/root/Level/Player");
				movementDirection = (player.GlobalPosition - GlobalPosition).Normalized();
			}else{
				creatureDirection.X = rnd.Next(-5,5);
				creatureDirection.Y = rnd.Next(-5,5);
				lastDirectionChange = DateTime.Now;
				movementDirection = creatureDirection.Normalized();
			}
			LinearVelocity = movementDirection * creatureSpeed;
		}

	}

	public void TakeDamage(int damage){
		//GD.Print("Damage received (" + damage + ")");
		Health -= damage;
	}

	void onBodyEnter(Node collidedBody){
		//GD.Print(this.Name + ": Colission detected");
		//GD.Print(this.Name + ": Collided with: " + collidedBody.Name);
		//GD.Print("Health: " + Health);
		var player = collidedBody as Player;
		if(player != null){
			//GD.Print("Bullet hit a creature");
			player.TakeDamage(Damage);
		}else{
			//GD.Print("Bullet hit something else");
		}
	}

}
