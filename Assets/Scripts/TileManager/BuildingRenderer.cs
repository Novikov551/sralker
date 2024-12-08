using System.Collections;
using UnityEngine;
using OsmChunkLoader;
using OsmSharp;
using System.Threading.Tasks;

public class BuildingGenerator: AbstractRenderer
{
	public BuildingGenerator(SpriteRenderer spriteRenderer,TileMap tileMap):base(spriteRenderer){
		this.tileMap = tileMap;
		var testTile = tileMap.getTileByName("grassed_ground_1");
		this.gridMultiplier = testTile.sprite.bounds.size.x;
		chunkLoader = new ChunkLoader(48.615718f, 44.431930f);
	}
	
	public SpriteRenderer spriteRenderer;
	public ChunkLoader chunkLoader;

	private ArrayList renderedSprites = new ArrayList();

	private TileMap tileMap;

	int size = 100;
	float gridMultiplier = 1f;

	// Start is called before the first frame update
	async public Task Render()
	{
		var chunk = await this.chunkLoader.GetChunk(0f,0f);

		foreach(var building in chunk){
			var buildingPivot = new GameObject();
			
			foreach(var tile in building.Tiles){
				string tileName = "";
				if(tile.Type == Models.TileType.AppartmentCorner)
					tileName = "apartment_corner_1";
				if(tile.Type == Models.TileType.AppartmentCenter)
					tileName = "apartment_center_1";
				if(tile.Type == Models.TileType.AppartmentLedge)
					tileName = "apartment_ledge_1";
				if(tile.Type == Models.TileType.AppartmentSide)
					tileName = "apartment_border_1";
				if(tile.Type == Models.TileType.AppartmentSingle)
					tileName = "apartment_ledge_1";

				var sprite = this.RenderTile(tileMap.getTileByName(tileName),new Vector2(tile.Coord.GlobalX*gridMultiplier,tile.Coord.GlobalY*gridMultiplier),buildingPivot);

				sprite.transform.Rotate(0,0,tile.Angle);

			}

		}


		

		
	}

}
