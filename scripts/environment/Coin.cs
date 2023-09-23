using Godot;
using System;
using System.Net;

public partial class Coin : Area2D {
	#region Variables
		[Export] private AnimatedSprite2D animatedSprite2D;
		[Export] private CollisionShape2D collision;
		[Export] private Timer animationTime;
	#endregion

	#region Godot Methods
		public override void _Ready() {
			animatedSprite2D.Pause();

			animationTime.WaitTime = GD.RandRange(.1f, .7f);
			animationTime.Start();
			animationTime.Timeout += AnimationTime_Timeout;
		}
	#endregion

	#region My Methods
		public void Collect() {
			Tween tween = CreateTween();
			tween.TweenProperty(animatedSprite2D, "scale", new Vector2(.1F, .1F), .2F);
			tween.Finished += Delete;
		}

		public void Delete() {
			QueueFree();
		}
	#endregion

	#region Events
		private void AnimationTime_Timeout() {
			animatedSprite2D.Frame = 0;
			animatedSprite2D.Play();
		}
	#endregion
}
