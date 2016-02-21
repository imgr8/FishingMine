using UnityEngine;
using System;
using System.Collections;

public interface ICatchable {

	float Weight {
		get;
	}

	float Price {
		get;
	}

	GameObject GameObject {
		get;
	}
		
	ISea Sea {	// Каждый предмет в море должен быть осведомлен о море, в котором он находится
		get;
		set;
	}

	void Destroy();	// Приводит к удалению объекта (объект сам должен оповестить своего "создателя", например море, о своем удалении)

	void WhenCatched ();		 // Должен, вызываться когда объект был пойман (рыба может останавливать движение, мина - взрываться и т.п.)
	void Use(IFisher fisher);	 // Поскольку Catchable инетрактивный объект, то после того, как объект был использован (помещен в лодку, использован как бонус и т.п.) должна вызываться функция
								 // по сути показывающая, что объект выполнил свою задачу, и в соответсвии с этим может выполнить необходимый в этом случае ему алгоритм.
	event Action<ICatchable> OnUsed;

	// ПОМЕТКА!!! Наверное лучше в отдельный интерфейс
	// ЗАМЕЧАНИЕ: Если объект ICatchable реализует собственое перемещение, то не стоит двигать его напрямую!!!
	// например с помощью transform.position. Поскольку это может помешать работе заложенной в нем логике
	// вместо этого передавать значение в метод SetAction, который должен вызываться в самом начале.
	// ТАК НЕ НАДО!!! (поскольку МОЖЕТ сбить действие заложенного Action):
	//newFish.transform.position = new Vector3 (
	//	choosenSpawnPoint.position.x + Random.Range(-2.5f, 2.5f), 
	//	choosenSpawnPoint.position.y + Random.Range(-1.5f, 1.5f), 
	//	choosenSpawnPoint.position.z
	//);
	void SetAction (string actionName, object data = null);
	void ChangeAction (object data);
	void StopAction ();
	void ResumeAction();

	//////////////////////////////////////////////////////////////////////////
}
