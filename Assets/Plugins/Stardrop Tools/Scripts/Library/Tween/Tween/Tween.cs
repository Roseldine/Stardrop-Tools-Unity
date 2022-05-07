
using System;
using UnityEngine;

namespace StardropTools.Tween
{
    public class Tween
    {
        public enum TweenTarget { Float, Integer, Vector2, Vector3, Vector4, Color, Position, Rotation, LocalScale, Size, AnchoredPosition };
        public enum LoopType { none, loop, pingPong }
        public enum TweenState { waiting, running, complete, paused, canceled }
        public TweenCurves curves;

        #region Curves

        private static AnimationCurve easeLinear;
        private static AnimationCurve easeIn;
        private static AnimationCurve easeOut;
        private static AnimationCurve easeInOut;

        private static AnimationCurve easeInBack;
        private static AnimationCurve easeOutBack;
        private static AnimationCurve easeInOutBack;

        private static AnimationCurve easeInBounce;
        private static AnimationCurve easeOutBounce;
        private static AnimationCurve easeInOutBounce;

        private static AnimationCurve easeInElastic;
        private static AnimationCurve easeOutElastic;
        private static AnimationCurve easeInOutElastic;

        private static AnimationCurve easeWavePositive;
        private static AnimationCurve easeWaveNegative;

        private static AnimationCurve easeWaveStrongPositive;
        private static AnimationCurve easeWaveStrongNegative;

        private static AnimationCurve easeWobble;

        public static AnimationCurve Linear { get { if (easeLinear == null) easeLinear = new AnimationCurve(new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f)); return easeLinear; } }
        public static AnimationCurve EaseIn { get { if (easeIn == null) easeIn = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1f, 2.388632f, 2.388632f)); return easeIn; } }
        public static AnimationCurve EaseOut { get { if (easeOut == null) easeOut = new AnimationCurve(new Keyframe(0f, 0f, 2f, 2f), new Keyframe(1f, 1f, 0f, 0f)); return easeOut; } }
        public static AnimationCurve EaseInOut { get { if (easeInOut == null) easeInOut = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f)); return easeInOut; } }
        
        public static AnimationCurve EaseInBack { get { if (easeInBack == null) easeInBack = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.4f, -0.1f, 0.1783385f, 0.1783385f), new Keyframe(1f, 1f, 3.343508f, 3.343508f)); return easeInBack; } }
        public static AnimationCurve EaseOutBack { get { if (easeOutBack == null) easeOutBack = new AnimationCurve(new Keyframe(0f, 0f, 3.494377f, 3.494377f), new Keyframe(0.6f, 1.1f, 0.3653818f, 0.3653818f), new Keyframe(1f, 1f, 0f, 0f)); return easeOutBack; } }
        public static AnimationCurve EaseInOutBack { get { if (easeInOutBack == null) easeInOutBack = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.25f, -0.1f, 1.259989f, 1.259989f), new Keyframe(0.75f, 1.1f, 1.271497f, 1.271497f), new Keyframe(1f, 1f, 0f, 0f)); return easeInOutBack; } }
        
        public static AnimationCurve EaseInBounce { get { if (easeInBounce == null) easeInBounce = new AnimationCurve(new Keyframe(0f, 0f, 4.720257f, 4.720257f), new Keyframe(0.05f, 0.1f, 0f, 0f), new Keyframe(0.1f, 0f, -4.10869f, 5.888695f), new Keyframe(0.2f, 0.25f, -9.513498E-08f, -9.513498E-08f), new Keyframe(0.3f, 0f, -6.05509f, 6.342979f), new Keyframe(0.45f, 0.5f, 0f, 0f), new Keyframe(0.6f, 0f, -7.369156f, 5.472521f), new Keyframe(1f, 1f, 0f, 0f)); return easeInBounce; } }
        public static AnimationCurve EaseOutBounce { get { if (easeOutBounce == null) easeOutBounce = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.4f, 1f, 5.472521f, -7.369156f), new Keyframe(0.55f, 0.499887f, 0f, 0f), new Keyframe(0.7f, 1f, 6.342979f, -6.05509f), new Keyframe(0.8f, 0.7498865f, -9.513498E-08f, -9.513498E-08f), new Keyframe(0.9f, 1f, 5.888695f, -4.10869f), new Keyframe(0.95f, 0.899886f, 0f, 0f), new Keyframe(1f, 1f, 4.720257f, 4.720257f)); return easeOutBounce; } }
        public static AnimationCurve EaseInOutBounce { get { if (easeInOutBounce == null) easeInOutElastic = new AnimationCurve(new Keyframe(0f, 0f, 4.720257f, 4.720257f), new Keyframe(0.02500096f, 0.05001157f, 0f, 0f), new Keyframe(0.05000193f, 0f, -4.10869f, 5.888695f), new Keyframe(0.1000039f, 0.1250289f, -9.513498E-08f, -9.513498E-08f), new Keyframe(0.1500058f, 0f, -6.05509f, 6.342979f), new Keyframe(0.2250087f, 0.2500578f, 0f, 0f), new Keyframe(0.3000116f, 0f, -7.369156f, 5.472521f), new Keyframe(0.5000193f, 0.5001157f, 0f, 0f), new Keyframe(0.6f, 1f, 11.79358f, -5.820361f), new Keyframe(0.7f, 0.75f, 0f, 0f), new Keyframe(0.805f, 1f, 4.386343f, -5.962316f), new Keyframe(0.8608643f, 0.8708649f, 0f, 0f), new Keyframe(0.919391f, 0.9996701f, 5.448766f, -2.422245f), new Keyframe(0.9562561f, 0.9457275f, 0f, 0f), new Keyframe(1.000286f, 0.9954951f, 2.497174f, 2.497174f)); return easeInOutBounce; } }
        
        public static AnimationCurve EaseInElastic { get { if (easeInElastic == null) easeInElastic = new AnimationCurve(new Keyframe(0f, 0f, 0.4605525f, 0.4605525f), new Keyframe(0.15f, -0.05f, 0f, 0f), new Keyframe(0.3f, 0.1f, 0f, 0f), new Keyframe(0.45f, -0.2f, 0f, 0f), new Keyframe(0.6f, 0.25f, 0f, 0f), new Keyframe(0.75f, -0.35f, 0f, 0f), new Keyframe(1f, 1f, 5.4f, 5.4f)); return easeInElastic; } }
        public static AnimationCurve EaseOutElastic { get { if (easeOutElastic == null) easeOutElastic = new AnimationCurve(new Keyframe(0f, 0f, 6.899229f, 6.899229f), new Keyframe(0.25f, 1.25f, 0f, 0f), new Keyframe(0.4f, 0.8f, 0f, 0f), new Keyframe(0.55f, 1.15f, 0f, 0f), new Keyframe(0.7f, 0.9f, 0f, 0f), new Keyframe(0.85f, 1.05f, 0f, 0f), new Keyframe(1f, 1f, 0.4605525f, 0.4605525f)); return easeOutElastic; } }
        public static AnimationCurve EaseInOutElastic { get { if (easeInOutElastic == null) easeInOutElastic = new AnimationCurve(new Keyframe(0f, -0.1296169f, 0.4605525f, 0.4605525f), new Keyframe(0.07500369f, -0.1611002f, 0f, 0f), new Keyframe(0.1500074f, -0.06665026f, 0f, 0f), new Keyframe(0.2250111f, -0.2555501f, 0f, 0f), new Keyframe(0.3000148f, 0.02779965f, 0f, 0f), new Keyframe(0.3750185f, -0.35f, 0f, 0f), new Keyframe(0.5000246f, 0.5000492f, 11.70122f, 11.70122f), new Keyframe(0.625f, 1.25f, -0.121671f, -0.121671f), new Keyframe(0.7080119f, 0.7316632f, -1.725249f, -1.725249f), new Keyframe(0.7978281f, 1.118169f, 0.3863115f, 0.3863115f), new Keyframe(0.8704107f, 0.8603979f, 0.1551553f, 0.1551553f), new Keyframe(0.9472989f, 1.033288f, 0.04791922f, 0.04791922f), new Keyframe(1f, 1f, -0.6316382f, -0.6316382f)); return easeInOutElastic; } }
        
        public static AnimationCurve EaseWavePositive { get { if (easeWavePositive == null) easeWavePositive = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.5f, 1f, 0f, 0f), new Keyframe(1f, 0f, 0f, 0f)); return easeWavePositive; } }
        public static AnimationCurve EaseWaveNegative { get { if (easeWaveNegative == null) easeWaveNegative = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.5f, -1f, 0f, 0f), new Keyframe(1f, 0f, 0f, 0f)); return easeWaveNegative; } }

        public static AnimationCurve EaseWaveStrongPositive { get { if (easeWaveStrongPositive == null) easeWaveStrongPositive = new AnimationCurve(new Keyframe(0f, 0f, 4.722973f, 4.722973f), new Keyframe(0.5f, 1f, 0f, 0f), new Keyframe(1f, 0f, -5.247751f, -5.247751f)); return easeWaveStrongPositive; } }
        public static AnimationCurve EaseWaveStrongNegative { get { if (easeWaveStrongNegative == null) easeWaveStrongNegative = new AnimationCurve(new Keyframe(0f, 0f, -5.086275f, -5.086275f), new Keyframe(0.5f, -1f, 0f, 0f), new Keyframe(1f, 0f, 4.722971f, 4.722971f)); return easeWaveStrongNegative; } }


        public static AnimationCurve EaseWobble { get { if (easeWobble == null) easeWobble = new AnimationCurve(new Keyframe(0f, 0f, 11.01978f, 30.76278f), new Keyframe(0.08054394f, 1f, 0f, 0f), new Keyframe(0.3153235f, -0.75f, 0f, 0f), new Keyframe(0.5614113f, 0.5f, 0f, 0f), new Keyframe(0.75f, -0.25f, 0f, 0f), new Keyframe(0.9086903f, 0.1361611f, 0f, 0f), new Keyframe(1f, 0f, -4.159244f, -1.351373f)); return easeWobble; } }

        #endregion // curves

        static TweenBase ProcessTween(TweenBase tween)
        {
            if (UnityEngine.Application.isPlaying == false)
                return null;

            TweenManager.Instance.ProcessTween(tween);
            return tween;
        }


        // Actions

        public static TweenBase Float(float startFloat, float targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<float> updateEvent = null)
        {
            TweenBase tween = new TweenFloat(Tween.TweenTarget.Float, tweenID, startFloat, targetFloat, duration, delay, ignoreTimeScale, curve, loop, updateEvent);
            return ProcessTween(tween);
        }

        public static TweenBase Vector3(Vector3 startVector, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> updateEvent = null)
        {
            TweenBase tween = new TweenVector3(Tween.TweenTarget.Vector3, tweenID, startVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, updateEvent);
            return ProcessTween(tween);
        }
    }
}