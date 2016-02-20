using UnityEngine;
using System;
using System.Collections;

public class SimpleSpinning : MonoBehaviour, ISpinning {

	//public GameObject boat;
	public GameObject fishingHookCenterOfRotation;	// Точка, центр, вращения (в рамках поставленной задачи может представлять конец удочки, место крепления лески.)
	public GameObject fishingHookPivotPoint;		// Точка представляющая место элемента вращения (в рамках поставленной задачи может представлять конец удочки, место под плавок)
	public GameObject fishingHookGameObject; // Должен быть типом IHook!

	IHook fishingHook;

	public float length = 1.0f;						// Минимальная длина
	public float maxLength = 5.0f;					// Максимальная длина
	public float angleSpeed = 1.0f;
	public float maxAngleDeviation = 75.0f;
	public float catchSpeed = 3.0f;

	const float lengthByDefault = 1.0f;
	const float maxLengthByDefault = 5.0f;
	const float angleSpeedByDefault = 10.0f;
	const float maxAngleDeviationByDefault = 75.0f;
	const float catchSpeedByDefault = 1.0f;

	enum SpinningState { Nothing, LookingFor, TryCatch, PullStuff };
	SpinningState spinningState;

	delegate void SpinningBehaviourDelegate();
	SpinningBehaviourDelegate spinningBehaviour;

	DirectionHookMove directionHookMove;

	enum Direction { clockwise = 1, anticlockwise = -1 };

	Direction directionOfMovemnet = Direction.clockwise;

	ICatchable catchedStuff = null;

	IFisher owner;

	void Awake() {
		this.ChangeState (SpinningState.Nothing);	// Начальное состояние обязательно в Awake!!!
	}

	// Use this for initialization
	void Start () {
		// Устанавливаем крючок, в соответствии с заданной позицией

		if (Mathf.Approximately (this.length, 0.0f)) {	// Если длина крючка равна 0, то используем расположение, заданное в редакторе
			this.length = Vector3.Distance(this.fishingHookPivotPoint.transform.position, this.fishingHookCenterOfRotation.transform.position);
		} else {
			if (this.length < 0) {
				this.length = SimpleSpinning.lengthByDefault;
			}

			this.fishingHookPivotPoint.transform.position = new Vector3 (
				this.fishingHookCenterOfRotation.transform.position.x, 
				this.fishingHookCenterOfRotation.transform.position.y - this.length,	// минус, потому что крючок направлен вниз
				this.fishingHookCenterOfRotation.transform.position.z
			);
		}

		if (this.maxLength <= this.length) {
			this.maxLength = SimpleSpinning.maxLengthByDefault;
		}

		if (this.angleSpeed <= 0) {
			this.angleSpeed = SimpleSpinning.angleSpeedByDefault;
		}

		if (this.maxAngleDeviation < 0.0f || this.maxAngleDeviation > 90.0f) {
			this.maxAngleDeviation = SimpleSpinning.maxAngleDeviationByDefault;
		}

		if (this.catchSpeed < 0) {
			this.catchSpeed = SimpleSpinning.catchSpeedByDefault;
		}

		this.directionHookMove = DirectionHookMove.nowhere;

		this.fishingHook = this.fishingHookGameObject.GetComponent<IHook> ();

		this.fishingHook.SetOwner (this);

		this.fishingHook.OnCatchStaff += (ICatchable obj) => {
			this.catchedStuff = obj;
			this.ChangeState(SpinningState.PullStuff);
		};
	}

	public void SetOwner(IFisher fisher) {
		this.owner = fisher;
	}

	public	DirectionHookMove DirectionHookMove {
		get {
			return this.directionHookMove;
		}
	}

	// Вращение крючка
	// maxAngleDeviation - максимальное отклонение крючка в одну сторону в градусах (Не больше 90 нрадусов)
	float currentAngleDeviation = 0.0f;

	/// <summary>
	/// Lookings for.
	/// </summary>
	public void LookingFor () {
		this.ChangeState (SpinningState.LookingFor);
	}

	void LookingForBehaviour() {
		if (this.currentAngleDeviation > this.maxAngleDeviation && this.directionOfMovemnet == Direction.clockwise) {
			this.directionOfMovemnet = Direction.anticlockwise;
		} else if (this.currentAngleDeviation <- this.maxAngleDeviation && this.directionOfMovemnet == Direction.anticlockwise) {
			this.directionOfMovemnet = Direction.clockwise;
		}

		if (this.directionOfMovemnet == Direction.clockwise) {
			this.currentAngleDeviation += Time.deltaTime * this.angleSpeed;
		} else {
			this.currentAngleDeviation -= Time.deltaTime * this.angleSpeed;
		}

		float newXCoord = this.length * Mathf.Sin (Mathf.Deg2Rad * this.currentAngleDeviation);
		float newYCoord = -Mathf.Sqrt(this.length * this.length - newXCoord * newXCoord);

		this.fishingHookPivotPoint.transform.position = new Vector3 (
			this.fishingHookCenterOfRotation.transform.position.x + newXCoord, 
			this.fishingHookCenterOfRotation.transform.position.y + newYCoord, 
			this.fishingHookPivotPoint.transform.position.z
		);

	}
		
	public void TryCatch () {
		this.directionHookMove = DirectionHookMove.forward;
		this.ChangeState (SpinningState.TryCatch);	
	}

	void TryCatchBehaviour() {

		float step = catchSpeed * Time.deltaTime;

		if (this.directionHookMove == DirectionHookMove.forward) {
			this.fishingHookGameObject.transform.localPosition += this.destinationNormalVector * Time.deltaTime * this.catchSpeed;
		} else if (this.directionHookMove == DirectionHookMove.back) {
			this.fishingHookGameObject.transform.localPosition -= this.destinationNormalVector * Time.deltaTime * this.catchSpeed;
		}
			
		if (Vector3.Distance (
			this.fishingHookGameObject.transform.position, this.fishingHookPivotPoint.transform.position) >= this.maxLength && 
			this.directionHookMove == DirectionHookMove.forward
		) 
		{
			this.directionHookMove = DirectionHookMove.back;
		} 
		else if (Vector3.Distance (
			this.fishingHookPivotPoint.transform.position, this.fishingHookGameObject.transform.position) < 0.1f && 
			this.directionHookMove == DirectionHookMove.back
		) 
		{
			this.directionHookMove = DirectionHookMove.forward;
			this.fishingHookGameObject.transform.position = this.fishingHookPivotPoint.transform.position;

			if (this.onEndTryCatch != null) {
				this.onEndTryCatch.Invoke (this.catchedStuff);
			}

			this.ChangeState (SpinningState.LookingFor);

		}

	}
		
	public void PullStaff() {
		this.ChangeState (SpinningState.PullStuff);	
	}

	void PullStaffBehaviour() {
		this.directionHookMove = DirectionHookMove.back;

		float resultCatchSpeed = this.catchSpeed * this.owner.Power / this.catchedStuff.Weight;

		this.fishingHookGameObject.transform.localPosition -= this.destinationNormalVector * Time.deltaTime * resultCatchSpeed;

		this.catchedStuff.GameObject.transform.position = this.fishingHookGameObject.transform.position;

		if (Vector3.Distance (
			this.fishingHookPivotPoint.transform.position, this.fishingHookGameObject.transform.position) < 0.1f) 
		{
			this.directionHookMove = DirectionHookMove.forward;
			this.fishingHookGameObject.transform.position = this.fishingHookPivotPoint.transform.position;

			if (this.onEndTryCatch != null) {
				this.onEndTryCatch.Invoke (this.catchedStuff);
			}

			this.ChangeState (SpinningState.LookingFor);

		}
	}

	void EmptyBehaviour() {
		// Поведение по-умолчанию
	}

	// Update is called once per frame
	void Update () {
		this.spinningBehaviour.Invoke ();
	}

	Vector3 destinationNormalVector; 

	void ChangeState(SpinningState spinningState) {
		switch (spinningState) {
			case SpinningState.Nothing:
				this.spinningBehaviour = this.EmptyBehaviour;
				this.spinningState = SpinningState.Nothing;
				break;
		case SpinningState.LookingFor:
				this.spinningBehaviour = this.LookingForBehaviour;
				this.catchedStuff = null;
				this.spinningState = SpinningState.LookingFor;
				break;
			case SpinningState.TryCatch:
				this.destinationNormalVector = (this.fishingHookPivotPoint.transform.position - this.fishingHookCenterOfRotation.transform.position).normalized /* *this.maxLength*/;	
				this.spinningBehaviour = this.TryCatchBehaviour;
				this.spinningState = SpinningState.TryCatch;	
				
				if (this.onStartTryCatch != null) {
					this.onStartTryCatch.Invoke();
				}

				break;
			case SpinningState.PullStuff:
				this.spinningBehaviour = this.PullStaffBehaviour;	
				this.spinningState = SpinningState.PullStuff;	
				break;
		}
	}

	/// <summary>
	/// G//////////////////////////	/// </summary>

	event Action onStartTryCatch; // Событие вызывается, когда стартуется попытка поймать

	public event Action OnStartTryCatch {
		add {
			this.onStartTryCatch += value;
		}

		remove {
			this.onStartTryCatch -= value;
		}
	}

	event Action<ICatchable> onEndTryCatch;

	public event Action<ICatchable> OnEndTryCatch {		// Событие вызывается, когда заканчивается попытка поймать (крючок возвращается)
		add {
			this.onEndTryCatch += value;
		}	

		remove {
			this.onEndTryCatch -= value;
		}
	}

	event Action<ICatchable> onCatchStaff;

	public event Action<ICatchable> OnCatchStaff {		// Событие вызывается когда, что-то поймано
		add {
			this.onCatchStaff += value;
		}

		remove {
			this.onCatchStaff -= value;
		}
	}
}
