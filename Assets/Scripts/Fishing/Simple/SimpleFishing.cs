using UnityEngine;
using System;
using System.Collections;

public class SimpleFishing : MonoBehaviour, IFishing {
	public GameObject fisherGameObject;
	IFisher fisher;

	public GameObject seaGameObject;
	ISea sea;

	public GameObject timerGameObject;
	ITimer timer;

	float earned = 0;	// Заработанное

	float Earned {
		get {
			return this.earned;
		}
	}

	ushort numLevel = 0;

	ushort NumLevel {
		set {
			this.numLevel = value;

			if (this.onLevelUpdate != null) {
				this.onLevelUpdate.Invoke (value);
			}
		}

		get {
			return this.numLevel;
		}
	}

	float levelTimeAmount = 60.0f;

	float levelTimeRemainded = 0;

	// Use this for initialization
	void Start () {
		this.sea = this.seaGameObject.GetComponent<ISea> ();

		if (this.sea == null) {
			throw new UnityException ("Sea are not initialized");
		}

		this.fisher = this.fisherGameObject.GetComponent<IFisher> ();

		if (this.fisher == null) {
			throw new UnityException ("Fisher are not initialized");
		}

		this.fisher.Boat.OnPutStaff += (ICatchable obj) => {
			this.earned += obj.Price;

			if (this.onEarnedUpdate != null) {
				this.onEarnedUpdate.Invoke(this.earned);
			}

		};

		this.timer = this.timerGameObject.GetComponent<ITimer> ();

		if (this.timer == null) {
			throw new UnityException ("Timer are not initialized");
		}

		this.timer.OnTimerStart += (float time) => {
			this.levelTimeRemainded = time;

			if (this.onChangeLevelTime != null) {
				this.onChangeLevelTime.Invoke(time);
			}
		};

		this.timer.OnTimerEnd += (float time) => {
			this.levelTimeRemainded = 0;

			if (this.onChangeLevelTime != null) {
				this.onChangeLevelTime.Invoke(time);
			}

			this.FinishLevel();
		};

		this.timer.OnTimerBeep += (float time) => {
			this.levelTimeRemainded = this.levelTimeAmount - time;

			if (this.onChangeLevelTime != null) {
				this.onChangeLevelTime.Invoke(this.levelTimeRemainded);
			}
		};

		this.IfCondiotionPassedStartNextLevel ();
			
	}

	void IfCondiotionPassedStartNextLevel() {
		if (this.NumLevel == 0) {
			this.NextLevel ();
		} else if (this.CheckConditions ()) {
			this.NextLevel ();
		} else {
			this.GameOver ();
		}
	}

	void FinishLevel() {
		this.fisher.StopCatchFish ();
		this.sea.Clear ();
		this.IfCondiotionPassedStartNextLevel ();
	}
		
	bool CheckConditions() {
		return true;
	}

	void NextLevel() {
		this.NumLevel++;

		this.sea.MakeLive ();

		this.fisher.Boat.Sea = this.sea;

		this.timer.StartTimer (this.levelTimeAmount, 1.0f);

		this.fisher.StartCatchFish ();
	}

	void GameOver() {
		this.numLevel = 0;
		this.earned = 0;
	}

	event Action<float> onChangeLevelTime;

	public event Action<float> OnChangeLevelTime {
		add {
			this.onChangeLevelTime += value;
		}

		remove {
			this.onChangeLevelTime -= value;	
		}
	}


	event Action<ushort> onLevelUpdate;

	public event Action<ushort> OnLevelUpdate {
		add {
			this.onLevelUpdate += value;
		}

		remove {
			this.onLevelUpdate -= value;	
		}
	}

	event Action<float> onEarnedUpdate;

	public event Action<float> OnEarnedUpdate {
		add {
			this.onEarnedUpdate += value;
		}

		remove {
			this.onEarnedUpdate -= value;	
		}
	}

	event Action<object> onStateUpdate;

	public event Action<object> OnStateUpdate {
		add {
			this.onStateUpdate += value;
		}

		remove {
			this.onStateUpdate -= value;	
		}
	}

}
