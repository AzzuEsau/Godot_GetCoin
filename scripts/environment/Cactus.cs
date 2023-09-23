using Godot;
using System;

public partial class Cactus : Area2D {
    public override void _Ready() {
        this.AreaEntered += Cactus_AreaEntered;
    }

    private void Cactus_AreaEntered(Area2D area) {
        if(area.IsInGroup("Player"))
            QueueFree();

        if(area.IsInGroup("Hurtable")) {
            float xRange = (float)GD.RandRange(5, Game.screenSize.X);
            float yRange = (float)GD.RandRange(5, Game.screenSize.Y);
            Position = new Vector2(xRange, yRange);
        }
    }

}
