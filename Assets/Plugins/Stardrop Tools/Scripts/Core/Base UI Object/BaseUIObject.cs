
using UnityEngine;

namespace StardropTools
{
    [RequireComponent(typeof(RectTransform))]
    public class BaseUIObject : BaseObject
    {
        [SerializeField] protected BaseUIObjectData rectData;

        public RectTransform RectTransform { get => rectData.RectTransform; }
        public BaseUIObjectData.PivotEnum DataPivot { get => rectData.Pivot; set => rectData.SetDataPivot(value); }
        public BaseUIObjectData.AnchorEnum DataAnchor { get => rectData.Anchor; set => rectData.SetDataAnchor(value); }

        public BaseUIObjectData.LayoutOptions DataLayout { get => rectData.LayoutOption; set => rectData.SetDataLayout(value); }
        public BaseUIObjectData.OrientationEnum DataOrientation { get => rectData.Orientation; set => rectData.SetDataOrientation(value); }
        public BaseUIObjectData.StretchEnum DataStretch { get => rectData.Stretch; set => rectData.SetDataStretch(value); }

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
                rectData = new BaseUIObjectData();
        }

        protected void ValidatePivotAndAnchor()
        {
            if (rectData.LayoutOption != BaseUIObjectData.LayoutOptions.none)
            {
                if (rectData.LayoutOption == BaseUIObjectData.LayoutOptions.orientation)
                    ValidateOrientation();
                else if (rectData.LayoutOption == BaseUIObjectData.LayoutOptions.stretch)
                    ValidateStretch();

                return;
            }

            #region Anchor
            // upper
            if (DataAnchor == BaseUIObjectData.AnchorEnum.upperCornerLeft)
                SetAnchorUpperCornerLeft();

            else if (DataAnchor == BaseUIObjectData.AnchorEnum.upperCenter)
                SetAnchorUpperCenter();

            else if (DataAnchor == BaseUIObjectData.AnchorEnum.upperCornerRight)
                SetAnchorUpperCornerRight();


            // middle
            if (DataAnchor == BaseUIObjectData.AnchorEnum.middleLeft)
                SetAnchorMiddleLeft();

            else if (DataAnchor == BaseUIObjectData.AnchorEnum.middleCenter)
                SetAnchorMiddleCenter();

            else if (DataAnchor == BaseUIObjectData.AnchorEnum.middleRight)
                SetAnchorMiddleRight();


            // lower
            if (DataAnchor == BaseUIObjectData.AnchorEnum.lowerCornerLeft)
                SetAnchorLowerCornerLeft();

            else if (DataAnchor == BaseUIObjectData.AnchorEnum.lowerCenter)
                SetAnchorLowerCenter();

            else if (DataAnchor == BaseUIObjectData.AnchorEnum.lowerCornerRight)
                SetAnchorLowerCornerRight();

            #endregion // anchor

            #region Pivot
            // upper
            if (DataPivot == BaseUIObjectData.PivotEnum.upperCornerLeft)
                SetPivotUpperCornerLeft();

            else if (DataPivot == BaseUIObjectData.PivotEnum.upperCenter)
                SetPivotUpperCenter();

            else if (DataPivot == BaseUIObjectData.PivotEnum.upperCornerRight)
                SetPivotUpperCornerRight();


            // middle
            else if (DataPivot == BaseUIObjectData.PivotEnum.middleLeft)
                SetPivotMiddleLeft();

            else if (DataPivot == BaseUIObjectData.PivotEnum.middleCenter)
                SetPivotMiddleCenter();

            else if (DataPivot == BaseUIObjectData.PivotEnum.middleRight)
                SetPivotMiddleRight();


            // lower
            else if (DataPivot == BaseUIObjectData.PivotEnum.lowerCornerLeft)
                SetPivotLowerCornerLeft();

            else if (DataPivot == BaseUIObjectData.PivotEnum.lowerCenter)
                SetPivotLowerCenter();

            else if (DataPivot == BaseUIObjectData.PivotEnum.lowerCornerRight)
                SetPivotLowerCornerRight();
            #endregion // pivot
        }

        void ValidateOrientation()
        {
            if (DataOrientation == BaseUIObjectData.OrientationEnum.none)
                return;


            if (DataOrientation == BaseUIObjectData.OrientationEnum.center)
                SetOrientationCenter();

            else if (DataOrientation == BaseUIObjectData.OrientationEnum.top)
                SetOrientationTop();

            else if (DataOrientation == BaseUIObjectData.OrientationEnum.left)
                SetOrientationLeft();

            else if (DataOrientation == BaseUIObjectData.OrientationEnum.right)
                SetOrientationRight();

            else if (DataOrientation == BaseUIObjectData.OrientationEnum.bottom)
                SetOrientationBottom();
        }

        void ValidateStretch()
        {
            if (DataStretch == BaseUIObjectData.StretchEnum.none)
                return;


            if (DataStretch == BaseUIObjectData.StretchEnum.top)
                SetStretchTop();

            if (DataStretch == BaseUIObjectData.StretchEnum.left)
                SetStretchLeft();

            else if (DataStretch == BaseUIObjectData.StretchEnum.right)
                SetStretchRight();

            else if (DataStretch == BaseUIObjectData.StretchEnum.bottom)
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
            DataPivot = BaseUIObjectData.PivotEnum.upperCornerLeft;
        }
        public void SetPivotUpperCenter()
        {
            SetPivot(.5f, 1);
            DataPivot = BaseUIObjectData.PivotEnum.upperCenter;
        }
        public void SetPivotUpperCornerRight()
        {
            SetPivot(1, 1);
            DataPivot = BaseUIObjectData.PivotEnum.upperCornerRight;
        }

        // middle
        public void SetPivotMiddleLeft()
        {
            SetPivot(0, .5f);
            DataPivot = BaseUIObjectData.PivotEnum.middleLeft;
        }
        public void SetPivotMiddleCenter()
        {
            SetPivot(.5f, .5f);
            DataPivot = BaseUIObjectData.PivotEnum.middleCenter;
        }
        public void SetPivotMiddleRight()
        {
            SetPivot(1, .5f);
            DataPivot = BaseUIObjectData.PivotEnum.middleRight;
        }

        // lower
        public void SetPivotLowerCornerLeft()
        {
            SetPivot(0, 0);
            DataPivot = BaseUIObjectData.PivotEnum.lowerCornerLeft;
        }
        public void SetPivotLowerCenter()
        {
            SetPivot(.5f, 0);
            DataPivot = BaseUIObjectData.PivotEnum.lowerCenter;
        }
        public void SetPivotLowerCornerRight()
        {
            SetPivot(1, 0);
            DataPivot = BaseUIObjectData.PivotEnum.lowerCornerLeft;
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

            DataAnchor = BaseUIObjectData.AnchorEnum.upperCornerLeft;
        }
        public void SetAnchorUpperCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, 1);
            RectTransform.anchorMax = new Vector2(.5f, 1);

            DataAnchor = BaseUIObjectData.AnchorEnum.upperCenter;
        }
        public void SetAnchorUpperCornerRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, 1);
            RectTransform.anchorMax = new Vector2(1, 1);

            DataAnchor = BaseUIObjectData.AnchorEnum.upperCornerRight;
        }

        // middle
        public void SetAnchorMiddleLeft()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(0, .5f);
            RectTransform.anchorMax = new Vector2(0, .5f);

            DataAnchor = BaseUIObjectData.AnchorEnum.middleLeft;
        }
        public void SetAnchorMiddleCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, .5f);
            RectTransform.anchorMax = new Vector2(.5f, .5f);

            DataAnchor = BaseUIObjectData.AnchorEnum.middleCenter;
        }
        public void SetAnchorMiddleRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, .5f);
            RectTransform.anchorMax = new Vector2(1, .5f);

            DataAnchor = BaseUIObjectData.AnchorEnum.middleRight;
        }

        // lower
        public void SetAnchorLowerCornerLeft()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 0);

            DataAnchor = BaseUIObjectData.AnchorEnum.lowerCornerLeft;
        }
        public void SetAnchorLowerCenter()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(.5f, 0);
            RectTransform.anchorMax = new Vector2(.5f, 0);

            DataAnchor = BaseUIObjectData.AnchorEnum.lowerCenter;
        }
        public void SetAnchorLowerCornerRight()
        {
            CheckIfNotStretching();
            RectTransform.anchorMin = new Vector2(1, 0);
            RectTransform.anchorMax = new Vector2(1, 0);

            DataAnchor = BaseUIObjectData.AnchorEnum.lowerCornerRight;
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
            DataAnchor = BaseUIObjectData.AnchorEnum.stretchTop;

            CheckStretchHeight();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchLeft()
        {
            SetPivotMiddleLeft();

            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(0, 1);
            DataAnchor = BaseUIObjectData.AnchorEnum.stretchLeft;

            CheckStretchWidth();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchRight()
        {
            SetPivotMiddleRight();

            RectTransform.anchorMin = new Vector2(1, 0);
            RectTransform.anchorMax = new Vector2(1, 1);
            DataAnchor = BaseUIObjectData.AnchorEnum.stretchRight;

            CheckStretchWidth();

            RectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetStretchBottom()
        {
            SetPivotLowerCenter();

            RectTransform.anchorMin = new Vector2(0, 0);
            RectTransform.anchorMax = new Vector2(1, 0);
            DataAnchor = BaseUIObjectData.AnchorEnum.stretchBottom;

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
            rectData.SetDataAnchor(BaseUIObjectData.AnchorEnum.middleCenter);
            rectData.SetDataPivot(BaseUIObjectData.PivotEnum.middleCenter);
            rectData.SetDataStretch(BaseUIObjectData.StretchEnum.none);
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (rectData == null)
                rectData = new BaseUIObjectData();

            if (rectData.resetCenter)
            {
                ResetCenter();
                rectData.resetCenter = false;
            }

            rectData.SetRectTransform(GetComponent<RectTransform>());

            if (rectData.ValidatePivotAndAnchor)
                ValidatePivotAndAnchor();
        }
    }
}