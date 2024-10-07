using System;
using Godot;

public partial class Basetool : Node2D
{
	[Signal]
	public delegate void ToolFiredEventHandler(Vector2 pos, Vector2 direction);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsMouseButtonPressed(MouseButton.Left)){
			EmitSignal(SignalName.ToolFired, GlobalPosition, -this.GlobalTransform.Y);	
		}
	}
}
