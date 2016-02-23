using UnityEngine;

public interface ISaveFromEditor {
	string Path {
		get;
	}

	GameObject GameObject {
		get;
	}

	// Если мы загружаем из файла данные, то море не будет иметь полную информацию об объекте, 
	// в данной функции можно устранить этот пробел, а также
	// при необходимости настроить нужные параметры
	void Load(ISea sea);

}
