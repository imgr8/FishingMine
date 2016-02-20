using UnityEngine;
using System.Collections;

public static class BehaviourCreator {
	public static IBehaviour CreateBehaviour(string behaviourName, ICatchable catchable, object data = null) {
		IBehaviour behaviour;

		switch (behaviourName) {
		case "SimpleFishBehaviour":
			behaviour = new SimpleFishBehaviour (catchable);
			return behaviour;
			break;
		case "SimpleStarBehaviour":
			behaviour = new SimpleStarBehaviour (catchable);
			return behaviour;
			break;
		default:
			return null;
		}
	}
}