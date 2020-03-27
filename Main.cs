using Godot;
using System;

public class Main : Node2D
{
    [Export]
    public PackedScene Mob;

    public int Score = 0;

    private Random rand = new Random();

    public override void _Ready()
    {
        OS.WindowMaximized = true;
    }

    private float RandRand(float min, float max)
    {
        return (float) (rand.NextDouble() * (max - min) + min);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void GameOver()
    {
        var hud = (HUD) GetNode("HUD");
        hud.ShowGameOver();

        AudioStreamPlayer music = (AudioStreamPlayer) GetNode("Music");
        music.Stop();

        AudioStreamPlayer deathSound = (AudioStreamPlayer) GetNode("DeathSound");
        deathSound.Play();

        var mobTimer = (Timer) GetNode("MobTimer");
        var scoreTimer = (Timer) GetNode("ScoreTimer");

        scoreTimer.Stop();
        mobTimer.Stop();
    }

    public void NewGame()
    {
        Score = 0;

        var hud = (HUD) GetNode("HUD");
        hud.UpdateScore(Score);
        hud.ShowMessage("Get Ready!");

        AudioStreamPlayer music = (AudioStreamPlayer) GetNode("Music");
        music.Play();

        var player = (Player) GetNode("Player");
        var startTimer = (Timer) GetNode("StartTimer");
        var startPosition = (Position2D) GetNode("StartPosition");

        player.Start(startPosition.Position);
        startTimer.Start();
    }

    public void OnStartTimerTimeout()
    {
        var mobTimer = (Timer) GetNode("MobTimer");
        var scoreTimer = (Timer) GetNode("ScoreTimer");

        mobTimer.Start();
        scoreTimer.Start();
    }

    public void OnScoreTimerTimeout()
    {
        Score += 1;
        var hud = (HUD) GetNode("HUD");
        hud.UpdateScore(Score);
    }

    public void OnMobTimerTimeout()
    {
        var mobSpawnLocation = (PathFollow2D) GetNode("MobPath/MobSpawnLocation");
        mobSpawnLocation.SetOffset(rand.Next());

        var mobInstance = (RigidBody2D) Mob.Instance();
        AddChild(mobInstance);

        var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        mobInstance.Position = mobSpawnLocation.Position;

        direction += RandRand(-Mathf.Pi / 4, Mathf.Pi / 4);
        mobInstance.Rotation = direction;

        mobInstance.SetLinearVelocity(new Vector2(RandRand(150f, 250f), 0).Rotated(direction));
    }
}
