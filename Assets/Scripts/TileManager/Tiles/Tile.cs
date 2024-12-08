using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tile: ITile {
	public string name {get;}
	public Sprite sprite {get;}

	public Tile(string name, Sprite sprite){
		this.name = name;
		this.sprite = sprite;
	}
}