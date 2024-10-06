using Godot;
using System;

public partial class BulletSpawner : Node2D
{
	internal PackedScene bulletBP = GD.Load<PackedScene>("res://Tools/Bullets/BaseBullet.tscn");

	DateTime lastFired = DateTime.MinValue;
	TimeSpan cooldown = TimeSpan.FromSeconds(0.1);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tool = GetNode<Basetool>("/root/Level/Player/Tool");
		tool.ToolFired += SpawnBullets;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SpawnBullets(Vector2 pos){
		if(DateTime.Now-lastFired >= cooldown){
			GD.Print("Spawning Bullet");
			var bullet = bulletBP.Instantiate();
			AddChild(bullet);
			GD.Print("Fuck Shit: " + bullet.Get("Position"));
			bullet.Set("Position",pos);
			lastFired = DateTime.Now;
		}
	}
}
