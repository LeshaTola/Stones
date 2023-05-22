using UnityEngine;

public interface IControllable
{
	public void Move(Vector3 direction);
	public void Rotate(Vector3 direction);
	public void Attack();
	public void Interact();

}
