using UnityEngine;
using System.Collections;
using System;

public enum DirectionHookMove { nowhere, forward, back };	// nowhere - если рыбак просто в состоянии поиска

public interface ISpinning {
	void LookingFor ();
	void TryCatch ();
	void PullStaff();
	void Nothing();

	void SetOwner (IFisher fisher);

	DirectionHookMove DirectionHookMove {
		get;
	}

	event Action OnStartTryCatch;	// Событие вызывается, когда стартуется попытка поймать
	event Action<ICatchable> OnEndTryCatch;		// Событие вызывается, когда заканчивается попытка поймать (крючок возвращается)
	event Action<ICatchable> OnCatchStaff;		// Событие вызывается когда, что-то поймано
}
