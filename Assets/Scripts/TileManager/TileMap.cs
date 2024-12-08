using UnityEngine;
using Unity.VisualScripting;
using System;
using System.Collections.Generic;
using System.Linq;




#if UNITY_EDITOR
using UnityEditor;
#endif



public class TileMap {
	private SpriteRenderer spriteRenderer;
	public string spriteFolder = "Sprites/Images/Tiles";
	private Sprite[] sprites;
	private Dictionary<String,ITile> spritesMap = new Dictionary<String,ITile>();


	public ITile getTileByName(string name){
		return this.spritesMap[name];
	}
	
	public TileMap(SpriteRenderer spriteRenderer){
		this.spriteRenderer = spriteRenderer;
	}

	public void WarmSprites(){
		this.LoadSprites();
		this.MakeTileMap();

	}

	void MakeTileMap(){
		foreach(var sprite in sprites){
			this.spritesMap.Add(sprite.name,new Tile(sprite.name,sprite));
		}
	}

	void LoadSprites() {
		string fullPath = $"{Application.dataPath}/{spriteFolder}";
		
		Debug.Log(fullPath);
		Debug.Log(System.IO.Directory.Exists(fullPath));

		if (!System.IO.Directory.Exists(fullPath)){
			return;
		}


		var folders = new string[]{$"Assets/{spriteFolder}"};
		var guids = AssetDatabase.FindAssets("t:Sprite", folders);

		var newSprites = new Sprite[guids.Length];




		bool mismatch;
		if (sprites == null) {
			mismatch = true;
			sprites = newSprites;
		} else {
			mismatch = newSprites.Length != sprites.Length;
		}


		for (int i = 0; i < newSprites.Length; i++) {
			var path = AssetDatabase.GUIDToAssetPath(guids[i]);
			newSprites[i] = AssetDatabase.LoadAssetAtPath<Sprite>(path);
			mismatch |= (i < sprites.Length && sprites[i] != newSprites[i]);
		}

		
		Debug.Log($"Sprites loaded");

	}

	// void Start(){

	// 	var gameObject = Instantiate(tilePrefab,this.transform.position,Quaternion.identity);
	// 	gameObject.sprite = this.sprites[0];
	// }
}