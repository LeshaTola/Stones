using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

	public event EventHandler OnTurnLeft;
	public event EventHandler OnTurnRight;
	
	public event EventHandler OnAttack;

	PlayerInputAction inputActions;

	private void Start() {
		inputActions = new PlayerInputAction();
		inputActions.Player.Enable();
		inputActions.Player.RotateLeft.performed += OnRotateLeft_performed;
		inputActions.Player.RotateRigth.performed += OnRotateRigth_performed;
		inputActions.Player.Attack.performed += OnAttack_performed;

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

	private void OnRotateRigth_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		OnTurnRight?.Invoke(this, EventArgs.Empty);
	}

	private void OnRotateLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		OnTurnLeft?.Invoke(this, EventArgs.Empty);
	}


}
