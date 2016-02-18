using UnityEngine;
using System.Collections;

public class Fisher : MonoBehaviour, IFisher {
	enum FisherState { Nothing, LookingFor, TryCatch, Pulling }

	public GameObject spinningGameObject;
	public GameObject boatGameObject;

	ISpinning spinning;
	IBoat boat;

	FisherState fisherState;

	float power = 1.0f;

	public float Power {
		get {
			return this.power;
		}

		set {
			if (value < 0) {
				value = 0;
			}

			this.power = value;
		}
	}

	void Awake() {
		this.spinning = this.spinningGameObject.GetComponent<ISpinning> ();

		if (this.spinning == null) {
			throw new UnityException ("Spinning are not initialized");
		}

		this.spinning.SetOwner (this);

		this.boat = this.boatGameObject.GetComponent<IBoat> ();

		if (this.boat == null) {
			throw new UnityException ("Boat are not initialized");
		}

		this.spinning.OnEndTryCatch += (ICatchable catchedStaff) => {
			Debug.Log(catchedStaff);

			if (catchedStaff != null) {
				this.boat.PutStaff(catchedStaff);
				GameObject.Destroy(catchedStaff.GameObject);
			}

			this.ChangeState(FisherState.LookingFor);
		};

		this.ChangeState (FisherState.Nothing); // Началное состояние обязательно задается в Awake

	}

	// Use this for initialization
	void Start () {

	}

	public void StartCatchFish () {
		this.ChangeState (FisherState.LookingFor);
	}

	void Update() {
		if (Input.GetMouseButtonDown (0) && this.fisherState == FisherState.LookingFor) {
			this.ChangeState (FisherState.TryCatch);
		}
	}

	void ChangeState(FisherState fisherState) {
		switch (fisherState) {
			case FisherState.Nothing:
				this.fisherState = FisherState.Nothing;
				break;
			case FisherState.LookingFor:
				this.fisherState = FisherState.LookingFor;
				this.spinning.LookingFor ();
				break;
			case FisherState.TryCatch:
				this.fisherState = FisherState.TryCatch;	
				this.spinning.TryCatch ();
				break;
			case FisherState.Pulling:
				this.fisherState = FisherState.Pulling;
				break;
		}
	}

}
