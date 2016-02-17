using UnityEngine;
using System.Collections;

public class FishingHook : MonoBehaviour {

	public GameObject fishingHookCenter;
	public float length = 0.0f;
	public float angleSpeed = 1.0f;
	public float maxAngleDeviation = 60.0f;

	const float lengthByDefault = 1.0f;

	// Use this for initialization
	void Start () {
		if (Mathf.Approximately (this.length, 0.0f)) {
			this.length = Vector3.Distance(this.transform.position, this.fishingHookCenter.transform.position);
		} else {
			if (this.length < 0) {
				this.length = FishingHook.lengthByDefault;
			}

			this.transform.position = new Vector3 (
				this.fishingHookCenter.transform.position.x, 
				this.fishingHookCenter.transform.position.y - this.length,	// минус, потому что крючок направлен вниз
				this.fishingHookCenter.transform.position.z
			);
		}
	}

	enum Direction {clockwise = 1, anticlockwise = -1};

	Direction directionOfMovemnet = Direction.clockwise;

	float angle = 0.0f;

	// Update is called once per frame
	void Update () {

		if (angle > this.maxAngleDeviation && this.directionOfMovemnet == Direction.clockwise) {
			this.directionOfMovemnet = Direction.anticlockwise;
		} else if (angle <- this.maxAngleDeviation && this.directionOfMovemnet == Direction.anticlockwise) {
			this.directionOfMovemnet = Direction.clockwise;
		}

		if (this.directionOfMovemnet == Direction.clockwise) {
			this.angle += Time.deltaTime * this.angleSpeed;
		} else {
			this.angle -= Time.deltaTime * this.angleSpeed;
		}

		float newXCoord = this.length * Mathf.Sin (Mathf.Deg2Rad * this.angle);
		float newYCoord = -Mathf.Sqrt(this.length * this.length - newXCoord * newXCoord);

		this.transform.position = new Vector3 (
			this.fishingHookCenter.transform.position.x + newXCoord, 
			this.fishingHookCenter.transform.position.y + newYCoord, 
			this.transform.position.z
		);
	}
}
