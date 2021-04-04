using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// タイマーを使う側
/// </summary>
public class TimerDisplayComponent : MonoBehaviour
{
    [SerializeField]
    private TimerComponent timerComponent;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        timerComponent.CurrentTime
            .SubscribeToText(text);
    }

    private void Update()
    {
        
    }
}
