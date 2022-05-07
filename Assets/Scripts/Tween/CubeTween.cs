using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools.Tween;

public class CubeTween : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 targetPos;
    public Tween.LoopType loop;
    [Space]
    public float duration = 1;
    public float delay = .2f;

    private CoreEvent<Vector3> tweenEvent = new CoreEvent<Vector3>();

    private void Start()
    {
        tweenEvent.AddListener(UpdatePosition);
    }

    public void Update()
    {
        if (Input.anyKey)
        {
            startPos = transform.position;
            targetPos = Random.insideUnitSphere * 3;
            Tween.Vector3(startPos, targetPos, duration, delay, false, Tween.EaseInBounce, loop, -1, tweenEvent);
        }
    }

    void UpdatePosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
