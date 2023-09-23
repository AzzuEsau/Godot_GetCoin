using Godot;
using System;

public partial class Player : Area2D {
	#region Variables
		#region Exports
			// Export allow us to modify values in Godot
			[Export] private int velocity;
			// Set the value of the animated sprite 2d inside godot inspector
			[Export] private AnimatedSprite2D animatedSprite2D;
		#endregion

		private Vector2 movement = new Vector2();
		private Vector2 windowSize = Game.screenSize;
	#endregion

	#region Signals
		[Signal] public delegate void RecolectCoinEventHandler();
		[Signal] public delegate void HurtEventHandler();
	#endregion

	#region Godot Methods
		// Called when the node enters the scene tree for the first time.
		public override void _Ready() {
			this.AreaEntered += Player_AreaEntered;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta) {
			GetInput();
			Animate();
			Move(delta);
		}
	#endregion

	#region My Methods
		public void Start(Vector2 initialPos) {
			Position = initialPos;
			// Enable the function _Process
			SetProcess(true);
		}

		private void GetInput() {
			movement = new Vector2(0, 0);
			float hMov = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
			float vMov = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
			movement = new Vector2(hMov, vMov).Normalized();
		}

		private void Move(double delta) {
			Position += movement * velocity * (float)delta;
			// Limit the player movement inside the vectors values
			Position = Position.Clamp(new Vector2(0, 20), windowSize);
		}

		private void Animate() {
			if(movement.Length() > 0) {
				animatedSprite2D.Play("run");
				if(movement.X != 0)
					animatedSprite2D.FlipH = movement.X < 0;
			}
			else
				animatedSprite2D.Play("idle");
		}

		public void Death() {
			animatedSprite2D.Play("hurt");
			// Disable the function _Process
			SetProcess(false);
		}
	#endregion

	#region Events
		private void Player_AreaEntered(Area2D area) {
			if(area.IsInGroup("Coin")) {
				((Coin)area).Collect();
				EmitSignal("RecolectCoin");
			}
			else if (area.IsInGroup("Hurtable")) {
				EmitSignal("Hurt");
				Death();
			}
		}
	#endregion
}
