using UnityEngine;
using System;
using System.Collections;

public class SurpriseBottle : AMCatchable, ISaveFromEditor {

	public override void Use(IFisher fisher) {

		PowerBonus powerUp = new PowerBonus ();
		powerUp.Use (fisher, this.sea, null);

		base.Use (fisher);
	}

	public override ICatchable WhenCatched(IHook hook) {
		this.MoveBehaviour.StopMove ();

		return this;
	}

	public string path;

	public string Path {
		get {
			return this.path;
		}
	}

	public void Load(ISea sea, string param = "") {
		this.Sea = sea;
		//this.SetAction(this.DefaultAction);	// Поскольку море не знает об объекте, устанавливаем поведение по-умолчанию сами, в последствии море уже будет само контролировать поведение
	}

	public string Save() {
		return "";
	}
}
