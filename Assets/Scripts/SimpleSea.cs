using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Каждое море может обладать своими уникальными параметрами
// генерироваться компьютером
// или же вручную (например, когда функция MakeLive загружает xml файл и расставляет объекты).
// Если же это генерация, то MakeLive - генерирует объекты

public class SimpleSea : MonoBehaviour, ISea {

	public float width = 10.0f;	

	public float Width {
		get {
			return this.width;
		}
	}

	public float depth = 6.0f;

	public float Depth {
		get {
			return this.depth;
		}
	}

	Vector3 center;

	public Vector3 Center {
		get {
			return this.center;
		}
	}

	public float flowHorizontal = 0.0f;

	public float FlowHorizontal {
		get {
			return this.flowHorizontal;	
		}
	}

	public float flowVertical = 0.0f;

	public float FlowVertical {
		get { 
			return this.flowVertical;
		}
	}

	public int totalFishAmount = 10;

	int numOfFishes = 0;

	public GameObject [] fishes;

	public int totalStarAmount = 10;

	int numOfStars = 0;

	public GameObject [] stars;

	public int totalSurpriseAmount = 10;

	int numOfSurprise = 0;

	public GameObject [] surprise;

	public void MakeLive(int param = 0, object data = null) {
		this.FishGen ();
		this.StarGen ();
		this.SurpriseGen ();
	}
		
	List<ICatchable> createdCatchableObjects = new List<ICatchable>();

	void FishGen() {
		for (int i = 0; i < this.totalFishAmount; i++) {
			GameObject newFish = GameObject.Instantiate (this.fishes [Random.Range (0, this.numOfFishes)]);

			newFish.transform.position = new Vector3 (
				Random.Range (this.center.x - this.width / 2, this.center.x + this.width / 2), 
				Random.Range (this.center.y - this.depth / 2, this.center.y + this.depth / 2), 
				newFish.transform.position.z
			);

			ICatchable newCatchable = newFish.GetComponent<ICatchable> ();

			newCatchable.Sea = this;
			newCatchable.SetAction ("SimpleFishBehaviour");

			this.createdCatchableObjects.Add (newCatchable);

			newCatchable.OnUsed += (ICatchable obj) => {
				this.createdCatchableObjects.Remove(obj);
				GameObject.Destroy(obj.GameObject);
			};

			newFish.SetActive (true);
		}

	}

	void StarGen() {
		for (int i = 0; i < this.totalStarAmount; i++) {
			GameObject newStar = GameObject.Instantiate (this.stars [Random.Range (0, this.numOfStars)]);

			newStar.transform.position = new Vector3 (
				Random.Range (this.center.x - this.width / 2, this.center.x + this.width / 2), 
				Random.Range (this.center.y - this.depth / 4, this.center.y - this.depth / 2), 
				newStar.transform.position.z
			);

			ICatchable newCatchable = newStar.GetComponent<ICatchable> (); 
			newCatchable.Sea = this;
			newCatchable.SetAction ("SimpleStarBehaviour");

			this.createdCatchableObjects.Add (newCatchable);

			newCatchable.OnUsed += (ICatchable obj) => {
				this.createdCatchableObjects.Remove(obj);
				GameObject.Destroy(obj.GameObject);
			};

			newStar.SetActive (true);
		}

	}

	void SurpriseGen() {
		for (int i = 0; i < this.totalSurpriseAmount; i++) {
			GameObject newSurprise = GameObject.Instantiate (this.surprise [Random.Range (0, this.numOfSurprise)]);

			newSurprise.transform.position = new Vector3 (
				Random.Range (this.center.x - this.width / 2, this.center.x + this.width / 2), 
				Random.Range (this.center.y - this.depth / 4, this.center.y - this.depth / 2), 
				newSurprise.transform.position.z
			);

			ICatchable newCatchable = newSurprise.GetComponent<ICatchable> (); 
			newCatchable.Sea = this;
			newCatchable.SetAction ("SimpleSurpriseBottleBehaviour");

			this.createdCatchableObjects.Add (newCatchable);

			newCatchable.OnUsed += (ICatchable obj) => {
				this.createdCatchableObjects.Remove(obj);
				GameObject.Destroy(obj.GameObject);
			};

			newSurprise.SetActive (true);
		}

	}

	public void Clear() {
		foreach (ICatchable catchable in this.createdCatchableObjects) {
			GameObject.Destroy(catchable.GameObject);
		}

		this.createdCatchableObjects.Clear ();
	}

	public void DestroyObject (ICatchable catchableObject) {
		if (this.createdCatchableObjects.Contains (catchableObject)) {
			this.createdCatchableObjects.Remove (catchableObject);
			GameObject.Destroy (catchableObject.GameObject);
		}
	}

	// Use this for initialization
	void Awake () {
		this.center = this.transform.position;

		if (this.width == 0 || this.depth == 0) {
			Collider2D seaSizeCollider = this.GetComponent<Collider2D> ();

			if (seaSizeCollider == null) {
				throw new UnityException ("Unable to find out sea size");
			} 

			this.width = seaSizeCollider.bounds.size.x;
			this.depth = seaSizeCollider.bounds.size.y;

			this.center = seaSizeCollider.bounds.center;
		}

		this.numOfFishes = this.fishes.Length;
		this.numOfStars = this.stars.Length;
		this.numOfSurprise = this.surprise.Length;
	}

	void Start() {
		
	}

}
