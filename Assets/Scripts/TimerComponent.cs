using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// タイマー側
/// </summary>
public class TimerComponent : MonoBehaviour
{
    private readonly ReactiveProperty<int> _timerReactiveProperty = new IntReactiveProperty(3);

    /// <summary>
    /// 現在のカウント
    /// </summary>
    public ReactiveProperty<int> CurrentTime {
        get { return _timerReactiveProperty; }
    }

    private void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .TakeWhile(_ => _timerReactiveProperty.Value > 0)
            .Subscribe(
                _ => _timerReactiveProperty.Value--,
                () => print("END")
            )
            .AddTo(gameObject); // ゲームオブジェクト破棄時に停止させる
    }
}
