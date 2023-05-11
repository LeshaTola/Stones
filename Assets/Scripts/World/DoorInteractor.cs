using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractor : MonoBehaviour, IInteractable {

	[SerializeField] private GameObject doorGameObject;

	private IDoor door;

	private void Awake() {
		door = doorGameObject.GetComponent<IDoor>();
	}

	public void Interact() {
		door.ToggleDoor();
	}
}
