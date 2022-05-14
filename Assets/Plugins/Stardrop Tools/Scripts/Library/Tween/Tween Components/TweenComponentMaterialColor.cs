﻿
namespace StardropTools.Tween
{
    public class TweenComponentMaterialColor : TweenComponentMaterial
    {
        [UnityEngine.Space]
        public UnityEngine.Color start = UnityEngine.Color.white;
        public UnityEngine.Color end = UnityEngine.Color.white;

        public override void InitializeTween()
        {
            if (data.hasStart)
                tween = Tween.MaterialColor(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.MaterialColor(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }
    }
}