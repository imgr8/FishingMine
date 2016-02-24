using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Пример бонусного уровня, в котором нельзя проиграть (Поскольку бонусный уровень может не учитываться под номером, то здесь можно понизить номер, поскольку в уровень передается текущий номер)
public class MyLevel_Bonus_1 : ILevel {

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