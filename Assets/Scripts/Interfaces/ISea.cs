using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Сущность море
// по сути представляет из себя отдельную локацию
// и каждое море может содержать рыбу и предметы, характерные только для него
// В рамках поставленной задачи, отвечает за содержание всех предметов в нем.
// А также уведомляет о себе других участников, чтобы те могли знать
// о его параметрах (ширина, глубина, сила течения и т.п.).

public interface ISea {
	float Width {
		get;
	}

	float Depth {
		get;
	}

	Vector3 Center {
		get;
	}

	float FlowHorizontal {
		get;
	}

	float FlowVertical {
		get;
	}

	void MakeLive(int param = 0, object data = null);	// Каждое море (локация) может по-разному генерироваться, как автоматически, так и вручную. Возможна передача неких параметров
														// Например, param может означать номер уровня.

	void DestroyObject (ICatchable catchableObject);	// Уничтожает выбранный объект в море

	void Clear();	// Очистить мореот всех объектов			

	HashSet<ICatchable> GetAllCatchableObjectInSea();
}
