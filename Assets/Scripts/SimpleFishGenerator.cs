// OBSOLETE!

using UnityEngine;
using System.Collections;

// Это очень простой генератор, только для тестов. Для реальной реализации необходимо внесение изменений!
public class SimpleFishGenerator : MonoBehaviour {

	public Transform [] spawnPonts;
	public GameObject [] fishes;

	int numOfSpawnPoints = 0;
	int numOfFishes = 0;

	// Use this for initialization
	void Start () {
		this.numOfSpawnPoints = this.spawnPonts.Length;
		this.numOfFishes = this.fishes.Length;

		InvokeRepeating ("FishGen", 0, 2);
	}

	ushort commonFishCount = 0;

	void FishGen() {
		if (this.commonFishCount++ == 10) {
			CancelInvoke ("FishGen");
			return;
		}

		Transform choosenSpawnPoint = this.spawnPonts[Random.Range(0, this.numOfSpawnPoints)];
		GameObject newFish = GameObject.Instantiate (this.fishes[Random.Range(0, this.numOfFishes)]);

		newFish.GetComponent<Fish> ().SetAction (
			"SimpleFishBehaviour"
		);

		newFish.SetActive (true);

	}
		
}
