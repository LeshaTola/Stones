using UnityEngine;

public abstract class Creature : MonoBehaviour {
	[Header("Параметры сущности")]
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float rotationSpeed;
	[SerializeField] protected float health;
	[SerializeField] protected float damageAttack;
	[SerializeField] protected float timeBetweenMoves;
	[SerializeField] protected float timeBetweenAttacks;


	protected Vector3 targetPosition;
	protected Vector3 targetRotation;

	protected float moveTimer;
	protected float attackTimer;

	[SerializeField] LayerMask layerMask;

	protected void Move() {

		if (targetRotation.y < 0) targetRotation.y = 270f;
		if (targetRotation.y >= 360) targetRotation.y = 0;

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation),
		Time.deltaTime * rotationSpeed);
	}

	protected bool IsPositionEmpty(Vector3 position) {
		float sizeOfCheckBox = 0.2f;
		return !Physics.CheckBox(position, new Vector3(sizeOfCheckBox, sizeOfCheckBox, sizeOfCheckBox), Quaternion.identity, layerMask);
	}


}
