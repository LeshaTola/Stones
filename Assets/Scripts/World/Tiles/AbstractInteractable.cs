using UnityEngine;

public abstract class AbstractInteractable : MonoBehaviour
{
	public abstract void Interact(Player player);
	public abstract bool CanInteract(Player player);
}
