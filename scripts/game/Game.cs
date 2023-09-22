using Godot;
using System;

public partial class Game : Node {
	#region Variables
		[Export] private int gameTime;
		// [Export] PackedScene coinPrefab;
		// [Export] PackedScene playerPrefab;
		[Export] Node coinContainer;
		[Export] Marker2D playerSpawn;
		[Export] Timer gameTimer;
		[Export] HUD hud;
		// PackedScene coins = (PackedScene)ResourceLoader.Load("res://prefabs/environment/Obstacle.tscn");

		public static Vector2 screenSize = new Vector2(480, 720);

		private Player player;

		private int level = 1;
		private int score;
		private int timeLeft;
		private bool playing;
	#endregion

	#region Signals

	#endregion

	#region Godot Methods
		// Called when the node enters the scene tree for the first time.
		public override void _Ready() {
			gameTimer.Timeout += GameTimer_Timeout;
			hud.StartGame += HUD_StartGame;

			CreatePlayer();
			// StartGame();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta) {
		}
	#endregion

	#region My Methods
		private void AddCoin() {
			for (int i = 0; i < 4 + level; i++) {
				Node2D newCoin = (Node2D)GameResources.coinsPrefab.Instantiate();;
				float xRange = (float)GD.RandRange(5, screenSize.X);
				float yRange = (float)GD.RandRange(5, screenSize.Y);
				coinContainer.AddChild(newCoin);
				newCoin.Position = new Vector2(xRange, yRange);
			}
		}

		private void CreatePlayer() {
			player = (Player)GameResources.playerPrefab.Instantiate();

			player.RecolectCoin += Player_RecolectCoin;
			player.Hurt += Player_Hurt;

			player.Hide();
			AddChild(player);
		}

		private void StartGame() {
			playing = true;
			level = 1;
			score = 0;
			timeLeft = 10;

			hud.UpdateScore(score);
			hud.UpdateTimer(timeLeft);
			
			AddCoin();

			gameTimer.Start();
			player.Start(playerSpawn.Position);
			player.Show();
		}

		private void GameOver() {
			playing = false;
			gameTimer.Stop();

			foreach(Coin coin in coinContainer.GetChildren()) {
				coin.QueueFree();
			}

			player.Death();
			hud.ShowTitleGame();
		}
	#endregion

	#region Events
		private void Player_RecolectCoin() {
			score++;
			hud.UpdateScore(score);

			if(playing && coinContainer.GetChildCount() <= 1) {
				level++;
				timeLeft += 7;
				AddCoin();
				hud.UpdateTimer(timeLeft);
			}
		}

		private void Player_Hurt() {
			GameOver();
		}

		private void GameTimer_Timeout() {
			timeLeft -= 1;
			hud.UpdateTimer(timeLeft);

			if(timeLeft <= 0)
				GameOver();
		}

		private void HUD_StartGame() {
			StartGame();
		}
	#endregion
}
