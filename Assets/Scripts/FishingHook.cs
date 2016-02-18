using UnityEngine;
using System.Collections;

// Крючок НЕ цепляет ничего обратным ходом!

public class FishingHook : MonoBehaviour {

	public GameObject boat;	// Грузоподъемность
	public GameObject fishingHookCenter;
	public float length = 0.0f;
	public float maxLength = 5.0f;
	public float angleSpeed = 1.0f;
	public float maxAngleDeviation = 75.0f;
	public 	float catchSpeed = 3.0f;

	const float lengthByDefault = 1.0f;
	const float maxLengthByDefault = 5.0f;

	enum HookState {General, Catch, GoWithStuff};
	HookState hookState;

	delegate void HookBehaviourDelegate();
	HookBehaviourDelegate hookBehaviour;

	enum DirectionHookMove {forward, back};
	DirectionHookMove directionHookMove;

	// Use this for initialization
	void Start () {
		if (Mathf.Approximately (this.length, 0.0f)) {
			this.length = Vector3.Distance(this.transform.position, this.fishingHookCenter.transform.position);
		} else {
			if (this.length < 0) {
				this.length = FishingHook.lengthByDefault;
			}

			this.transform.position = new Vector3 (
				this.fishingHookCenter.transform.position.x, 
				this.fishingHookCenter.transform.position.y - this.length,	// минус, потому что крючок направлен вниз
				this.fishingHookCenter.transform.position.z
			);
		}

		if (this.maxLength <= this.length) {
			this.maxLength = FishingHook.maxLengthByDefault;
		}

		this.hookBehaviour = this.GeneralBehaviour;
		this.hookState = HookState.General;
		this.directionHookMove = DirectionHookMove.forward;
	}

	enum Direction {clockwise = 1, anticlockwise = -1};

	Direction directionOfMovemnet = Direction.clockwise;

	float angle = 0.0f;


	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && this.hookState == HookState.General) {
			this.ChangeState (HookState.Catch);
		}

		this.hookBehaviour.Invoke ();
	}

	Vector3 sourceVector;
	Vector3 destinationVector; 
	Vector3 boatPositionWhenHookedThrowen;

	void ChangeState(HookState hookState) {
		switch (hookState) {
		case HookState.Catch:
			this.destinationVector = (this.transform.position - this.fishingHookCenter.transform.position).normalized * this.maxLength;	
				this.sourceVector = this.transform.position;
				this.boatPositionWhenHookedThrowen = this.boat.transform.position;
				this.hookBehaviour = this.CatchBehaviour;
				this.hookState = HookState.Catch;	
				break;
			case HookState.General:
				this.hookBehaviour = this.GeneralBehaviour;	
				this.hookState = HookState.General;
				break;
			case HookState.GoWithStuff:
				this.hookBehaviour = this.GoWithStuffBehaviour;	
				this.hookState = HookState.GoWithStuff;	
				break;
		}
	}

	void GeneralBehaviour() {
		if (angle > this.maxAngleDeviation && this.directionOfMovemnet == Direction.clockwise) {
			this.directionOfMovemnet = Direction.anticlockwise;
		} else if (angle <- this.maxAngleDeviation && this.directionOfMovemnet == Direction.anticlockwise) {
			this.directionOfMovemnet = Direction.clockwise;
		}

		if (this.directionOfMovemnet == Direction.clockwise) {
			this.angle += Time.deltaTime * this.angleSpeed;
		} else {
			this.angle -= Time.deltaTime * this.angleSpeed;
		}

		float newXCoord = this.length * Mathf.Sin (Mathf.Deg2Rad * this.angle);
		float newYCoord = this.length * Mathf.Cos (Mathf.Deg2Rad * this.angle); //-Mathf.Sqrt(this.length * this.length - newXCoord * newXCoord);

		this.transform.position = new Vector3 (
			this.fishingHookCenter.transform.position.x + newXCoord, 
			this.fishingHookCenter.transform.position.y + newYCoord, 
			this.transform.position.z
		);
	}

	GameObject catchedStuff = null;

	void OnTriggerEnter2D(Collider2D other) {
		if (this.directionHookMove == DirectionHookMove.back) {	
			return;	// Если крючок движется назад, то ничего не цепляем
		}

		this.catchedStuff = other.gameObject;
		this.ChangeState (HookState.GoWithStuff);
	}

	void GoWithStuffBehaviour() {
		float step = (catchSpeed / 2) * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, this.sourceVector, step);
		this.catchedStuff.transform.position = this.transform.position;

		if (Vector3.Distance (this.transform.position, this.sourceVector) < 0.1f) {
			this.directionHookMove = DirectionHookMove.forward;
			this.transform.position = this.sourceVector;
			GameObject.Destroy (this.catchedStuff);
			this.catchedStuff = null;
			this.ChangeState (HookState.General);
		}
	}

	void CatchBehaviour() {
		Vector3 boatDeltaPosition = this.boatPositionWhenHookedThrowen - this.boat.transform.position;

		float step = catchSpeed * Time.deltaTime;

		if (this.directionHookMove == DirectionHookMove.forward) {
			transform.localPosition += this.destinationVector * Time.deltaTime * this.catchSpeed;
		} else if (this.directionHookMove == DirectionHookMove.back) {
			transform.localPosition -= this.destinationVector * Time.deltaTime * this.catchSpeed;
		}



		/*
		if (Vector3.Distance (this.transform.position, this.destinationVector) < 0.1f && this.directionHookMove == DirectionHookMove.forward) {
			this.directionHookMove = DirectionHookMove.back;
		} else if (Vector3.Distance (this.transform.position, this.sourceVector) < 0.1f && this.directionHookMove == DirectionHookMove.back) {
			this.directionHookMove = DirectionHookMove.forward;
			this.transform.position = this.sourceVector;
			this.ChangeState (HookState.General);
		}
		*/
	}
		
}
