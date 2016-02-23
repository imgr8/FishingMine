using UnityEngine;
using System.Collections;

public interface ILevel {
	bool Passed ();	// метод должен сигализировать о том, был пройден уровень или нет
}
