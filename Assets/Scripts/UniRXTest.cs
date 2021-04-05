using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public class UniRXTest : MonoBehaviour
{
    private Subject<string> mSubject = new Subject<string>();

    public IObservable<string> mObservable {
        get { return mSubject; }
    }

    void Start()
    {
        // SampleSubject();
        // SampleIObserver();
        // SampleOperator();
        // SampleDelay();
        // SampleButtonOnClick();
        // SampleCheckProperty();

        SampleTriggerEvent();
    }

    void Update()
    {
        
    }

    /// <summary> Subjectとは </summary>
    private void SampleSubject()
    {
        var sub = new Subject<string>();
        sub.Subscribe(text => print(text));
        sub.OnNext("テキストを表示");
    }

    /// <summary> 処理の登録部分(Subscribeメソッド)のみを公開 </summary>
    private void SampleIObservable()
    {
        mObservable.Subscribe(text => print(text));
    }

    /// <summary> OnError,OnCompleteの使い方(いずれも呼び出すと以降のOn~~は呼び出されない) </summary>
    private void SampleIObserver()
    {
        var sub = new Subject<string>();

        sub.Subscribe(
            /*onNext: <-省略可*/ text => print("テキスト表示： " + text),
            onError: error => print("エラー発生： " + error),
            onCompleted: () => print("完了")
        );

        sub.OnNext("テストテストテスト");
        sub.OnError(new Exception("例外エラーテスト"));
        sub.OnCompleted(); // 上でOnErrorを呼んでいるので通らない
    }

    /// <summary> オペレータの使い方(他にも無数に種類有) </summary>
    private void SampleOperator()
    {
        var sub = new Subject<string>();

        sub.Where(text => text.Length < 10)
            .Select(text => text + text[0])
            .Subscribe(
                onNext: text => print(text),
                onError: error => print("エラー発生： " + error)
            );

        sub.OnNext("テキストテキストテキストテキストテキスト"); // 10文字以上なのでWhereでひっかかる(Select以降は行われない)
        sub.OnNext("テス"); // 表示：テステ

        sub.OnNext(""); // 1文字目がないのでSelectのtext[0]でエラー(IndexOutOfRangeException)
        sub.OnNext("テキスト"); // 上がErrorなので呼ばれない
    }

    /// <summary> 遅延処理 </summary>
    private void SampleDelay()
    {
        Observable.Timer(TimeSpan.FromSeconds(2f))
            .Subscribe(_=> print("2秒遅延"));
    }

    /// <summary> UIボタン処理 </summary>
    private void SampleButtonOnClick()
    {
        GameObject btn = GameObject.Find("Canvas/Button01");
        btn.GetComponent<Button>()
            .OnClickAsObservable()
            .Buffer(2)
            .Subscribe(_=> print("偶数"));
    }

    /// <summary> 値変化の監視 </summary>
    private ReactiveProperty<int> reactiveProperty = new ReactiveProperty<int>(0);
    public IObservable<int> ReactiveObservable {
        get { return reactiveProperty; }
    }
    private void SampleCheckProperty()
    {
        ReactiveObservable.Subscribe(count => print(count));

        SetValue(1);
        SetValue(2);
        SetValue(2);
        SetValue(1);
        SetValue(1);
    }
    private void SetValue(int value)
    {
        reactiveProperty.Value = value;
    }

    /// <summary> TriggerイベントのObservable化 </summary>
    private void SampleTriggerEvent()
    {
        bool mOnWall = false;
        ReactiveProperty<bool> OnWall = new BoolReactiveProperty(false);
        var sub = new Subject<Unit>();

        this.OnTriggerEnter2DAsObservable()
            .Where(other => other.gameObject.tag == "Wall")
            .Subscribe(_ => mOnWall = true);
        
        this.OnTriggerExit2DAsObservable()
            .Where(other => other.gameObject.tag == "Wall")
            .Subscribe(_ => mOnWall = false);

        OnWall.Subscribe(_ => print("壁に触れているか： " + OnWall.Value));
        
    }
}
