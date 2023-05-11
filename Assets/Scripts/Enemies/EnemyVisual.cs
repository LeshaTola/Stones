using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
	private const string STEP_TRIGGER = "Step";
	private Animator animator;

	private void Awake() {
		animator = GetComponent<Animator>();
	}


}
