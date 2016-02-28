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
		
		string parameters = this.Name + "@" + this.weight + "@" + this.price + "\n";

		ISaveComponent [] saveComponents = this.GetComponents<ISaveComponent> ();
	
		foreach (ISaveComponent saveComponent in saveComponents) {
			parameters += "<component>\n";
			parameters += saveComponent.Save ();
			parameters += "\n<\\component>";
		}

		return parameters;
	}

	public void Load(ISea sea, string param) {
		
		this.Sea = sea;


		string [] parameters = param.Split (new char [] { '@' });
		/*
		int initLook = int.Parse (parameters [0]);

		if (initLook == 0) {
			this.initialLook = InitialLook.None;
		} else if (initLook == 1) {
			this.initialLook = InitialLook.Left;
		} else {
			this.initialLook = InitialLook.Right;
		}
		*/

		this.objectName = parameters [0];
		this.weight = float.Parse (parameters [1], System.Globalization.NumberStyles.Any);
		this.price = int.Parse (parameters [2]);
	}

}
