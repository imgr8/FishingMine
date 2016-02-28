using UnityEngine;
using System.Collections;
using System;

public class Treasure : ACatchable, ISaveFromEditor {

	public void Use(IFisher fisher)
    {
        fisher.Boat.PutStaff(this);
		base.Use (fisher);
    }

	public string path;

	public string Path {
		get {
			return this.path;
		}
	}

	public string Save() {
		return "";
	}

	public void Load(ISea sea, string param = "") {
		this.Sea = sea;
		//this.SetAction("SimpleFishBehaviour");	// Поскольку море не знает об объекте, устанавливаем поведение по-умолчанию сами, в последствии море уже будет само контролировать поведение
	}
}
