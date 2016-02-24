using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Уровень, в котором надо собрать необходимый набор рыбы и в нужных колличествах
public class MyLevel_OnCertainCatch_1 : ILevel {

	IFishing fishing;
	ISea sea;
	IFisher fisher;

	Dictionary<string, int> fishAmount = new Dictionary<string, int>();

	int totalFishToCatch = 4;

	public void Init (IFishing fishing, ISea sea, IFisher fisher, Action method = null) {
		this.fishing = fishing;
		this.sea = sea;
		this.fisher = fisher;

		this.fishAmount.Add ("BW Fish", 1);
		this.fishAmount.Add ("Yellow Fish", 2);
		this.fishAmount.Add ("Blue Fish", 1);

		this.sea.OnDestroyCatchableObject += Foo;
	}



	void Foo(ICatchable catchable) {
		if (this.fishAmount.ContainsKey (catchable.Name)) {
			if (this.fishAmount [catchable.Name] > 0) {
				this.fishAmount [catchable.Name]--;
				this.totalFishToCatch--;
			}
		}
	}

	public bool Passed() {
		if (this.totalFishToCatch == 0) {
			return true;
		} else {
			return false;
		}
	}

	public void Unload() {
		this.sea.OnDestroyCatchableObject -= Foo;
	}
}