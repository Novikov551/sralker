using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingGenerator: AbstractRenderer
{
	public BuildingGenerator(SpriteRenderer spriteRenderer,TileMap tileMap):base(spriteRenderer){
		this.tileMap = tileMap;
		var testTile = tileMap.getTileByName("grassed_ground_1");
		this.gridMultiplier = testTile.sprite.bounds.size.x;
	}
	
	public SpriteRenderer spriteRenderer;

	private ArrayList renderedSprites = new ArrayList();

	private TileMap tileMap;

	int size = 100;
	float gridMultiplier = 1f;

	private float PerlinCoef1 = 0.2f;
	private float PerlinGridMultiplier1 = 0.1f;

	private float PerlinCoef2 = 0.2f;
	private float PerlinGridMultiplier2 = 0.4f;

	private float PerlinCoef3 = 0.3f;
	private float PerlinGridMultiplier3 = 0.2f;

	// Start is called before the first frame update
	public void Render()
	{
		var r1 = UnityEngine.Random.Range(-100f,50f);
		var r2 = UnityEngine.Random.Range(100f,150f);
		var r3 = UnityEngine.Random.Range(-200f,-100f);
		
		for(int x = 0; x<size;x++){
			for(int y = 0; y<size; y++){

				var perlinValue1 = Mathf.PerlinNoise((x*PerlinGridMultiplier1)+r1,y*PerlinGridMultiplier1) * PerlinCoef1;
				var perlinValue2 = Mathf.PerlinNoise((x*PerlinGridMultiplier2+r2),y*PerlinGridMultiplier2) * PerlinCoef2;
				var perlinValue3 = Mathf.PerlinNoise((x*PerlinGridMultiplier3)+r3,y*PerlinGridMultiplier1) * PerlinCoef3;

				if(perlinValue1>0.1) {
					renderedSprites.Add(this.RenderTile(tileMap.getTileByName("bushes_1"),new Vector2(x*gridMultiplier,y*gridMultiplier)));
					continue;
				}

				if(perlinValue2>0.1) {
					renderedSprites.Add(this.RenderTile(tileMap.getTileByName("grassed_ground_1"),new Vector2(x*gridMultiplier,y*gridMultiplier)));
					continue;
				}

				if(perlinValue3>0.1) {
					renderedSprites.Add(this.RenderTile(tileMap.getTileByName("bushes_2"),new Vector2(x*gridMultiplier,y*gridMultiplier)));
					continue;
				}

				renderedSprites.Add(this.RenderTile(tileMap.getTileByName("grassed_ground_2"),new Vector2(x*gridMultiplier,y*gridMultiplier)));

			}
		}
		
	}

}
