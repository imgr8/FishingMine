using UnityEngine;
using System.Collections;

public interface IMove {
	void Init (ISea sea);

	void Move();
	void StopMove();
	void ResumeMove();

	float Speed {
		set;
	}

	float HorizontalSpeed {
		get;
		set;
	}

	float VerticalSpeed {
		get;
		set;
	}
}

public interface IMovable {

	IMove MoveBehaviour {
		get;
		set;
	}

}
