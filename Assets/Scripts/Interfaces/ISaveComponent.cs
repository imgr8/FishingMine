using UnityEngine;
using System.Collections;

public interface ISaveComponent {
	string Save();
	void Load(string data);
}
