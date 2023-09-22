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
			CreatePlayer();
			AddCoin();

			StartGame();
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

			player.Hide();
			AddChild(player);
		}

		private void StartGame() {
			playing = true;
			level = 1;
			score = 0;
			timeLeft = 0;
			player.Start(playerSpawn.Position);
			player.Show();
		}
	#endregion

	#region Events
		private void Player_RecolectCoin() {
			if(playing && coinContainer.GetChildCount() <= 1) {
				level++;
				timeLeft += 7;
				AddCoin();
			}
		}
	#endregion
}
