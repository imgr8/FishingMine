using UnityEngine;
using System.Collections;

// Этот объект может выбирать рыбалку. Фактически мы можем менять игры на лету

public class MainFishing : MonoBehaviour {
	public GameObject fishingGameObject;
   
    IFishing fishing;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < this.transform.childCount; i++) {
			this.transform.GetChild (i).gameObject.SetActive (false);
		}

		if (this.fishingGameObject.GetComponent<IFishing>() == null) {
			throw new UnityException ("Fishing are not initialized");
		}

		this.fishingGameObject.SetActive (true);
        fishing = fishingGameObject.GetComponent<IFishing>();
	}

    public void BuyPower()
    {
        fishing.BuyPower();
    }
    public void BuyHook()
    {
        fishing.BuyHook();
    }
    public void Play()
    {
        fishing.NextLevel();
    }

}
