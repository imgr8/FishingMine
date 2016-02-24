using UnityEngine;
using System;
using System.Collections;

public interface ILevel {
	void Init (IFishing fishing, ISea sea, IFisher fisher, ushort levelNum = 0, Action method = null);
	bool Passed ();	// метод должен сигализировать о том, был пройден уровень или нет
	void Unload();	// После того как уровень не нужен, вызывающий код должен вызвать метод Unload, чтобы дать объекту Level возможность очистить ресурсы и отписаться от событий
}
