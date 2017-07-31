using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : bl_PhotonHelper {
	public bool isAttack = false;
	public bool isAim = false;

	// Use this for initialization
	void Start () {
		if (!isMine) {
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// one click
		if (Input.GetMouseButtonDown (0)) {
			isAttack = true;
		} else
			isAttack = false;

		// long click
		if (Input.GetMouseButton (1)) {
			isAim = true;
		} else
			isAim = false;
	}
}
