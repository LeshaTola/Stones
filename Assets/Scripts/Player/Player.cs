using UnityEngine;

public class Player : Creature, IControllable {

	[SerializeField] private GameInput gameInput;

	private void Start() {
		Init();
	}

	public void Move(Vector3 direction) {
		movement.TryMoveToTargetPosition(GetVector2IntPosition(direction));
	}

	public void Rotate(Vector3 direction) {
		movement.RotateToPosition(GetVector2IntPosition(direction));
	}

	public void Attack() {
		Debug.Log("Attack");
	}

	public void Interact() {
		Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, 1f);
		var hitCollider = raycastHit.collider;
		if (hitCollider != null) {

			if (hitCollider.TryGetComponent<IInteractable>(out IInteractable interactObject)) {
				interactObject.Interact();
			}
		}
	}
}