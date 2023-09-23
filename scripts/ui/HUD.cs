using Godot;
using System;

public partial class HUD : CanvasLayer {
    #region Variables
        [Export] Label scoreLabel;
        [Export] Label timeLabel;
        [Export] Label titleLabel;
        [Export] Button playButton;
    #endregion

    #region Signal
        [Signal] public delegate void StartGameEventHandler();
    #endregion

    #region Godot Methods
        public override void _Ready() {
            playButton.Pressed += PlayButton_Pressed;
        }
    #endregion

    #region My Methods
        public void UpdateScore(int value) => scoreLabel.Text = "SCORE: " + value.ToString();

        public void UpdateTimer(int value) => timeLabel.Text = value.ToString();

        public void ShowTitleGame() {
            titleLabel.Show();
        }
    #endregion

    #region Events
        private void PlayButton_Pressed() {
            titleLabel.Hide();
            EmitSignal(SignalName.StartGame);
        }
    #endregion
}
