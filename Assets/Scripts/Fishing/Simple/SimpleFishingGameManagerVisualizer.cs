using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleFishingGameManagerVisualizer : MonoBehaviour {
	public SimpleFishing fishing;
	public Text textForEarnedMoney;
	public Text textForLevelNumber;
	public Text textForTimeRemainded;
    public Text textForRequiredMoney;

	// Use this for initialization
	void Start () {

		if (this.fishing == null) {
			throw new UnityException ("Fishing have not initialized");
		}

		this.fishing.OnEarnedUpdate += (float money) => {
			this.textForEarnedMoney.text = money.ToString();
		};

        this.fishing.OnRequiredUpdate += (float money) =>
        {
            this.textForRequiredMoney.text = money.ToString();
        };

		this.fishing.OnLevelUpdate += (ushort levelNum) => {
			this.textForLevelNumber.text = levelNum.ToString();
		};

		this.fishing.OnChangeLevelTime += (float time) => {
			this.textForTimeRemainded.text = time.ToString();
		};

	}

}
