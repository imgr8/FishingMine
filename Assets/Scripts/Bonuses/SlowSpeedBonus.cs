using UnityEngine;
using System;

public class SlowSpeedBonus : Bonus {

    ISea sea = null;
    float changeSpeedCoefficient = 0.75f;

    public SlowSpeedBonus(ISea sea, float changeSpeedCoefficient = 0.75f)
    {
        this.sea = sea;
        this.changeSpeedCoefficient = changeSpeedCoefficient;
    }

    public override void Use(IFisher fisher)
    {
        this.sea.OnEveryCatchableObject((obj) =>
        {
            obj.ChangeSpeed(this.changeSpeedCoefficient);
        });
        Debug.Log("Slow speed");
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
