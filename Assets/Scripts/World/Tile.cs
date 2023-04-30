using UnityEngine;

public class Tile : MonoBehaviour {
	[SerializeField] private TileType tileType;
	[SerializeField] private bool walkable;

	public bool Walkable { get=> walkable; }
	public bool Occupied { get; set; }
	public Vector2Int Position { get; set; }
/*	public Tile LeftTile { get; private set; }
	public Tile RightTile { get; private set; }
	public Tile UpTile { get; private set; }
	public Tile DownTile { get; private set; }*/

/*	public void SetNeighbors(Tile upTile, Tile leftTile) {
		UpTile = upTile;
		if (UpTile != null) {
			UpTile.DownTile = this;
		}

		LeftTile = leftTile;
		if (LeftTile != null) {
			LeftTile.RightTile = this;
		}
	}*/

	public bool isWalkable() => walkable && !Occupied;
}
