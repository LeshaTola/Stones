using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDoor : MonoBehaviour, IDoor {


	[SerializeField] private GameObject door;
	private bool isClosed;
	private Tile tile;

	private void Start() {
		tile = GetComponent<Tile>();
		Close();
	}

	public void Close() {
		door.SetActive(true);
		isClosed= true;
		tile.Walkable = false;
	}

	public void Open() {
		door.SetActive(false);
		isClosed = false;
		tile.Walkable = true;
	}

	public void ToggleDoor() {
		if(isClosed) {
			Open();
		}
		else {
			Close();
		}
	}
}
