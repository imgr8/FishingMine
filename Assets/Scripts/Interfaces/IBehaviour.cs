public interface IBehaviour {
	//void Init (Transform obj, float speed);
	void Action ();
	void Stop();
	void Resume();

	void Change (object data);
}
