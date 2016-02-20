using System;

public interface ITimer {

	void StartTimer(float seconds, float msgTime);
	void Pause();
	void Resume();

	event Action<float> OnTimerStart;
	event Action<float> OnTimerEnd;
	event Action<float> OnTimerBeep;

}
