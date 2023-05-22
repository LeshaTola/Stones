using UnityEngine;

public class DoorInteractor : AbstractInteractable
{

	[SerializeField] private GameObject doorGameObject;

	private IDoor door;

	private void Awake()
	{
		door = doorGameObject.GetComponent<IDoor>();
	}

	public override void Interact(Player player)
	{
		door.ToggleDoor();
	}

	public override bool CanInteract(Player player)
	{
		return true;
	}
}
