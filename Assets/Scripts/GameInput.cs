using System;
using UnityEngine;

public class GameInput : MonoBehaviour {

	public event EventHandler OnTurnLeft;
	public event EventHandler OnTurnRight;

	public event EventHandler OnAttack;
	public event EventHandler OnInteract;

	PlayerInputAction inputActions;

	private void Start() {
		inputActions = new PlayerInputAction();
		inputActions.Player.Enable();
		inputActions.Player.RotateLeft.performed += RotateLeft_performed;
		inputActions.Player.RotateRigth.performed += RotateRigth_performed;
		inputActions.Player.Attack.performed += OnAttack_performed;
		inputActions.Player.Interact.performed += Interact_performed;

	}

	private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		OnInteract?.Invoke(this, EventArgs.Empty);
	}

	public bool OnMoveForward() {
		return inputActions.Player.MoveForward.IsPressed();
	}
	public bool OnMoveBack() {
		return inputActions.Player.MoveBack.IsPressed();
	}
	public bool OnMoveRight() {
		return inputActions.Player.MoveRight.IsPressed();
	}
	public bool OnMoveLeft() {
		return inputActions.Player.MoveLeft.IsPressed();
	}

	private void OnAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		OnAttack?.Invoke(this, EventArgs.Empty);
	}

	private void RotateRigth_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		OnTurnRight?.Invoke(this, EventArgs.Empty);
	}

	private void RotateLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		OnTurnLeft?.Invoke(this, EventArgs.Empty);
	}


}
