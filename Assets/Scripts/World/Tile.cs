using UnityEngine;

public class Tile : MonoBehaviour {
	[SerializeField] private TileType tileType;
	[SerializeField] private bool walkable;

	public bool Walkable { get=> walkable; }
	public bool Occupied { get; set; }
	public Vector2Int Position { get; set; }

	public bool IsAvailableToMove() => walkable && !Occupied;
}
