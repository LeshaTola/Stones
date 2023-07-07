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
	}

	public bool IsPositionAvailable(Vector3 nextPosition)
	{
		Tile tileToCheck = GetTileFromPosition(nextPosition);
		return IsPositionSuitableToMove(tileToCheck);
	}

	public void ChangeOccupiedState(Vector3 currentPosition, Vector3 targetPosition, Creature creature)
	{
		Tile currentTile = GetTileFromPosition(currentPosition);
		Tile targetTile = GetTileFromPosition(targetPosition);

		currentTile.DeOccupied();
		targetTile.SetOccupied(creature);
	}

	public bool TryGetCreatureInPosition(Vector3 position, out Creature creature)
	{
		creature = null;
		Tile tileToCheck = GetTileFromPosition(position);
		if (tileToCheck.Occupant != null)
		{
			creature = tileToCheck.Occupant;
			return true;
		}
		return false;
	}

	public Tile GetTileFromPosition(Vector3 postiton)
	{
		Vector2Int vector2IntPosition = Tools.GetVector2IntPosition(postiton);
		return tiles.FirstOrDefault((x) => x.Position == vector2IntPosition);
	}

	private bool IsPositionSuitableToMove(Tile tileToCheck)
	{
		return tileToCheck != null && tileToCheck.IsAvailableToMove();
	}
}