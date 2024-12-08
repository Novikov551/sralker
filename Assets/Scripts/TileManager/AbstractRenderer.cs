using UnityEngine;

public abstract class AbstractRenderer{
	private SpriteRenderer spriteRenderer;

	public AbstractRenderer(SpriteRenderer spriteRenderer){
		this.spriteRenderer = spriteRenderer;
	}

	public SpriteRenderer RenderTile(ITile tile, Vector2 position){
		SpriteRenderer tileObject = MonoBehaviour.Instantiate(this.spriteRenderer,position,Quaternion.identity);
		tileObject.sprite = tile.sprite;

		return tileObject;
	}

	public SpriteRenderer RenderTile(ITile tile, Vector2 position, GameObject parent){
		SpriteRenderer tileObject = MonoBehaviour.Instantiate(this.spriteRenderer,position,Quaternion.identity, parent.transform);
		tileObject.sprite = tile.sprite;

		return tileObject;
	}
}