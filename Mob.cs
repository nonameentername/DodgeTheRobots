using Godot;
using System;

public class Mob : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public int MinSpeed = 150;

    [Export]
    public int MaxSpeed = 250;

    private String[] _mobTypes = {"walk", "swim", "fly"};

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var animatedSprite = (AnimatedSprite) GetNode("AnimatedSprite");

        var randomMob = new Random();
        animatedSprite.Animation = _mobTypes[randomMob.Next(0, _mobTypes.Length)];

        animatedSprite.Play();
    }

    public void onVisibilityScreenExited() {
        QueueFree();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
