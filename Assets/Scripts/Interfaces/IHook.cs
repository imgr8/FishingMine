using UnityEngine;
using System.Collections;
using System;

public interface IHook {

	event Action<ICatchable> OnCatchStaff;

}
