

namespace StardropTools.TweenSurge
{
    [UnityEngine.AddComponentMenu("TweenSurge / Tween Action / Tween Action Image Color")]
    public class ImageColorTweenSurgeAction : TweenSurgeActionColor
    {
        public UnityEngine.UI.Image image;

        public override void OnValidateMethods()
        {
            base.OnValidateMethods();

            if (image == null)
                return;

            if (setName)
                SetName(image.name);
            setName = false;

            if (copyValues == false)
                return;

            start = image.color;
            target = image.color;

            copyValues = false;
        }
    }
}