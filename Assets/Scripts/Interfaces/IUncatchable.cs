using UnityEngine;
using System.Collections;

// Объекты, которые нельзя поймать, но либо с ними можно как-то взаимодействовать, либо они выполняют функцию декора

public interface IUncatchable : IActionable {
	GameObject GameObject {
		get;
	}

	ISea Sea {	// Каждый предмет в море должен быть осведомлен о море, в котором он находится
		get;
		set;
	}

	void Destroy();	// Приводит к удалению объекта (объект сам должен оповестить своего "создателя", например море, о своем удалении)
}
