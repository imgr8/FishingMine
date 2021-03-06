﻿using UnityEngine;
using System;
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

	void MakeLive(int? param = null, object data = null);	// Каждое море (локация) может по-разному генерироваться, как автоматически, так и вручную. Возможна передача неких параметров
															// Например, param может означать номер уровня.

	void AddObject (ICatchable catchableObject);		// Добавляет выбранный объект в море
	void AddObject (IUncatchable uncatchableObject);		// Добавляет выбранный объект в море
	void AddObject (GameObject gameObject);				// Добавляет выбранный объект в море. Море должно проверить какой это объект, и если неподходящий - удалить

	void DestroyObject (ICatchable catchableObject);	// Уничтожает выбранный объект в море
	void DestroyObject (IUncatchable uncatchableObject);	// Уничтожает выбранный объект в море

	event Action<ICatchable> OnDestroyCatchableObject;	// Событие происходит, когда уничтожается ICatchable объект
	event Action<IUncatchable> OnDestroyUncatchableObject;	// Событие происходит, когда уничтожается Iuncatchable объект

	void OnEveryCatchableObject (Action<ICatchable> foo);
	void OnEveryUncatchableObject (Action<IUncatchable> foo);

	void Clear();	// Очистить море от всех объектов			
	void ClearAllCatchable();	// Очистить море от всех Catchable объектов	
	void ClearAllUncatchable();	// Очистить море от всех Uncatchable объектов	

	int CountOfCatchableObjects {
		get;
	}

	int CountOfUncatchableObjects {
		get;
	}

	int CountOfAllObjects {
		get;
	}
		
	HashSet<ICatchable> GetAllCatchableObjectInSea();
	HashSet<IUncatchable> GetAllUncatchableObjectInSea();
}
