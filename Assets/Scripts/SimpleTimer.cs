using UnityEngine;
using System;
using System.Collections;

public class SimpleTimer : MonoBehaviour, ITimer {
	float time = 0;		// общее время, прошедшее от начала запуска таймера
	float msgTime = 0;	// время интервала между вызовами сообщения
	float seconds = 0;	// общее время таймера

	public void StartTimer(float seconds, float msgTime) {
		if (IsInvoking ("Timer")) {
			CancelInvoke ("Timer");	

			if (this.onTimerEnd != null) {
				this.onTimerEnd.Invoke (this.time);
			}
		}

		this.time = 0;
		this.msgTime = msgTime;
		this.seconds = seconds;

		if (this.onTimerStart != null) {
			this.onTimerStart.Invoke (this.seconds);	// При старте показывает общее время (напримре, чтобы дать алгоритму подготовить нужнве данные)
		}

		InvokeRepeating ("Timer", msgTime, msgTime);
	}

	public void Pause() {
		throw new UnityException ("Not realized");
	}

	public void Resume() {
		throw new UnityException ("Not realized");
	}

	event Action<float> onTimerStart;

	public event Action<float> OnTimerStart {
		add {
			this.onTimerStart += value;
		}

		remove {
			this.onTimerStart -= value;
		}

	}

	event Action<float> onTimerEnd;

	public event Action<float> OnTimerEnd {
		add {
			this.onTimerEnd += value;
		}

		remove {
			this.onTimerEnd -= value;
		}
	}

	event Action<float> onTimerBeep;

	public event Action<float> OnTimerBeep {
		add {
			this.onTimerBeep += value;
		}

		remove {
			this.onTimerEnd -= value;
		}
	}

	void Timer() {
		time += msgTime;

		if (time >= this.seconds) {
			CancelInvoke ("Timer");

			if (this.onTimerEnd != null) {
				this.onTimerEnd.Invoke (this.time); // Показываем время прошедшее с начала до последнекго шага, 
													// может несколько отличаться от изначально заданного seconds, 
													// которое модно получить при обрабокет события onTimerStart.
			}
		}

		if (this.onTimerBeep != null) {
			this.onTimerBeep.Invoke (this.time);	// Показываем время прошедшее с начала до текущего шага
		}
	}
}
