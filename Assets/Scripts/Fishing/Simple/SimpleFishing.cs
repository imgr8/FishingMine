using UnityEngine;
using System;
using System.Collections;

public class SimpleFishing : MonoBehaviour, IFishing
{
    public GameObject fisherGameObject;
    IFisher fisher;

    public GameObject seaGameObject;
    ISea sea;

    public GameObject timerGameObject;
    ITimer timer;
    float earnedInCurrentLevel = 0; // заработанное в текущем уровне, чтоб в случае поражения, начать заново текущий уровень аннулируя все заработанное этом уровне
    float earned = 0;	// Заработанное
    float Earned
    {
        get
        {
            return this.earned;
        }
    }

    float required = 0; //необходимые
    float Required
    {
        get
        {
            return this.required;
        }
    }

    ushort numLevel = 0;

    ushort NumLevel
    {
        set
        {
            this.numLevel = value;

            if (this.onLevelUpdate != null)
            {
                this.onLevelUpdate.Invoke(value);
            }
           
        }

        get
        {
            return this.numLevel;
        }
    }

    float levelTimeAmount = 60.0f;

    float levelTimeRemainded = 0;

    // Use this for initialization
    void Start()
    {
        this.sea = this.seaGameObject.GetComponent<ISea>();

        if (this.sea == null)
        {
            throw new UnityException("Sea are not initialized");
        }

        this.fisher = this.fisherGameObject.GetComponent<IFisher>();

        this.fisher.ClearState();

        if (this.fisher == null)
        {
            throw new UnityException("Fisher are not initialized");
        }

        this.fisher.Boat.OnPutStaff += (ICatchable obj) =>
        {
            this.earned += obj.Price;
            this.earnedInCurrentLevel += obj.Price;

            if (this.onEarnedUpdate != null)
            {
                this.onEarnedUpdate.Invoke(this.earned);
            }

        };

        this.timer = this.timerGameObject.GetComponent<ITimer>();

        if (this.timer == null)
        {
            throw new UnityException("Timer are not initialized");
        }

        this.timer.OnTimerStart += (float time) =>
        {
            this.levelTimeRemainded = time;

            if (this.onChangeLevelTime != null)
            {
                this.onChangeLevelTime.Invoke(time);
            }
        };

        this.timer.OnTimerEnd += (float time) =>
        {
            this.levelTimeRemainded = 0;

            if (this.onChangeLevelTime != null)
            {
                this.onChangeLevelTime.Invoke(time);
            }

            this.FinishLevel();
        };

        this.timer.OnTimerBeep += (float time) =>
        {
            this.levelTimeRemainded = this.levelTimeAmount - time;

            if (this.onChangeLevelTime != null)
            {
                this.onChangeLevelTime.Invoke(this.levelTimeRemainded);
            }
        };

        this.IfConditionPassedStartNextLevel();

    }

    void IfConditionPassedStartNextLevel()
    {
        if (this.NumLevel == 0)
        {
            this.NextLevel();
        }
        else if (this.CheckConditions())
        {
            this.NextLevel();
        }
        else
        {
            //this.GameOver();
            numLevel--;
            earned -= earnedInCurrentLevel;
            this.onEarnedUpdate.Invoke(this.earned);
            NextLevel();
        }
    }

    void FinishLevel()
    {
        this.fisher.StopCatchFish();
        this.fisher.ClearState();
        this.sea.Clear();
        this.IfConditionPassedStartNextLevel();
    }

    bool CheckConditions()
    {
        if (earned >= required)
            return true;
        else return false;
    }

    void NextLevel()
    {
        earnedInCurrentLevel = 0;
        this.NumLevel++;

        this.sea.MakeLive(NumLevel);

        this.fisher.Boat.Sea = this.sea;

        this.timer.StartTimer(this.levelTimeAmount, 1.0f);

        this.fisher.StartCatchFish();

        this.required = 10 * NumLevel * NumLevel + 125 * NumLevel;

        if (this.onRequiredUpdate != null)
            this.onRequiredUpdate.Invoke(this.required);
    }

    void GameOver()
    {
        this.numLevel = 0;
        this.earned = 0;
    }

    event Action<float> onChangeLevelTime;

    public event Action<float> OnChangeLevelTime
    {
        add
        {
            this.onChangeLevelTime += value;
        }

        remove
        {
            this.onChangeLevelTime -= value;
        }
    }


    event Action<ushort> onLevelUpdate;

    public event Action<ushort> OnLevelUpdate
    {
        add
        {
            this.onLevelUpdate += value;
        }

        remove
        {
            this.onLevelUpdate -= value;
        }
    }

    event Action<float> onEarnedUpdate;

    public event Action<float> OnEarnedUpdate
    {
        add
        {
            this.onEarnedUpdate += value;
        }

        remove
        {
            this.onEarnedUpdate -= value;
        }
    }

    event Action<float> onRequiredUpdate;

    public event Action<float> OnRequiredUpdate
    {
        add
        {
            this.onRequiredUpdate += value;
        }

        remove
        {
            this.onRequiredUpdate -= value;
        }
    }

    event Action<object> onStateUpdate;

    public event Action<object> OnStateUpdate
    {
        add
        {
            this.onStateUpdate += value;
        }

        remove
        {
            this.onStateUpdate -= value;
        }
    }

}
