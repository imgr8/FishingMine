using UnityEngine;
using System.Collections;

public abstract class AMCatchable : ACatchable, IMovable {
	protected IMove moveBehaviour;

	public IMove MoveBehaviour {
		get {
			return this.moveBehaviour;
		}

		set {
			this.moveBehaviour = value;
		}
	}

	protected virtual void Start() {
		this.moveBehaviour = this.GetComponent<IMove> ();
		if (this.moveBehaviour == null) {
			this.moveBehaviour = this.gameObject.AddComponent<EmptyMove> ();
		}

		this.moveBehaviour.Init (this.sea);
	}

	protected virtual void Update() {
		this.moveBehaviour.Move ();
	}
}
