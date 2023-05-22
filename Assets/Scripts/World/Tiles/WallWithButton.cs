using UnityEngine.Events;

public class WallWithButton : AbstractInteractable
{
	public UnityEvent OnInteract;

	public override bool CanInteract(Player player)
	{
		return true;
	}

	public override void Interact(Player player)
	{
		OnInteract?.Invoke();
	}
}
