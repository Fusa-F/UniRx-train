using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// Model.設定を管理するコンポーネント
/// </summary>
public class ConfigComponent : SingletonMonoBehaviour<ConfigComponent>
{
    /// <summary>
    /// Sliderの値
    /// </summary>
    public ReactiveProperty<int> SliderReactiveProperty = new IntReactiveProperty(0);

    // 以下他のReactivePropertyの定義


    // sliderのMVP動作確認
    // private void Start()
    // {
    //     this.UpdateAsObservable()
    //         .Subscribe(_ => print(SliderReactiveProperty.Value));
    // }
}
