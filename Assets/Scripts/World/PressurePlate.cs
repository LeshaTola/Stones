using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
	public UnityEvent OnPlateStepOn;
	public UnityEvent OnPlateStepDown;
	
	private Tile tile;

	private void Awake() {
		tile = GetComponent<Tile>();
	}

	private void Start() {
		tile.OnTileOccupied += Tile_OnTileOccupied;
		tile.OnTileDeoccupied += Tile_OnTileDeoccupied;

	}

	private void Tile_OnTileDeoccupied() {
		OnPlateStepDown?.Invoke();
	}

	private void Tile_OnTileOccupied() {
		OnPlateStepOn?.Invoke();
	}
}
