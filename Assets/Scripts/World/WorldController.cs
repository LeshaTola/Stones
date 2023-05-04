using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour {

	[SerializeField] private Tilemap map;
	private List<Tile> tiles;

	private void Awake() {
		tiles = new List<Tile>();
		foreach (Transform child in map.transform) {
			var tile = child.GetComponent<Tile>();
			tile.Position = new Vector2Int(Mathf.RoundToInt(child.position.x), Mathf.RoundToInt(child.position.z));
			tiles.Add(tile);
		}
	}
	private bool IsPositionSuitableToMove(Tile tileToCheck) {
		if (tileToCheck == null) {
			return false;
		}
		return tileToCheck.IsAvailableToMove();
	}

	public bool IsPositionAvailable(Vector2Int nextPosition) {
		var tileToCheck = GetTileFromPosition(nextPosition);
		return IsPositionSuitableToMove(tileToCheck);
	}

	public void ChangeOccupiedState(Vector2Int currentPosition, Vector2Int targetPosition) {
		var currentTile = GetTileFromPosition(currentPosition);
		var targetTile = GetTileFromPosition(targetPosition);
		if(IsPositionSuitableToMove(targetTile) == false) {
			return;
		}
		currentTile.Occupied = false;
		targetTile.Occupied = true;
	}

	private Tile GetTileFromPosition(Vector2Int postiton) {
		return tiles.FirstOrDefault((x) => x.Position == postiton);
	}
}