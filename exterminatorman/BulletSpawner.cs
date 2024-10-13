using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class BulletSpawner : Node2D
{

	DateTime lastFired = DateTime.MinValue;
	TimeSpan cooldown = TimeSpan.FromSeconds(0.0075);
	int bulletSpreadRange = 10;
	Random rnd = new Random();
	PackedScene bulletScene = GD.Load<PackedScene>("res://Tools/Bullets/BaseBullet.tscn");
	


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

	public void SpawnBullets(Vector2 pos, Vector2 direction){
		if(DateTime.Now-lastFired >= cooldown){
			//GD.Print("Spawning Bullet");
			var bullet = bulletScene.Instantiate<CharacterBody2D>();
			AddChild(bullet);
			bullet.GlobalPosition = pos;
			bullet.Velocity = (direction + new Vector2((float)rnd.Next(-bulletSpreadRange,bulletSpreadRange)/100,(float)rnd.Next(-bulletSpreadRange,bulletSpreadRange)/100)).Normalized() * 500;
			if(!GetNode<AudioStreamPlayer>("SFX").Playing){
				GetNode<AudioStreamPlayer>("SFX").Play(0);		
			}
			
			//GD.Print("Fuck Shit: " + bullet.GlobalPosition);
			lastFired = DateTime.Now;
		}
	}
}
