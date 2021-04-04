using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// スライダーのPresenter
/// </summary>
public class SliderPresenter : MonoBehaviour
{
    [SerializeField] private TimerComponent timerComponent;
    private void Start()
    {
        var slider = GetComponent<Slider>();
        var config = ConfigComponent.Instance;

        // Model -> View
        config.SliderReactiveProperty
            .Subscribe(x => slider.value = x);

        // View -> Model
        slider.OnValueChangedAsObservable()
            .DistinctUntilChanged()
            .Subscribe(x => config.SliderReactiveProperty.Value = (int)x);

        // 以下実験
        timerComponent.CurrentTime
            .Where(count => count == 0)
            .Subscribe(_ => slider.value = 0);
    }
}
