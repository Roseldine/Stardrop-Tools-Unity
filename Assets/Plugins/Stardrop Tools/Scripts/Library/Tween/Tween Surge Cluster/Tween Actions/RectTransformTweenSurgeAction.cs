

namespace StardropTools.TweenSurge
{
    [UnityEngine.AddComponentMenu("TweenSurge / Tween Action / Tween Action RectTransform")]
    public class RectTransformTweenSurgeAction : TweenSurgeActionVector2
    {
        public UnityEngine.RectTransform agent;

        public override void OnValidateMethods()
        {
            base.OnValidateMethods();

            if (agent == null)
                return;

            if (setName)
                SetName(agent.name);
            setName = false;

            if (copyValues == false)
                return;

            // POSITION
            if (type == Pixelplacement.Tween.TweenType.Position)
            {
                start = agent.position;
                target = agent.position;
            }

            // ROTATION
            else if (type == Pixelplacement.Tween.TweenType.Rotation)
            {
                start = agent.rotation.eulerAngles;
                target = agent.rotation.eulerAngles;
            }

            // SCALE
            else if (type == Pixelplacement.Tween.TweenType.LocalScale)
            {
                start = agent.localScale;
                target = agent.localScale;
            }

            // SIZE
            else if (type == Pixelplacement.Tween.TweenType.Size)
            {
                start = agent.sizeDelta;
                target = agent.sizeDelta;
            }

            // ANCHORED POSITION
            else if (type == Pixelplacement.Tween.TweenType.AnchoredPosition)
            {
                start = agent.anchoredPosition;
                target = agent.anchoredPosition;
            }

            copyValues = false;
        }
    }
}