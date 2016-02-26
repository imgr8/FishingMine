using UnityEngine;
using System;

public class FortuneBonus : Bonus
{

    float priceKoef = 1.5f;

    public FortuneBonus(ISea sea, float koef)
    {
        this.Sea = sea;
        this.priceKoef = koef;
    }
    public override void Use(IFisher fisher)
    {
        Sea.OnEveryCatchableObject((obj) =>
            {

                obj.ChangePrice(priceKoef);
            });
        Debug.Log("Fortune");

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
