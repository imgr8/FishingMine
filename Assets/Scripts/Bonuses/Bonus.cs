using UnityEngine;
using System;
using System.Collections;

public interface IBonus {
	void Use(IFisher fisher, ISea sea, IFishing fishing);	 
	event Action<IBonus> OnUsed;
}
