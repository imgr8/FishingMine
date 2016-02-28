using UnityEngine;
using System;
using System.Collections;


public class Fish : AMCatchable, IMovable, ISaveFromEditor {
	public override void Use(IFisher fisher) {
		fisher.Boat.PutStaff (this);
		base.Use (fisher);
	}

	public override ICatchable WhenCatched(IHook hook) {
		this.MoveBehaviour.StopMove();
		return this;
	}

	public string path;

	public string Path {
		get {
			return this.path;
		}
	}
		
	public string Save() {
		
		string parameters = "";
		/*
		short initParam = 0;	// None by default

		if (this.initialLook == InitialLook.Left) {
			initParam = 1;
		} else {
			initParam = 2;
		}

		parameters = initParam.ToString () + "@" +
		this.Weight.ToString () + "@" +
		this.Price.ToString () + "@" +
		this.speed.ToString ();
*/
		return parameters;
	}

	public void Load(ISea sea, string param) {
		/*
		this.Sea = sea;

		string [] parameters = param.Split (new char [] { '@' });

		int initLook = int.Parse (parameters [0]);

		if (initLook == 0) {
			this.initialLook = InitialLook.None;
		} else if (initLook == 1) {
			this.initialLook = InitialLook.Left;
		} else {
			this.initialLook = InitialLook.Right;
		}

		this.weight = float.Parse (parameters [1], System.Globalization.NumberStyles.Any);
		this.price = int.Parse (parameters [2]);
		this.speed = float.Parse (parameters[3], System.Globalization.NumberStyles.Any);
		*/
	}

}
