

public static class TweenActionValidator
{
    public static void ValidateTween(TweenActionBase data)
    {
        if (data == null)
            return;

        #region Region
        #endregion // region

        if (data.duration <= 0)
        {
            UnityEngine.Debug.Log("Data has 0 duration");
            return;
        }

        
        #region Transforms

        // POSITION
        if (data.type == Pixelplacement.Tween.TweenType.Position)
        {
            var _ = (TransformTweenAction)data;
            if (_.hasStart == false)
                Pixelplacement.Tween.Position(_.agent, _.target, _.duration, _.delay, _.curve);
            else
                Pixelplacement.Tween.Position(_.agent, _.start, _.target, _.duration, _.delay, _.curve);
        }

        // ROTATION
        if (data.type == Pixelplacement.Tween.TweenType.Rotation)
        {
            var _ = (TransformTweenAction)data;
            if (_.hasStart == false)
                Pixelplacement.Tween.Rotation(_.agent, _.target, _.duration, _.delay, _.curve);
            else
                Pixelplacement.Tween.Rotation(_.agent, _.start, _.target, _.duration, _.delay, _.curve);
        }

        // LOCAL SCALE
        if (data.type == Pixelplacement.Tween.TweenType.LocalScale)
        {
            var _ = (TransformTweenAction)data;
            if (_.hasStart == false)
                Pixelplacement.Tween.LocalScale(_.agent, _.target, _.duration, _.delay, _.curve);
            else
                Pixelplacement.Tween.LocalScale(_.agent, _.start, _.target, _.duration, _.delay, _.curve);
        }
        #endregion // transforms


        #region Rect Transform

        // SIZE RECT
        if (data.type == Pixelplacement.Tween.TweenType.Size)
        {
            var _ = (RectTransformTweenAction)data;
            if (_.hasStart == false)
                Pixelplacement.Tween.Size(_.agent, _.target, _.duration, _.delay, _.curve);
            else
                Pixelplacement.Tween.Size(_.agent, _.start, _.target, _.duration, _.delay, _.curve);
        }

        // ANCHORED POSITION
        if (data.type == Pixelplacement.Tween.TweenType.AnchoredPosition)
        {
            var _ = (RectTransformTweenAction)data;
            if (_.hasStart == false)
                Pixelplacement.Tween.AnchoredPosition(_.agent, _.target, _.duration, _.delay, _.curve);
            else
                Pixelplacement.Tween.AnchoredPosition(_.agent, _.start, _.target, _.duration, _.delay, _.curve);
        }
        #endregion // rect


        #region Color

        // IMAGE COLOR
        if (data.type == Pixelplacement.Tween.TweenType.ImageColor)
        {
            var _ = (ImageColorTweenAction)data;
            if (_.hasStart == false)
                Pixelplacement.Tween.Color(_.image, _.target, _.duration, _.delay, _.curve);
            else
                Pixelplacement.Tween.Color(_.image, _.start, _.target, _.duration, _.delay, _.curve);
        }

        #endregion // color



    }
}
