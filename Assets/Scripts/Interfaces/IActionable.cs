using UnityEngine;
using System.Collections;

// ЗАМЕЧАНИЕ: Если объект ICatchable реализует собственое перемещение, то не стоит двигать его напрямую!!!
// например с помощью transform.position. Поскольку это может помешать работе заложенной в нем логике
// вместо этого передавать значение в метод SetAction, который должен вызываться в самом начале.
// ТАК НЕ НАДО!!! (поскольку МОЖЕТ сбить действие заложенного Action):
//newFish.transform.position = new Vector3 (
//	choosenSpawnPoint.position.x + Random.Range(-2.5f, 2.5f), 
//	choosenSpawnPoint.position.y + Random.Range(-1.5f, 1.5f), 
//	choosenSpawnPoint.position.z
//);

//////////////////////////////////////////////////////////////////////////

public interface IActionable {
	string DefaultAction {
		get;
	}

	void SetAction (string actionName, object data = null);
	void ChangeAction (object data);
	void StopAction ();
	void ResumeAction();

	void ChangeSpeed (float ratio = 1.0f);
}
