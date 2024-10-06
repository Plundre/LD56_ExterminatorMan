using Godot;
using System;


public partial class Player : CharacterBody2D
{

	public const float speedMultiplierFB = 100;
	public const float speedMultiplierLR = 50;
	public const float rotOffset = 90;
	public Vector2 movementDirection;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var a2D = GetNode<Area2D>("Area2D");
		a2D.BodyEntered += onBodyEnter;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		RotateTowardMouse();
		
		movementDirection = Input.GetVector("left","right","forward","backward").Rotated(Rotation).Normalized();

		Velocity = movementDirection * speedMultiplierFB;

/*
		if(Input.IsKeyPressed(Key.W)){
			Velocity = Vector2.Up.Rotated(Rotation).Normalized() * speedMultiplierFB;
		}
		if(Input.IsKeyPressed(Key.A)){
			Velocity = Vector2.Left.Rotated(Rotation).Normalized() * speedMultiplierLR;
		}
		if(Input.IsKeyPressed(Key.S)){
			Velocity = Vector2.Down.Rotated(Rotation).Normalized() * speedMultiplierFB;
		}
		if(Input.IsKeyPressed(Key.D)){
			Velocity = Vector2.Right.Rotated(Rotation).Normalized() * speedMultiplierLR;
		}
*/
		MoveAndSlide();
	}

	internal void RotateTowardMouse(){
		float radToMouse = GetAngleTo(GetGlobalMousePosition()) + Mathf.DegToRad(rotOffset);
		Rotate(radToMouse);
	}

	void onBodyEnter(Node collidedBody){
		GD.Print(this.Name + ": Colission detected");
		GD.Print(this.Name + ": Collided with: " + collidedBody.Name);
	}
	
}
