using Godot;
using System;

public partial class PowerUp : Area2D {
    #region Variables
        [Export] private AnimatedSprite2D animatedSprite2D;
		[Export] private CollisionShape2D collision;
		[Export] private Timer animationTimer;
		[Export] private Timer lifeTimer;
    #endregion

	#region Godot Methods
		public override void _Ready() {
			animatedSprite2D.Pause();

			animationTimer.WaitTime = GD.RandRange(.1f, .7f);
			animationTimer.Start();
			animationTimer.Timeout += AnimationTimer_Timeout;

			lifeTimer.Timeout += LifeTimer_Timeout;

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
		private void AnimationTimer_Timeout() {
			animatedSprite2D.Frame = 0;
			animatedSprite2D.Play();
		}

        private void LifeTimer_Timeout() {
            Delete();
        }
	#endregion
}
