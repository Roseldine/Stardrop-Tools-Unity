﻿
using UnityEngine;

/// <summary>
/// Input Class focused on user swipe input (pc or mobile)
/// </summary>
public class SwipeManager : StardropTools.Singletons.SingletonCoreManager<SwipeManager>
{
    public enum SwipeDirection
    {
        none,

        left,
        right,

        up,
        upLeft,
        upRight,

        down,
        downLeft,
        downRight
    }

    [Header("Swipe Params")]
    [SerializeField] Vector2 screenSize;
    [SerializeField] float screenAvg = 0;
    [SerializeField] float screenPercentTarget = .05f;
    [Space]
    [SerializeField] float minSwipeDistance = 80;
    [SerializeField] float swipeTime = .2f;

    [Header("Internal Variables")]
    [SerializeField] bool hasInput;
    [Space]
    [SerializeField] float deltaDragTime;
    [SerializeField] float dragDistance;

    float startPosX;
    float startPosY;

    float endPosX;
    float endPosY;

    float distanceX;
    float distanceY;

    SwipeData data;

    [Header("Result")]
    public SwipeDirection swipeDirection;
    [SerializeField] bool debug;

    #region Events

    public static readonly CoreEvent<SwipeData> OnSwipe = new CoreEvent<SwipeData>();

    public static readonly CoreEvent<int> OnSwipeHorizontal = new CoreEvent<int>();
    public static readonly CoreEvent<int> OnSwipeVertical = new CoreEvent<int>();

    public static readonly CoreEvent OnSwipeUp = new CoreEvent();
    public static readonly CoreEvent OnSwipeDown = new CoreEvent();

    public static readonly CoreEvent OnSwipeLeft = new CoreEvent();
    public static readonly CoreEvent OnSwipeRight = new CoreEvent();

    public static readonly CoreEvent OnSwipeUpLeft = new CoreEvent();
    public static readonly CoreEvent OnSwipeUpRight = new CoreEvent();

    public static readonly CoreEvent OnSwipeDownLeft = new CoreEvent();
    public static readonly CoreEvent OnSwipeDownRight = new CoreEvent();

    #endregion // events

    public override void Initialize()
    {
        base.Initialize();
        screenSize = new Vector2(Screen.width, Screen.height);
        CalculateScreenPercent();
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
        CheckSwipe();
    }

    public void CheckSwipe()
    {
        #region Standalone
        if (Input.GetMouseButton(0))
        {
            if (hasInput == false)
            {
                ResetValues();
                deltaDragTime = 0;

                startPosX = Input.mousePosition.x;
                startPosY = Input.mousePosition.y;

                hasInput = true;
            }

            else if (hasInput && deltaDragTime < swipeTime)
            {
                deltaDragTime += Time.deltaTime;

                endPosX = Input.mousePosition.x;
                endPosY = Input.mousePosition.y;

                distanceX = endPosX - startPosX;
                distanceY = endPosY - startPosY;

                dragDistance = Vector2.Distance(new Vector2(startPosX, startPosY), new Vector2(endPosX, endPosY));
            }
        }

        else
        {
            if (hasInput == true)
            {
                Swipe();
                hasInput = false;
            }
        }
        #endregion // standalone
    }

    public void Swipe()
    {
        var touchVector = new Vector2(distanceX, distanceY);

        if (touchVector.magnitude > minSwipeDistance && deltaDragTime <= swipeTime)
        {
            float angle = Mathf.Atan2(distanceY, distanceX) / Mathf.PI;

            if (debug) Debug.Log("Swipe angle = " + angle);

            //-------- 4 directions
            // left
            if (angle < -0.875f || angle > 0.875f)
            {
                swipeDirection = SwipeDirection.left;
                if (debug) Debug.Log("Left");

                OnSwipeHorizontal?.Invoke(1);
                OnSwipeLeft?.Invoke();
            }

            // right
            else if (angle > -0.125f && angle < 0.125f)
            {
                swipeDirection = SwipeDirection.right;
                if (debug) Debug.Log("Right");

                OnSwipeHorizontal?.Invoke(-1);
                OnSwipeRight?.Invoke();
            }

            // up
            if (angle > 0.375f && angle < 0.625f)
            {
                swipeDirection = SwipeDirection.up;
                if (debug) Debug.Log("Up");

                OnSwipeVertical?.Invoke(-1);
                OnSwipeUp?.Invoke();
            }

            // down
            else if (angle < -0.375f && angle > -0.625f)
            {
                swipeDirection = SwipeDirection.down;
                if (debug) Debug.Log("Down");

                OnSwipeVertical?.Invoke(1);
                OnSwipeDown?.Invoke();
            }



            //-------- 4 diagonal directions

            // up left
            if (angle > 0.625f && angle < 0.875f)
            {
                swipeDirection = SwipeDirection.upLeft;
                if (debug) Debug.Log("Up Left");

                OnSwipeUpLeft?.Invoke();
            }

            // up right
            else if (angle > 0.125f && angle < 0.375f)
            {
                swipeDirection = SwipeDirection.upRight;
                if (debug) Debug.Log("Up Right");

                OnSwipeUpRight?.Invoke();
            }

            // down left
            else if (angle < -0.625f && angle > -0.875f)
            {
                swipeDirection = SwipeDirection.downLeft;
                if (debug) Debug.Log("Down Left");

                OnSwipeDownLeft?.Invoke();
            }

            // down right
            else if (angle < -0.125f && angle > -0.375f)
            {
                swipeDirection = SwipeDirection.downRight;
                if (debug) Debug.Log("Down Right");

                OnSwipeDownRight?.Invoke();
            }

            // Create swipe data
            GenerateData();

            // Invoke Swipe Event
            OnSwipe?.Invoke(data);
            if (debug) Debug.Log("Swiped: " + swipeDirection);
        }
    }

    SwipeData GenerateData()
    {
        Vector2 startPos = new Vector2(startPosX, startPosY);
        Vector2 endPos = new Vector2(endPosX, endPosY);
        Vector2 direction = data.endPoint - data.startPoint;
        data = new SwipeData(swipeDirection, startPos, endPos, direction);

        return data;
    }

    private void ResetValues()
    {
        swipeDirection = SwipeDirection.none;
        hasInput = false;
        deltaDragTime = 0;

        startPosX = 0;
        startPosY = 0;

        distanceX = 0;
        distanceY = 0;

        data = new SwipeData();
    }

    void CalculateScreenPercent()
    {
        screenAvg = (screenSize.x + screenSize.y) * .5f;
        minSwipeDistance = screenAvg * screenPercentTarget;
    }
}
