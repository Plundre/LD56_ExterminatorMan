using Godot;
using System;

public partial class CreatureSpawner : Node2D
{
	PackedScene creatureScene = GD.Load<PackedScene>("res://Creatures/basecreature.tscn");
	DateTime latestSpawn = DateTime.MinValue;
	TimeSpan cooldown = TimeSpan.FromMilliseconds(400);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(DateTime.Now - latestSpawn >= cooldown){
			SpawnCreature(new Vector2 (0,0));
			latestSpawn = DateTime.Now;
		}
	}

	void SpawnCreature(Vector2 pos){
		GD.Print("Spawning Bullet");
		var creature = creatureScene.Instantiate<RigidBody2D>();
		AddChild(creature);
		creature.Position = pos;
	}
}
