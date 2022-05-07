
using UnityEngine;

namespace StardropTools
{
    [RequireComponent(typeof(RectTransform))]
    public class CoreRectTransform : CoreTransform
    {
        [SerializeField] protected CoreRectTransformData rectData;

        public RectTransform RectTransform { get => rectData.RectTransform; }
        public CoreRectTransformData.PivotEnum DataPivot { get => rectData.Pivot; set => rectData.SetDataPivot(value); }
        public CoreRectTransformData.AnchorEnum DataAnchor { get => rectData.Anchor; set => rectData.SetDataAnchor(value); }

        public CoreRectTransformData.LayoutOptions DataLayout { get => rectData.LayoutOption; set => rectData.SetDataLayout(value); }
        public CoreRectTransformData.OrientationEnum DataOrientation { get => rectData.Orientation; set => rectData.SetDataOrientation(value); }
        public CoreRectTransformData.StretchEnum DataStretch { get => rectData.Stretch; set => rectData.SetDataStretch(value); }

        public Vector2 Size { get => RectTransform.sizeDelta; set => RectTransform.sizeDelta = value; }
        public Vector2 Pivot { get => RectTransform.pivot; }
        public Vector2 AnchoredPosition { get => RectTransform.anchoredPosition; set => RectTransform.anchoredPosition = value; }

        public float Width { get => Size.x; set => Size.SetX(value); }
        public float Height { get => Size.y; set => Size.SetY(value); }

        public float TargetWidth { get => rectData.TargetWidth; }
        public float TargetHeight { get => rectData.TargetHeight; }

        protected override void DataCheck()
        {
            base.DataCheck();
            if (rectData == null)
                rectData = new CoreRectTransformData();
        }

        protected void ValidatePivotAndAnchor()
        {
            if (rectData.LayoutOption != CoreRectTransformData.LayoutOptions.none)
            {
                if (rectData.LayoutOption == CoreRectTransformData.LayoutOptions.orientation)
                    ValidateOrientation();
                else if (rectData.LayoutOption == CoreRectTransformData.LayoutOptions.stretch)
                    ValidateStretch();

                return;
            }

            #region Anchor
            // upper
            if (DataAnchor == CoreRectTransformData.AnchorEnum.upperCornerLeft)
                SetAnchorUpperCornerLeft();

            else if (DataAnchor == CoreRectTransformData.AnchorEnum.upperCenter)
                SetAnchorUpperCenter();

            else if (DataAnchor == CoreRectTransformData.AnchorEnum.upperCornerRight)
                SetAnchorUpperCornerRight();


            // middle
            if (DataAnchor == CoreRectTransformData.AnchorEnum.middleLeft)
                SetAnchorMiddleLeft();

            else if (DataAnchor == CoreRectTransformData.AnchorEnum.middleCenter)
                SetAnchorMiddleCenter();

            else if (DataAnchor == CoreRectTransformData.AnchorEnum.middleRight)
                SetAnchorMiddleRight();


            // lower
            if (DataAnchor == CoreRectTransformData.AnchorEnum.lowerCornerLeft)
                SetAnchorLowerCornerLeft();

            else if (DataAnchor == CoreRectTransformData.AnchorEnum.lowerCenter)
                SetAnchorLowerCenter();

            else if (DataAnchor == CoreRectTransformData.AnchorEnum.lowerCornerRight)
                SetAnchorLowerCornerRight();

            #endregion // anchor

            #region Pivot
            // upper
            if (DataPivot == CoreRectTransformData.PivotEnum.upperCornerLeft)
                SetPivotUpperCornerLeft();

            else if (DataPivot == CoreRectTransformData.PivotEnum.upperCenter)
                SetPivotUpperCenter();

            else if (DataPivot == CoreRectTransformData.PivotEnum.upperCornerRight)
                SetPivotUpperCornerRight();


            // middle
            else if (DataPivot == CoreRectTransformData.PivotEnum.middleLeft)
                SetPivotMiddleLeft();

            else if (DataPivot == CoreRectTransformData.PivotEnum.middleCenter)
                SetPivotMiddleCenter();

            else if (DataPivot == CoreRectTransformData.PivotEnum.middleRight)
                SetPivotMiddleRight();


            // lower
            else if (DataPivot == CoreRectTransformData.PivotEnum.lowerCornerLeft)
                SetPivotLowerCornerLeft();

            else if (DataPivot == CoreRectTransformData.PivotEnum.lowerCenter)
                SetPivotLowerCenter();

            else if (DataPivot == CoreRectTransformData.PivotEnum.lowerCornerRight)
                SetPivotLowerCornerRight();
            #endregion // pivot
        }

        void ValidateOrientation()
        {
            if (DataOrientation == CoreRectTransformData.OrientationEnum.none)
                return;


            if (DataOrientation == CoreRectTransformData.OrientationEnum.center)
                SetOrientationCenter();

            else if (DataOrientation == CoreRectTransformData.OrientationEnum.top)
                SetOrientationTop();

            else if (DataOrientation == CoreRectTransformData.OrientationEnum.left)
                SetOrientationLeft();

            else if (DataOrientation == CoreRectTransformData.OrientationEnum.right)
                SetOrientationRight();

            else if (DataOrientation == CoreRectTransformData.OrientationEnum.bottom)
                SetOrientationBottom();
        }

        void ValidateStretch()
        {
            if (DataStretch == CoreRectTransformData.StretchEnum.none)
                return;


            if (DataStretch == CoreRectTransformData.StretchEnum.top)
                SetStretchTop();

            if (DataStretch == CoreRectTransformData.StretchEnum.left)
                SetStretchLeft();

            else if (DataStretch == CoreRectTransformData.StretchEnum.right)
                SetStretchRight();

            else if (DataStretch == CoreRectTransformData.StretchEnum.bottom)
                SetStretchBottom();
        }

        #region Set Pivot
        public void SetPivot(Vector2 vector)
            => RectTransform.pivot = vector;
        public void SetPivot(float xValue, float yValue)
            => RectTransform.pivot = new Vector2(xValue, yValue);

        // upper
        public void SetPivotUpperCornerLeft()
        {
            SetPivot(0, 1);
            DataPivot = CoreRectTransformData.PivotEnum.upperCornerLeft;
        }
        public void SetPivotUpperCenter()
        {
            SetPivot(.5f, 1);
            DataPivot = CoreRectTransformData.PivotEnum.upperCenter;
        }
        public void SetPivotUpperCornerRight()
        {
            SetPivot(1, 1);
            DataPivot = CoreRectTransformData.PivotEnum.upperCornerRight;
        }

        // middle
        public void SetPivotMiddleLeft()
        {
            SetPivot(0, .5f);
            DataPivot = CoreRectTransformData.PivotEnum.middleLeft;
        }
        public void SetPivotMiddleCenter()
        {
            SetPivot(.5f, .5f);
            DataPivot = CoreRectTransformData.PivotEnum.middleCenter;
        }
        public void SetPivotMiddleRight()
        {
            SetPivot(1, .5f);
            DataPivot = CoreRectTransformData.PivotEnum.middleRight;
        }

        // lower
        public void SetPivotLowerCornerLeft()
        {
            SetPivot(0, 0);
            DataPivot = CoreRectTransformData.PivotEnum.lowerCornerLeft;
        }
        public void SetPivotLowerCenter()
        {
            SetPivot(.5f, 0);
            DataPivot = CoreRectTransformData.PivotEnum.lowerCenter;
        }
        public void SetPivotLowerCornerRight()
        {
            SetPivot(1, 0);
            DataPivot = CoreRectTransformData.PivotEnum.lowerCornerLeft;
        }

        #endregion // set pivot

        #region Set Anchor
        protected void CheckIfNotStretching()
        {
            if (rectData.IsStretching == false)
                SetTargetSize();
        }

        // upper
        public void SetAnchorUpperCornerLeft()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(0, 1);
            RectTransform.anchorMax = new Vector2(0, 1);

            DataAnchor = CoreRectTransformData.AnchorEnum.upperCornerLeft;
        }
        public void SetAnchorUpperCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, 1);
            RectTransform.anchorMax = new Vector2(.5f, 1);

            DataAnchor = CoreRectTransformData.AnchorEnum.upperCenter;
        }
        public void SetAnchorUpperCornerRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, 1);
            RectTransform.anchorMax = new Vector2(1, 1);

            DataAnchor = CoreRectTransformData.AnchorEnum.upperCornerRight;
        }

        // middle
        public void SetAnchorMiddleLeft()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(0, .5f);
            RectTransform.anchorMax = new Vector2(0, .5f);

            DataAnchor = CoreRectTransformData.AnchorEnum.middleLeft;
        }
        public void SetAnchorMiddleCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, .5f);
            RectTransform.anchorMax = new Vector2(.5f, .5f);

            DataAnchor = CoreRectTransformData.AnchorEnum.middleCenter;
        }
        public void SetAnchorMiddleRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, .5f);
            RectTransform.anchorMax = new Vector2(1, .5f);

            DataAnchor = CoreRectTransformData.AnchorEnum.middleRight;
        }

        // lower
        public void SetAnchorLowerCornerLeft()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 0);

            DataAnchor = CoreRectTransformData.AnchorEnum.lowerCornerLeft;
        }
        public void SetAnchorLowerCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, 0);
            RectTransform.anchorMax = new Vector2(.5f, 0);

            DataAnchor = CoreRectTransformData.AnchorEnum.lowerCenter;
        }
        public void SetAnchorLowerCornerRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, 0);
            RectTransform.anchorMax = new Vector2(1, 0);

            DataAnchor = CoreRectTransformData.AnchorEnum.lowerCornerRight;
        }
        #endregion // set anchor

        #region Set Orientation
        public void SetOrientationCenter()
        {
            SetAnchorMiddleCenter();
            SetPivotMiddleCenter();
        }

        public void SetOrientationTop()
        {
            SetAnchorUpperCenter();
            SetPivotUpperCenter();
        }

        public void SetOrientationLeft()
        {
            SetAnchorMiddleLeft();
            SetPivotMiddleLeft();
        }

        public void SetOrientationRight()
        {
            SetAnchorMiddleRight();
            SetPivotMiddleRight();
        }

        public void SetOrientationBottom()
        {
            SetAnchorLowerCenter();
            SetPivotLowerCenter();
        }
        #endregion // orientation

        #region Set Stretch
        void CheckStretchWidth()
        {
            if (Width != TargetWidth || Height != 0)
                SetSize(TargetWidth, 0);
        }

        void CheckStretchHeight()
        {
            if (Height != TargetHeight || Width != 0)
                SetSize(0, TargetHeight);
        }

        public void SetStretchTop()
        {
            SetPivotUpperCenter();

            RectTransform.anchorMin = new Vector2(0, 1);
            RectTransform.anchorMax = new Vector2(1, 1);
            DataAnchor = CoreRectTransformData.AnchorEnum.stretchTop;

            CheckStretchHeight();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchLeft()
        {
            SetPivotMiddleLeft();

            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 1);
            DataAnchor = CoreRectTransformData.AnchorEnum.stretchLeft;

            CheckStretchWidth();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchRight()
        {
            SetPivotMiddleRight();

            RectTransform.anchorMin = new Vector2(1, 0);
            RectTransform.anchorMax = new Vector2(1, 1);
            DataAnchor = CoreRectTransformData.AnchorEnum.stretchRight;

            CheckStretchWidth();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchBottom()
        {
            SetPivotLowerCenter();

            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(1, 0);
            DataAnchor = CoreRectTransformData.AnchorEnum.stretchBottom;

            CheckStretchHeight();

            RectTransform.anchoredPosition = Vector2.zero;
        }
        #endregion // set stretch



        public void SetSize(Vector2 size) => Size = size;
        public void SetSize(float width, float height)
            => Size = new Vector2(width, height);


        public void SetWidth(float width)
            => Width = width;
        public void SetHeight(float height)
            => Height = height;

        public void SetTargetSize()
        {
            if (rectData.UseTargetSize)
                SetSize(new Vector2(TargetWidth, TargetHeight));
        }
        public void SetTargetWidth()
            => SetWidth(TargetWidth);
        public void SetTargetHeight()
            => SetHeight(TargetHeight);
        

        public void ResetCenter()
        {
            rectData.SetDataAnchor(CoreRectTransformData.AnchorEnum.middleCenter);
            rectData.SetDataPivot(CoreRectTransformData.PivotEnum.middleCenter);
            rectData.SetDataStretch(CoreRectTransformData.StretchEnum.none);
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (rectData == null)
                rectData = new CoreRectTransformData();

            if (rectData.resetCenter)
            {
                ResetCenter();
                rectData.resetCenter = false;
            }

            rectData.SetRectTransform(GetComponent<RectTransform>());
            ValidatePivotAndAnchor();
        }
    }
}