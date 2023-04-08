using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
	[Header ("Параметры сущности")]
    [SerializeField] protected float moveSpeed;
	[SerializeField] protected float rotationSpeed;
	[SerializeField] protected float health;
    [SerializeField] protected float damageAttack;

	protected Vector3 targetPosition;
	protected Vector3 targetRotation;

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
		//Debug.Log(!Physics(position, new Vector3(sizeOfCheckBox, sizeOfCheckBox, sizeOfCheckBox)));
		return !Physics.CheckBox(position, new Vector3(sizeOfCheckBox,sizeOfCheckBox,sizeOfCheckBox),Quaternion.identity, layerMask);
	}


}
