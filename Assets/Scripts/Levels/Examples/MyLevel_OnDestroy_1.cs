using UnityEngine;
using System;
using System.Collections;

// Уровень, в котором надо уничтожить все ICatchable объекты (либо поймать, либо подорвать)
// Если игрок уничтожил раньше времени можно доработать, чтобы уровень оповещал об этом
public class MyLevel_OnDestroy_1 : ILevel {

	IFishing fishing;
	ISea sea;
	IFisher fisher;

	int totalObjectForDestroying = 0;

	public void Init (IFishing fishing, ISea sea, IFisher fisher, Action method = null) {
		this.fishing = fishing;
		this.sea = sea;
		this.fisher = fisher;

		this.totalObjectForDestroying = this.sea.CountOfCatchableObjects;

		this.sea.OnDestroyCatchableObject += Foo;
	}

	void Foo(ICatchable catchable) {
		this.totalObjectForDestroying--;
	}

	public bool Passed() {
		if (this.totalObjectForDestroying == 0) {
			return true;
		} else {
			return false;
		}
	}

	public void Unload() {
		this.sea.OnDestroyCatchableObject -= Foo;
	}
}
