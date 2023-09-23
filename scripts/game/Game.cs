using Godot;
using System;

public partial class Game : Node {
	#region Variables
		[Export] private int gameTime;
		[Export] Node coinContainer;
		[Export] Marker2D playerSpawn;
		[Export] Timer gameTimer;
		[Export] Timer powerUpTimer;
		[Export] HUD hud;



		[Export] AudioStreamPlayer startAudio;
		[Export] AudioStreamPlayer deathAudio;
		[Export] AudioStreamPlayer coinAudio;
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

			powerUpTimer.Timeout += PowerUpTimer_Timeout;

			CreatePlayer();
			// StartGame();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta) {
			CheckCoins();
		}
	#endregion

	#region My Methods
		private void AddCoin() {
			for (int i = 0; i < 4 + level; i++) {
				Coin newCoin = (Coin)GameResources.coinsPrefab.Instantiate();;
				coinContainer.AddChild(newCoin);
				float xRange = (float)GD.RandRange(5, screenSize.X);
				float yRange = (float)GD.RandRange(5, screenSize.Y);
				newCoin.Position = new Vector2(xRange, yRange);
			}
		}

		private void CreatePlayer() {
			player = (Player)GameResources.playerPrefab.Instantiate();

			player.RecolectCoin += Player_RecolectCoin;
			player.Hurt += Player_Hurt;
			player.RecolectPowerUp += Player_RecolectPowerUp;

			player.Hide();
			AddChild(player);
		}

		private void StartGame() {
			startAudio.Play();

			playing = true;
			level = 1;
			score = 0;
			timeLeft = 10;

			hud.UpdateScore(score);
			hud.UpdateTimer(timeLeft);
			
			AddCoin();

			powerUpTimer.Start();
			gameTimer.Start();
			player.Start(playerSpawn.Position);
			player.Show();
		}

		private void GameOver() {
			deathAudio.Play();
			playing = false;
			gameTimer.Stop();

			foreach(Coin coin in coinContainer.GetChildren()) {
				coin.QueueFree();
			}

			player.Death();
			hud.ShowTitleGame();
		}

		private void CheckCoins() {
			if(playing && coinContainer.GetChildCount() <= 0) {
				level++;
				timeLeft += 7;
				AddCoin();
				hud.UpdateTimer(timeLeft);
			}
		}
	#endregion

	#region Events
		private void Player_RecolectCoin() {
			coinAudio.Stream = GameResources.coinAudio;
			coinAudio.Play();
			score++;
			hud.UpdateScore(score);
		}

		private void Player_Hurt() {
			GameOver();
		}

		private void Player_RecolectPowerUp() {
			coinAudio.Stream = GameResources.powerUpAudio;
			coinAudio.Play();
			timeLeft += 3;
			hud.UpdateTimer(timeLeft);
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

		private void PowerUpTimer_Timeout() {
			powerUpTimer.WaitTime = GD.RandRange(5f, 30f);

			PowerUp newPowerUp = (PowerUp)GameResources.powerUpPrefab.Instantiate();
			AddChild(newPowerUp);
			float xRange = (float)GD.RandRange(5, screenSize.X);
			float yRange = (float)GD.RandRange(5, screenSize.Y);
			newPowerUp.Position = new Vector2(xRange, yRange);
		}
	#endregion
}
