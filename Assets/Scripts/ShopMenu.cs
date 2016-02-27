using UnityEngine;
using System.Collections;

public class ShopMenu : MonoBehaviour {

   
   
    int powerPrice = 100;

	void Start () {
        
	}
	
	
	void Update () {
	
	}
    public void BuyPower(IFisher fisher)
    {
        fisher.Power++;
    }
}
