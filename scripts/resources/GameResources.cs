using Godot;
using System;

public static class GameResources {
    public static PackedScene coinsPrefab = (PackedScene)ResourceLoader.Load("res://prefabs/environment/coin.tscn");
    public static PackedScene playerPrefab = (PackedScene)ResourceLoader.Load("res://prefabs/player/player.tscn");

}
