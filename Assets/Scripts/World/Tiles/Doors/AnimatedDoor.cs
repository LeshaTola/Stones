using System;
using UnityEngine;

public class AnimatedDoor : MonoBehaviour, IDoor
{
	public event Action OnDoorOpening;
	public event Action OnDoorClosing;

	private bool isClosed;
	private Tile tile;

	private void Start()
	{
		tile = GetComponent<Tile>();
		StartClosing();
	}

	public void StartClosing()
	{
		OnDoorClosing?.Invoke();
	}

	public void StartOpening()
	{
		OnDoorOpening?.Invoke();
	}

	public void Close()
	{
		isClosed = true;
		tile.Walkable = false;
	}

	public void Open()
	{
		isClosed = false;
		tile.Walkable = true;

	}

	public void ToggleDoor()
	{
		if (isClosed)
		{
			StartOpening();
		}
		else
		{
			StartClosing();
		}
	}
}
