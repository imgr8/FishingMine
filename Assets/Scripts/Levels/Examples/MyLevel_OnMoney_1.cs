using UnityEngine;
using System;
using System.Collections;

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
