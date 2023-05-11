using System;
using UnityEngine;

public class Tile : MonoBehaviour {

	public event Action OnTileOccupied;
	public event Action OnTileDeoccupied;

	[SerializeField] private TileType tileType;
	[SerializeField] private bool walkable;

	private bool occupied;

	public bool Walkable { get => walkable; set => walkable = value; }// ToFix
	public bool Occupied { 
		get => occupied; 
		set {
			occupied = value;
			if(occupied == true) {
				OnTileOccupied?.Invoke();
			}
			else
			{
				OnTileDeoccupied?.Invoke();
			}
		}
	}// ToFix
	public Vector2Int Position { get; set; }// ToFix

	public bool IsAvailableToMove() => walkable && !Occupied;
}
