using UnityEngine;
using System.Collections;

public static class BehaviourCreator {
	public static IBehaviour CreateBehaviour(string behaviourName, ICatchable catchable, object data = null) {
		switch (behaviourName) {
		case "SimpleFishBehaviour":
			IBehaviour behaviour = new SimpleFishBehaviour (catchable);
			return behaviour;
			break;
		default:
			return null;
			break;
		}
	}
}