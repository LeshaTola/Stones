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

	public bool Walkable { get => walkable; set => walkable = value; }//ToFix

	public Creature Occupant { get; private set; }
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
			Occupant = creature;
			OnTileOccupied?.Invoke();
		}
	}

	public void DeOccupied()
	{
		Occupant = null;
		OnTileDeoccupied?.Invoke();
	}

	private void Awake()
	{
		Position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
	}

	public bool IsAvailableToMove()
	{
		return walkable && Occupant == null;
	}

	public void DamageOcupant(Creature damager, float damage)
	{
		if (Occupant != null)
		{
			Occupant.ApplyDamage(damager, damage);
		}
	}
}
