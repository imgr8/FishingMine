using UnityEngine;
using System.Collections;
using System;

public interface ISpinning {
	void LookingFor ();
	void TryCatch ();
	void PullStaff();

	void SetOwner (IFisher fisher);

	event Action OnStartTryCatch;	// Событие вызывается, когда стартуется попытка поймать
	event Action<ICatchable> OnEndTryCatch;		// Событие вызывается, когда заканчивается попытка поймать (крючок возвращается)
	event Action<ICatchable> OnCatchStaff;		// Событие вызывается когда, что-то поймано
}
