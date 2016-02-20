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
		if (this.spinning.DirectionHookMove == DirectionHookMove.back || this.spinning.DirectionHookMove == DirectionHookMove.nowhere) {	// игнорируем ловлю на обратный ход, а также если просто рыбак в состоянии поиска
			return;
		}

		if (other.CompareTag ("Catchable")) {
			if (this.onCatchStaff != null) {		
				ICatchable catchedStaff = other.GetComponent<ICatchable> ();
				catchedStaff.StopAction (); // Остановим действие, которое выполнял объект
				this.onCatchStaff (catchedStaff);
			}
		}
	}


}
