using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : bl_PhotonHelper {
	public bool isAttack = false;

	// Use this for initialization
	void Start () {
		if (!isMine) {
			this.enabled = false;
		}
	}
}