
namespace StardropTools.Tween
{
    public class TweenComponentImagePixelPerUnitMultiplier : TweenComponentImage
    {
        [UnityEngine.Space]
        public float start;
        public float end;

        public CoreEvent<float> OnTween = new CoreEvent<float>();

        public override void InitializeTween()
        {
            if (data.hasStart)
                tween = Tween.ImagePixelsPerUnitMultiplier(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.ImagePixelsPerUnitMultiplier(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }
    }
}