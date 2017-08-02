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
		if (!isAttack) {
			// one click
			if (Input.GetMouseButtonDown (0)) {
				isAttack = true;
			}
		}

		if (!isAim) {
			// long click
			if (Input.GetMouseButton (1)) {
				isAim = true;
			}
		}
	}
}
