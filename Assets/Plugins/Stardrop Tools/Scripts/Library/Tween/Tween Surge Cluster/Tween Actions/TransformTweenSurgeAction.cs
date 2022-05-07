

namespace StardropTools.TweenSurge
{
    [UnityEngine.AddComponentMenu("TweenSurge / Tween Action / Tween Action Transform")]
    public class TransformTweenSurgeAction : TweenSurgeActionVector3
    {
        public UnityEngine.Transform agent;

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

            if (type == Pixelplacement.Tween.TweenType.Position)
            {
                start = agent.position;
                target = agent.position;
            }

            else if (type == Pixelplacement.Tween.TweenType.Rotation)
            {
                start = agent.rotation.eulerAngles;
                target = agent.rotation.eulerAngles;
            }

            else if (type == Pixelplacement.Tween.TweenType.LocalScale)
            {
                start = agent.localScale;
                target = agent.localScale;
            }

            copyValues = false;
        }
    }
}