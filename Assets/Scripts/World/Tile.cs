using System;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
	public event Action OnTileOccupied;
	public event Action OnTileDeoccupied;

	[SerializeField] private TileType tileType;
	[SerializeField] private bool walkable;
	[SerializeField] private AbstractInteractable interactablePart;

	private bool occupied;
	private Creature occupant;

	public bool Walkable { get => walkable; set => walkable = value; }//ToFix
	public bool Occupied
	{
		get => occupied;
		private set
		{
			occupied = value;
			if (occupied == true)
			{
				OnTileOccupied?.Invoke();
			}
			else
			{
				OnTileDeoccupied?.Invoke();
			}
		}
	}

	public Vector2Int Position { get; private set; }

	public bool TryInteract(Player player)
	{
		if (interactablePart == null)
		{
			return false;
		}
		if (interactablePart.CanInteract(player))
		{
			interactablePart.Interact(player);
			return true;
		}
		return false;
	}

	public bool CanInteract(Player player)
	{
		return interactablePart != null && interactablePart.CanInteract(player);
	}

	public void SetOccupied(Creature creature)
	{
		if (creature != null)
		{
			occupant = creature;
			Occupied = true;
		}
	}

	public void DeOccupied()
	{
		occupant = null;
		Occupied = false;
	}

	private void Awake()
	{
		Position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
	}

	public bool IsAvailableToMove()
	{
		return walkable && !Occupied;
	}
}
