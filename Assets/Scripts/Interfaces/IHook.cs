using UnityEngine;
using System.Collections;
using System;

public interface IHook {

	void SetOwner(ISpinning spinning);
	event Action<ICatchable> OnCatchStaff;

}
