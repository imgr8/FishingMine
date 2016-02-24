using UnityEngine;
using System;
using System.Collections;

public class PowerBonus : Bonus {

	public override float Weight {
		get {
			return 5.0f;
		}
	}

	IFisher fisher;

	public override void Use(IFisher fisher) {
		fisher.Power++;
		Debug.Log (fisher.Power);

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


public class SlowSpeedBonus : Bonus {
	float weight = 1.0f;

	public override float Weight {
		get {
			return this.weight;
		}
	}

	ISea sea = null;

	float changeSpeedCoefficient = 0.75f;

	public SlowSpeedBonus(ISea sea, float changeSpeedCoefficient = 0.75f, float weight = 1.0f) {
		this.sea = sea;
		this.weight = weight;
		this.changeSpeedCoefficient = changeSpeedCoefficient;
	}

	public override void Use(IFisher fisher) {
		this.sea.OnEveryCatchableObject ((obj) => {
			obj.ChangeSpeed(this.changeSpeedCoefficient);
		});

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
