using Godot;
using System;

public partial class BaseBullet : CharacterBody2D
{
	public int Damage = 10;
	float speed = 100;
	Vector2 direction;
	DateTime birthTime;
	TimeSpan ttl = TimeSpan.FromMilliseconds(600);
	BaseBullet(){

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		birthTime = DateTime.Now;
		//GD.Print("Spawned bullet at:" + GlobalPosition);
		direction = Vector2.One.Rotated(Rotation).Normalized();
		var a2D = GetNode<Area2D>("BulletHitbox");
		a2D.BodyEntered += onBodyEnter;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(DateTime.Now - birthTime > ttl)
			QueueFree();
		//Velocity = direction * speed;
		MoveAndSlide();
	}

	void onBodyEnter(Node collidedBody){
		//GD.Print(this.Name + ": Colission detected");
		//GD.Print(this.Name + ": Collided with: " + collidedBody.Name);
		var creature = collidedBody as Basecreature;
		if(creature != null){
			//GD.Print("Bullet hit a creature");
			creature.TakeDamage(Damage);
		}else{
			//GD.Print("Bullet hit something else");
		}
		QueueFree();
	}
}
