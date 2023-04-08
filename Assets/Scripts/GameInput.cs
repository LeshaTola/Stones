using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

	public event EventHandler TurnLeft;
	public event EventHandler TurnRight;

	public event EventHandler MoveForward;
	public event EventHandler MoveBack;
	public event EventHandler MoveRight;
	public event EventHandler MoveLeft;
	
	public event EventHandler Attack;

	PlayerInputAction inputActions;

	private void Start() {
		inputActions = new PlayerInputAction();
		inputActions.Player.Enable();
		inputActions.Player.OnRotateLeft.performed += OnRotateLeft_performed;
		inputActions.Player.OnRotateRigth.performed += OnRotateRigth_performed;
		inputActions.Player.OnMoveForward.performed += OnMoveForward_performed;
		inputActions.Player.OnMoveBack.performed += OnMoveBack_performed;
		inputActions.Player.OnMoveLeft.performed += OnMoveLeft_performed;
		inputActions.Player.OnMoveRight.performed += OnMoveRight_performed;
		inputActions.Player.OnAttack.performed += OnAttack_performed;
	}

	private void OnAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		Attack?.Invoke(this, EventArgs.Empty);
	}

	private void OnMoveRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		MoveRight?.Invoke(this, EventArgs.Empty);
	}

	private void OnMoveLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		MoveLeft?.Invoke(this, EventArgs.Empty);
	}

	private void OnMoveBack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		MoveBack?.Invoke(this, EventArgs.Empty);
	}

	private void OnMoveForward_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		MoveForward?.Invoke(this, EventArgs.Empty);
	}

	private void OnRotateRigth_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		TurnRight?.Invoke(this, EventArgs.Empty);
	}

	private void OnRotateLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		TurnLeft?.Invoke(this, EventArgs.Empty);
	}


}
