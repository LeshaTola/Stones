using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


public class WorldController : MonoBehaviour
{

	[SerializeField] private Tilemap map;
	private List<Tile> tiles;

	private void Awake()
	{
		tiles = GetComponentsInChildren<Tile>().ToList();
		Debug.Log(tiles.Count);
	}

	public bool IsPositionAvailable(Vector2Int nextPosition)
	{
		Tile tileToCheck = GetTileFromPosition(nextPosition);
		return IsPositionSuitableToMove(tileToCheck);
	}

	public void ChangeOccupiedState(Vector2Int currentPosition, Vector2Int targetPosition, Creature creature)
	{
		Tile currentTile = GetTileFromPosition(currentPosition);
		Tile targetTile = GetTileFromPosition(targetPosition);
		currentTile.DeOccupied();
		targetTile.SetOccupied(creature);
	}

	public Tile GetTileFromPosition(Vector2Int postiton)
	{
		return tiles.FirstOrDefault((x) => x.Position == postiton);
	}

	private bool IsPositionSuitableToMove(Tile tileToCheck)
	{
		return tileToCheck != null && tileToCheck.IsAvailableToMove();
	}
}