using UnityEngine;
using System;
using System.Collections;

// По сути представляет собой рыбалку. С ее набором правил. То есть каждые отдельные объект Fishing может
// реализовывать свою логику, набор правил, условия успеха, организацию уровней, порядок генерации и т.п. 
// Заменяя объект Fishing другим можем получить иные условия.
// Иными словами Fishing - это менеджер игры.

// По скольку Fishing это в первую очередь логика, то она должна быть отделена от визуализации,
// поэтому за отображение информации должен отвечать иной объект Visualizer, который также в зависимости от 
// потребностей может заменяться другим.

public interface IFishing {
	ushort NumLevel {
		get;
		set;
	}


    void ExtraTime(float time);
    void BuyPower();
    void NextLevel();

	event Action<object> OnStateUpdate;	// уведомление о произошедших изменениях, в частности может быть использовано Visualizer'ом, чтобы обновить представление информации
	event Action<float> OnEarnedUpdate;
	event Action<ushort> OnLevelUpdate;
	event Action<float> OnChangeLevelTime;
    event Action<float> OnRequiredUpdate;
    event Action<int> OnPowerCostUpdate;
}
