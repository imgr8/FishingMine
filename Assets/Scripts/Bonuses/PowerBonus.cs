using UnityEngine;
using System;
using System.Collections;

public class PowerBonus : Bonus {
 
	public override void Use(IFisher fisher) {
		fisher.Power++;
		Debug.Log ("Power Up");

		if (this.onUsed != null) {
			this.onUsed.Invoke (this);
		}
	}

	event Action<ICatchable> onUsed;

	public override event Action<ICatchable> OnUsed {
		add {
			this.onUsed += value;
		}

		remove {
			this.onUsed -= value;
		}
	}

}


