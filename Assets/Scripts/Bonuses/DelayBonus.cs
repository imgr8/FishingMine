using UnityEngine;
using System;

public class DelayBonus : Bonus {

    float timeAmount = 25;
    public DelayBonus(float timeAmount)
    {
        this.timeAmount = timeAmount;
    }
    public override void Use(IFisher fisher)
    {
        Debug.Log("Delay");

        GameObject.Find("FISHINGS").GetComponent<MainFishing>().fishingGameObject.GetComponent<IFishing>().ExtraTime(timeAmount);

        if (this.onUsed != null)
        {
            this.onUsed.Invoke(this);
        }
    }



    event Action<ICatchable> onUsed;

    public override event Action<ICatchable> OnUsed
    {
        add
        {
            this.onUsed += value;
        }

        remove
        {
            this.onUsed -= value;
        }
    }
}
