using UnityEngine;
using System;
using System.Collections;

public class PowerBonus : IBonus {

	IFisher fisher;

	public void Use(IFisher fisher, ISea sea, IFishing fishing) {
		fisher.Power++;
		Debug.Log (fisher.Power);

		if (this.onUsed != null) {
			this.onUsed.Invoke (this);
		}
	}

	event Action<IBonus> onUsed;

	public event Action<IBonus> OnUsed {
		add {
			this.onUsed += value;
		}

		remove {
			this.onUsed -= value;
		}
	}

}