using UnityEngine;

public interface ISaveFromEditor {
	string Path {
		get;
	}

	GameObject GameObject {
		get;
	}
}
