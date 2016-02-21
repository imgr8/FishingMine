using UnityEngine;
using System.Collections;

public static class BehaviourCreator {
	public static IBehaviour CreateBehaviour(string behaviourName, ICatchable catchable, object data = null) {
		IBehaviour behaviour;

		switch (behaviourName) {
		case "SimpleFishBehaviour":
			behaviour = new SimpleFishBehaviour (catchable);
			return behaviour;
		case "SimpleStarBehaviour":
			behaviour = new SimpleStarBehaviour (catchable);
			return behaviour;
		case "SimpleSurpriseBottleBehaviour":
			behaviour = new SimpleSurpriseBottleBehaviour (catchable);
			return behaviour;
		default:
			return null;
		}
	}
}