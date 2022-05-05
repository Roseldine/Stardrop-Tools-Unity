
using UnityEngine;

namespace StardropTools
{
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformObject : TransformObject
    {
        [SerializeField] protected RectTransformObjectData rectData;

        public RectTransform RectTransform { get => rectData.RectTransform; }
        public RectTransformObjectData.PivotEnum DataPivot { get => rectData.Pivot; set => rectData.SetDataPivot(value); }
        public RectTransformObjectData.AnchorEnum DataAnchor { get => rectData.Anchor; set => rectData.SetDataAnchor(value); }

        public RectTransformObjectData.LayoutOptions DataLayout { get => rectData.LayoutOption; set => rectData.SetDataLayout(value); }
        public RectTransformObjectData.OrientationEnum DataOrientation { get => rectData.Orientation; set => rectData.SetDataOrientation(value); }
        public RectTransformObjectData.StretchEnum DataStretch { get => rectData.Stretch; set => rectData.SetDataStretch(value); }

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
                rectData = new RectTransformObjectData();
        }

        protected void ValidatePivotAndAnchor()
        {
            if (rectData.LayoutOption != RectTransformObjectData.LayoutOptions.none)
            {
                if (rectData.LayoutOption == RectTransformObjectData.LayoutOptions.orientation)
                    ValidateOrientation();
                else if (rectData.LayoutOption == RectTransformObjectData.LayoutOptions.stretch)
                    ValidateStretch();

                return;
            }

            #region Anchor
            // upper
            if (DataAnchor == RectTransformObjectData.AnchorEnum.upperCornerLeft)
                SetAnchorUpperCornerLeft();

            else if (DataAnchor == RectTransformObjectData.AnchorEnum.upperCenter)
                SetAnchorUpperCenter();

            else if (DataAnchor == RectTransformObjectData.AnchorEnum.upperCornerRight)
                SetAnchorUpperCornerRight();


            // middle
            if (DataAnchor == RectTransformObjectData.AnchorEnum.middleLeft)
                SetAnchorMiddleLeft();

            else if (DataAnchor == RectTransformObjectData.AnchorEnum.middleCenter)
                SetAnchorMiddleCenter();

            else if (DataAnchor == RectTransformObjectData.AnchorEnum.middleRight)
                SetAnchorMiddleRight();


            // lower
            if (DataAnchor == RectTransformObjectData.AnchorEnum.lowerCornerLeft)
                SetAnchorLowerCornerLeft();

            else if (DataAnchor == RectTransformObjectData.AnchorEnum.lowerCenter)
                SetAnchorLowerCenter();

            else if (DataAnchor == RectTransformObjectData.AnchorEnum.lowerCornerRight)
                SetAnchorLowerCornerRight();

            #endregion // anchor

            #region Pivot
            // upper
            if (DataPivot == RectTransformObjectData.PivotEnum.upperCornerLeft)
                SetPivotUpperCornerLeft();

            else if (DataPivot == RectTransformObjectData.PivotEnum.upperCenter)
                SetPivotUpperCenter();

            else if (DataPivot == RectTransformObjectData.PivotEnum.upperCornerRight)
                SetPivotUpperCornerRight();


            // middle
            else if (DataPivot == RectTransformObjectData.PivotEnum.middleLeft)
                SetPivotMiddleLeft();

            else if (DataPivot == RectTransformObjectData.PivotEnum.middleCenter)
                SetPivotMiddleCenter();

            else if (DataPivot == RectTransformObjectData.PivotEnum.middleRight)
                SetPivotMiddleRight();


            // lower
            else if (DataPivot == RectTransformObjectData.PivotEnum.lowerCornerLeft)
                SetPivotLowerCornerLeft();

            else if (DataPivot == RectTransformObjectData.PivotEnum.lowerCenter)
                SetPivotLowerCenter();

            else if (DataPivot == RectTransformObjectData.PivotEnum.lowerCornerRight)
                SetPivotLowerCornerRight();
            #endregion // pivot
        }

        void ValidateOrientation()
        {
            if (DataOrientation == RectTransformObjectData.OrientationEnum.none)
                return;


            if (DataOrientation == RectTransformObjectData.OrientationEnum.center)
                SetOrientationCenter();

            else if (DataOrientation == RectTransformObjectData.OrientationEnum.top)
                SetOrientationTop();

            else if (DataOrientation == RectTransformObjectData.OrientationEnum.left)
                SetOrientationLeft();

            else if (DataOrientation == RectTransformObjectData.OrientationEnum.right)
                SetOrientationRight();

            else if (DataOrientation == RectTransformObjectData.OrientationEnum.bottom)
                SetOrientationBottom();
        }

        void ValidateStretch()
        {
            if (DataStretch == RectTransformObjectData.StretchEnum.none)
                return;


            if (DataStretch == RectTransformObjectData.StretchEnum.top)
                SetStretchTop();

            if (DataStretch == RectTransformObjectData.StretchEnum.left)
                SetStretchLeft();

            else if (DataStretch == RectTransformObjectData.StretchEnum.right)
                SetStretchRight();

            else if (DataStretch == RectTransformObjectData.StretchEnum.bottom)
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
            DataPivot = RectTransformObjectData.PivotEnum.upperCornerLeft;
        }
        public void SetPivotUpperCenter()
        {
            SetPivot(.5f, 1);
            DataPivot = RectTransformObjectData.PivotEnum.upperCenter;
        }
        public void SetPivotUpperCornerRight()
        {
            SetPivot(1, 1);
            DataPivot = RectTransformObjectData.PivotEnum.upperCornerRight;
        }

        // middle
        public void SetPivotMiddleLeft()
        {
            SetPivot(0, .5f);
            DataPivot = RectTransformObjectData.PivotEnum.middleLeft;
        }
        public void SetPivotMiddleCenter()
        {
            SetPivot(.5f, .5f);
            DataPivot = RectTransformObjectData.PivotEnum.middleCenter;
        }
        public void SetPivotMiddleRight()
        {
            SetPivot(1, .5f);
            DataPivot = RectTransformObjectData.PivotEnum.middleRight;
        }

        // lower
        public void SetPivotLowerCornerLeft()
        {
            SetPivot(0, 0);
            DataPivot = RectTransformObjectData.PivotEnum.lowerCornerLeft;
        }
        public void SetPivotLowerCenter()
        {
            SetPivot(.5f, 0);
            DataPivot = RectTransformObjectData.PivotEnum.lowerCenter;
        }
        public void SetPivotLowerCornerRight()
        {
            SetPivot(1, 0);
            DataPivot = RectTransformObjectData.PivotEnum.lowerCornerLeft;
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

            DataAnchor = RectTransformObjectData.AnchorEnum.upperCornerLeft;
        }
        public void SetAnchorUpperCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, 1);
            RectTransform.anchorMax = new Vector2(.5f, 1);

            DataAnchor = RectTransformObjectData.AnchorEnum.upperCenter;
        }
        public void SetAnchorUpperCornerRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, 1);
            RectTransform.anchorMax = new Vector2(1, 1);

            DataAnchor = RectTransformObjectData.AnchorEnum.upperCornerRight;
        }

        // middle
        public void SetAnchorMiddleLeft()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(0, .5f);
            RectTransform.anchorMax = new Vector2(0, .5f);

            DataAnchor = RectTransformObjectData.AnchorEnum.middleLeft;
        }
        public void SetAnchorMiddleCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, .5f);
            RectTransform.anchorMax = new Vector2(.5f, .5f);

            DataAnchor = RectTransformObjectData.AnchorEnum.middleCenter;
        }
        public void SetAnchorMiddleRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, .5f);
            RectTransform.anchorMax = new Vector2(1, .5f);

            DataAnchor = RectTransformObjectData.AnchorEnum.middleRight;
        }

        // lower
        public void SetAnchorLowerCornerLeft()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 0);

            DataAnchor = RectTransformObjectData.AnchorEnum.lowerCornerLeft;
        }
        public void SetAnchorLowerCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, 0);
            RectTransform.anchorMax = new Vector2(.5f, 0);

            DataAnchor = RectTransformObjectData.AnchorEnum.lowerCenter;
        }
        public void SetAnchorLowerCornerRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, 0);
            RectTransform.anchorMax = new Vector2(1, 0);

            DataAnchor = RectTransformObjectData.AnchorEnum.lowerCornerRight;
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
            DataAnchor = RectTransformObjectData.AnchorEnum.stretchTop;

            CheckStretchHeight();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchLeft()
        {
            SetPivotMiddleLeft();

            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 1);
            DataAnchor = RectTransformObjectData.AnchorEnum.stretchLeft;

            CheckStretchWidth();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchRight()
        {
            SetPivotMiddleRight();

            RectTransform.anchorMin = new Vector2(1, 0);
            RectTransform.anchorMax = new Vector2(1, 1);
            DataAnchor = RectTransformObjectData.AnchorEnum.stretchRight;

            CheckStretchWidth();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchBottom()
        {
            SetPivotLowerCenter();

            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(1, 0);
            DataAnchor = RectTransformObjectData.AnchorEnum.stretchBottom;

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
            => SetSize(new Vector2(TargetWidth, TargetHeight));
        public void SetTargetWidth()
            => SetWidth(TargetWidth);
        public void SetTargetHeight()
            => SetHeight(TargetHeight);
        

        public void ResetCenter()
        {
            rectData.SetDataAnchor(RectTransformObjectData.AnchorEnum.middleCenter);
            rectData.SetDataPivot(RectTransformObjectData.PivotEnum.middleCenter);
            rectData.SetDataStretch(RectTransformObjectData.StretchEnum.none);
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (rectData == null)
                rectData = new RectTransformObjectData();

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