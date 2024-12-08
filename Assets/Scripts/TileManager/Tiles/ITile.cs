using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public interface ITile {
	public string name {get;}
	public Sprite sprite {get;}
}