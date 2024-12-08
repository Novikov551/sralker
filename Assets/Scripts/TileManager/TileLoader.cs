using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLoader : MonoBehaviour
{
	
	public SpriteRenderer spriteRenderer;
	static TileMap tileMap;

	private TerrainRenderer terrainRenderer;
	private BuildingGenerator buildingGenerator;

	int size = 100;
	float gridDx = 1f;
	float gridDY = 1f;

	// Start is called before the first frame update
	async void Start()
	{
		terrainRenderer = new TerrainRenderer(this.spriteRenderer,TileLoader.tileMap);
		terrainRenderer.Render();

		buildingGenerator = new BuildingGenerator(this.spriteRenderer,TileLoader.tileMap);
		await buildingGenerator.Render();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnValidate(){
		TileLoader.tileMap = new TileMap(this.spriteRenderer);
		TileLoader.tileMap.WarmSprites();
	}
}
