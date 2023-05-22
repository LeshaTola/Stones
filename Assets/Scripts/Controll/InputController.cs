using System;
using UnityEngine;

public class InputController : MonoBehaviour
{

	private IControllable controlablle;
	private GameInput gameInput;

	private void Awake()
	{
		gameInput = new GameInput();
		gameInput.Player.Enable();

		controlablle = GetComponent<IControllable>();

		if (controlablle == null)
		{
			throw new Exception($"There is no IContollable component: {gameObject.name}");
		}
	}

	private void OnEnable()
	{
		gameInput.Player.RotateLeft.performed += RotateLeft_performed;
		gameInput.Player.RotateRigth.performed += RotateRigth_performed;
		gameInput.Player.Attack.performed += OnAttack_performed;
		gameInput.Player.Interact.performed += Interact_performed;
	}

	private void OnDisable()
	{
		gameInput.Player.RotateLeft.performed -= RotateLeft_performed;
		gameInput.Player.RotateRigth.performed -= RotateRigth_performed;
		gameInput.Player.Attack.performed -= OnAttack_performed;
		gameInput.Player.Interact.performed -= Interact_performed;
	}

	private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		controlablle.Interact();
	}

	private void ReadMovment()
	{
		if (gameInput.Player.MoveRight.IsPressed())
		{
			controlablle.Move(transform.position + transform.right);
		}
		else if (gameInput.Player.MoveLeft.IsPressed())
		{
			controlablle.Move(transform.position - transform.right);
		}
		else if (gameInput.Player.MoveBack.IsPressed())
		{
			controlablle.Move(transform.position - transform.forward);
		}
		else if (gameInput.Player.MoveForward.IsPressed())
		{
			controlablle.Move(transform.position + transform.forward);
		}

	}

	private void Update()
	{
		ReadMovment();
		//ReadRotation();
	}

	private void OnAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		controlablle.Attack();
	}

	private void RotateRigth_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		controlablle.Rotate(transform.position + transform.right);
	}

	private void RotateLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		controlablle.Rotate(transform.position - transform.right);
	}
}
