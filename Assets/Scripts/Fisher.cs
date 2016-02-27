using UnityEngine;
using System.Collections;

public class Fisher : MonoBehaviour, IFisher {
	enum FisherState { Nothing, LookingFor, TryCatch, Pulling }

	public GameObject spinningGameObject;
	public GameObject boatGameObject;

	public float delayBetweenUsingCatchedStaff = 0.0f;  // Задержка между моментом, когда выловлен предмет и новым запуском удочки (может быть использована, 
														// например, для показа надвиси и выловденного)

	ISpinning spinning;
	IBoat boat;

	FisherState fisherState;

	public ISpinning Spinning {
		get {
			return this.spinning;
		}

		set {
			this.spinning = value;
		}
	}

	public IBoat Boat {
		get {
			return this.boat;
		}

		set {
			this.boat = value;
		}
	}

	public float power = 1.0f;

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

    int powerLevel = 1;
    public int PowerLevel
    {
        get { return this.powerLevel; }
        set { this.powerLevel = value; }
    }
     public float extraPower = 0f;
     public float ExtraPower
     {
         get { return this.extraPower; }
         set { this.extraPower = value; }
     }

	public void ClearState() {
		this.extraPower = 0f;
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
	
			if (catchedStaff != null) {
				catchedStaff.Use(this);

				StartCoroutine(StartAfterDelay());

			} else {
				this.ChangeState(FisherState.LookingFor);
			}

		};

		this.ChangeState (FisherState.Nothing); // Началное состояние обязательно задается в Awake

	}

	IEnumerator StartAfterDelay() {
		this.ChangeState(FisherState.Nothing);

		yield return new WaitForSeconds (this.delayBetweenUsingCatchedStaff);

		this.ChangeState(FisherState.LookingFor);
	}

	// Use this for initialization
	void Start () {

	}

	public void StartCatchFish () {
		this.ChangeState (FisherState.LookingFor);
	}

	public void StopCatchFish () {
		this.ChangeState (FisherState.Nothing);
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
				this.spinning.Nothing ();
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
