using UnityEngine;
using System.Collections;

public class EmptyMove : MonoBehaviour, IMove {

	public void Init (ISea sea) {
	
	}

	public void Move() {
	
	}

	public void StopMove() {
	
	}

	public void ResumeMove(){

	}

	public float Speed {
		set {
		
		}
	}

	public float HorizontalSpeed {
		get {
			return 0;
		}

		set {
		
		}
	}

	public float VerticalSpeed {
		get {
			return 0;
		}

		set {
		
		}
	}
}
