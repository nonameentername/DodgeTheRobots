using Godot;
using System;

public class HUD : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    public delegate void StartGame();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void ShowMessage(string text)
    {
        var messageTimer = (Timer) GetNode("MessageTimer");
        var messageLabel = (Label) GetNode("MessageLabel");

        messageLabel.Text = text;
        messageLabel.Show();
        messageTimer.Start();
    }

    async public void ShowGameOver()
    {
        var startButton = (Button) GetNode("StartButton");
        var messageTimer = (Timer) GetNode("MessageTimer");
        var messageLabel = (Label) GetNode("MessageLabel");

        ShowMessage("Game Over");
        await ToSignal(messageTimer, "timeout");
        messageLabel.Text = "Dodge the Robots";
        messageLabel.Show();
        startButton.Show();
    }

    public void UpdateScore(int score)
    {
        var scoreLabel = (Label) GetNode("ScoreLabel");
        scoreLabel.Text = score.ToString();
    }

    public void OnStartButtonPressed()
    {
        var startButton = (Button) GetNode("StartButton");
        startButton.Hide();

        EmitSignal("StartGame");
    }

    public void OnMessageTimerTimeout()
    {
        var messageLabel = (Label) GetNode("MessageLabel");
        messageLabel.Hide();
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
