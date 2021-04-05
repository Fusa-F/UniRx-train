using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// Wallオブジェクト
/// </summary>
public class WallObjectManager : MonoBehaviour
{
    private void Start()
    {
        var collider = GetComponent<BoxCollider2D>();

        this.OnMouseDownAsObservable()
            .Subscribe(
                _ => collider.enabled = collider.enabled ? false : true
            );
    }
}
