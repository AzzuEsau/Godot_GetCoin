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
		private Vector2 windowSize = new Vector2(480, 720);
	#endregion

	#region Signals
	#endregion

	#region Godot Methods
		// Called when the node enters the scene tree for the first time.
		public override void _Ready() {
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


			if (Input.IsActionJustPressed("ui_accept"))
				Death();
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
	#endregion
}
