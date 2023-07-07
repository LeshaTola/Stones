using UnityEngine;

public class CommonDoor : MonoBehaviour, IDoor
{
	[SerializeField] private GameObject door;
	private bool isClosed;
	private Tile tile;

	private void Start()
	{
		tile = GetComponent<Tile>();
		Close();
	}

	public void Close()
	{
		isClosed = true;
		tile.Walkable = false;
		door.SetActive(false);
	}

	public void Open()
	{
		isClosed = false;
		tile.Walkable = true;
		door.SetActive(true);
	}

	public void ToggleDoor()
	{
		if (isClosed)
		{
			Open();
		}
		else
		{
			Close();
		}
	}
}
