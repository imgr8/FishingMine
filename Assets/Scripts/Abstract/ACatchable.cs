using UnityEngine;
using System;
using System.Collections;

public abstract class ACatchable :  MonoBehaviour, ICatchable {
	public string objectName = "Catchable";

	public string Name {
		get {
			return this.objectName;
		}
	}

	public float weight = 1.0f;

	public float Weight {
		get {
			return this.weight;
		}
	}

	public int price = 100;

	public int Price {
		get {
			return this.price;
		}
	}

	protected ISea sea;

	public ISea Sea {
		set {
			this.sea = value;
		} 

		get {
			return this.sea;
		}
	}

	public virtual GameObject GameObject {
		get {
			return this.gameObject;
		}
	}

	public virtual void ChangeWeight (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		this.weight *= ratio;	
	}

	public virtual void ChangePrice (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		int tmp = (int)(this.price * ratio);

		this.price = tmp;	
	}

	protected event Action<ICatchable> onUsed;

	public event Action<ICatchable> OnUsed {
		add {
			this.onUsed += value;
		}

		remove {
			this.onUsed -= value;
		}
	}

	public virtual void Use(IFisher fisher) {
		if (this.onUsed != null) {
			this.onUsed.Invoke (this);
		}
	}

	public virtual ICatchable WhenCatched(IHook hook) {
		return this;
	}

	public virtual void Destroy() {
		if (this.sea != null) {
			this.sea.DestroyObject (this);
		} else {
			GameObject.Destroy (this.GameObject);
		}
	}
}
