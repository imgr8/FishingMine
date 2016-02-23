using UnityEngine;
using System;
using System.Collections;

public class SimpleLevel : ILevel {

	IFishing fishing;
	ISea sea;
	IFisher fisher;

	void Init (IFishing fishing, ISea sea, IFisher fisher, ushort levelNum = 0, Action method = null) {
		this.fishing = fishing;
		this.sea = sea;
		this.fisher = fisher;
	}

	public bool Passed() {
		return true;
	}

}
