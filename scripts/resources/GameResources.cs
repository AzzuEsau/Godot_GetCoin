using Godot;
using System;

public static class GameResources {
    public static PackedScene coinsPrefab = (PackedScene)ResourceLoader.Load("res://prefabs/environment/coin.tscn");
    public static PackedScene powerUpPrefab = (PackedScene)ResourceLoader.Load("res://prefabs/environment/powerUp.tscn");
    public static PackedScene cactusPrefab = (PackedScene)ResourceLoader.Load("res://prefabs/environment/cactus.tscn");
    public static PackedScene playerPrefab = (PackedScene)ResourceLoader.Load("res://prefabs/player/player.tscn");


    public static AudioStream coinAudio = (AudioStream)GD.Load("res://media/audio/Coin.wav");
    public static AudioStream powerUpAudio = (AudioStream)GD.Load("res://media/audio/PowerUp.wav");
}
