using Godot;
using System;


public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void PlayerDiedEventHandler();

	public const float speedWalk = 100;
	public const float speedSprint = 200;
	public float speed;
	public const float rotOffset = 90;
	public Vector2 movementDirection;
	
	internal int Health = 1000;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var a2D = GetNode<Area2D>("Area2D");
		a2D.BodyEntered += onBodyEnter;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Health <= 0){
			EmitSignal(SignalName.PlayerDied);
		}
		if(!Input.IsKeyPressed(Key.Shift)){
			speed = speedWalk;
		}else{
			speed = speedSprint;
		}
		RotateTowardMouse();
		
		movementDirection = Input.GetVector("left","right","forward","backward").Rotated(Rotation).Normalized();

		Velocity = movementDirection * speed;

		MoveAndSlide();
	}

	internal void RotateTowardMouse(){
		float radToMouse = GetAngleTo(GetGlobalMousePosition()) + Mathf.DegToRad(rotOffset);
		Rotate(radToMouse);
	}
		public void TakeDamage(int damage){
		GD.Print("Damage received (" + damage + " | " + Health + ")");
		Health -= damage;
	}

	void onBodyEnter(Node collidedBody){
		GD.Print(this.Name + ": Colission detected");
		GD.Print(this.Name + ": Collided with: " + collidedBody.Name);
	}
	
}
