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
        // TimeSpanでプロパティ更新する方法 ->TimeDisplayComponentでUIText表示
        // Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1)) でも可
        // Observable.Interval(TimeSpan.FromSeconds(1))
        //     .TakeWhile(_ => _timerReactiveProperty.Value > 0)
        //     .Subscribe(
        //         _ => _timerReactiveProperty.Value--,
        //         () => print("END")
        //     )
        //     .AddTo(gameObject); // ゲームオブジェクト破棄時に停止させる

        // Coroutine内で発行する方法 コルーチンの終了待ちができるので便利
        // Observable.FromCoroutine<int>(observer => CountDownCoroutine(observer, 5))
        //     .Subscribe(
        //         count => print(count),
        //         () => print("end")
        //     )
        //     .AddTo(gameObject);
    }

    private IEnumerator CountDownCoroutine(IObserver<int> observer, int startTime)
    {
        var currentTime = startTime;
        while(currentTime > 0)
        {
            if(/*条件式=*/true)
            {
                observer.OnNext(currentTime--);
            }
            yield return new WaitForSeconds(1f);
        }
        observer.OnNext(0);
        observer.OnCompleted();
    }
}
