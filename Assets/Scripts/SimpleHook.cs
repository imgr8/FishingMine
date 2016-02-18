using UnityEngine;
using System.Collections;
using System;

public class SimpleHook : MonoBehaviour, IHook {

	event Action<ICatchable> onCatchStaff;

	public event Action<ICatchable> OnCatchStaff {
		add {
			this.onCatchStaff += value;
		}

		remove {
			this.onCatchStaff -= value;
		}
	}

	ISpinning spinning;

	public void SetOwner(ISpinning spinning) {
		this.spinning = spinning;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Catchable")) {
			if (this.onCatchStaff != null) {		
				this.onCatchStaff (other.GetComponent<ICatchable> ());
			}
		}
	}


}
