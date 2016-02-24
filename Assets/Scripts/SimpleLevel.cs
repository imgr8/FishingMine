using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Пример бонусного уровня, в котором нельзя проиграть (Поскольку бонусный уровень может не учитываться под номером, то здесь можно понизить номер, поскольку в уровень передается текущий номер)
public class SimpleLevel : ILevel {

	IFishing fishing;
	ISea sea;
	IFisher fisher;

	public void Init (IFishing fishing, ISea sea, IFisher fisher, Action method = null) {
		this.fishing = fishing;
		this.sea = sea;
		this.fisher = fisher;

		// Пример понижения уровня
		//this.fishing.NumLevel--;
	}

	public bool Passed() {
		return true;
	}

	public void Unload() {
		
	}

}

// Уровень в котором надо зарабатывать определенное колличество денег
// Колличество сколько надо заработать зависит от уровня
// Если уровень требует специфических цифр, то не надо менять здесь, поскольку это затронет
// предыдущие уровни, которые уже используют данный скрипт, поэтому, либо унаследоваться, либо просто создать новый

// TODO подумать над более универсальным алгоритмом! Пока так

public class MyLevel_OnMoney_1 : ILevel {

	IFishing fishing;
	ISea sea;
	IFisher fisher;
	float minimumMoneyBaseCoeff = 100;
	float income = 0;

	public void Init (IFishing fishing, ISea sea, IFisher fisher, Action method = null) {
		this.fishing = fishing;
		this.sea = sea;
		this.fisher = fisher;
		this.minimumMoneyBaseCoeff += 50 * (fishing.NumLevel - 1);

		this.fishing.OnEarnedUpdate += IncomeFunc;
	}

	void IncomeFunc(float incomeMoney) {
		this.income += incomeMoney;
	}

	public bool Passed() {
		if (this.income >= this.minimumMoneyBaseCoeff) {
			return true;
		} else {
			return false;
		}
	}

	public void Unload() {
		this.fishing.OnEarnedUpdate -= IncomeFunc;
	}
}

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