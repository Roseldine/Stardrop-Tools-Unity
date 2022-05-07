using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools.Tween;

public class CubeTween : MonoBehaviour
{
    public Tween.LoopType loop;
    public Tween.EaseCurve ease;
    [Space]
    public Vector3 startPos;
    public Vector3 targetPos;
    [Space]
    public Vector3 intensity;
    [Space]
    public Vector3 startRot;
    public Vector3 targetRot;
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
        if (Input.anyKeyDown)
        {
            startPos = transform.position;
            targetPos = Random.insideUnitSphere * 3;

            startRot = transform.eulerAngles;
            targetRot = new Vector3(GetRandom360(), GetRandom360(), GetRandom360());

            var curve = Tween.GetEaseCurve(ease);

            //Tween.RotationEuler(transform, startRot, targetRot, duration, delay, false, Tween.EaseInBack, loop, -1, tweenEvent);
            //Tween.Vector3(startPos, targetPos, duration, delay, false, Tween.EaseInBounce, loop, -1, tweenEvent);
            //Tween.ShakePosition(transform, intensity, duration, delay, false, curve, loop);
            Tween.Position(transform, targetPos, duration, delay, false, curve, loop);
        }
    }

    float GetRandom360() => Random.Range(-360, 360);

    void UpdatePosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
