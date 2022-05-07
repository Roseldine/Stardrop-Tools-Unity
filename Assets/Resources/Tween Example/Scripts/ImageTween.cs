using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools.Tween;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class ImageTween : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] UnityEngine.UI.Image image;
    [Space]
    [SerializeField] Tween.LoopType loop;
    [SerializeField] Tween.EaseCurve ease;
    [SerializeField] float duration;
    [SerializeField] float delay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            var curve = Tween.GetEaseCurve(ease);

            var color = Random.ColorHSV();
            Tween.ImageColor(image, color, duration, delay, false, curve, loop);

            var pixels = Random.Range(8, 64);
            Tween.ImagePixelsPerUnitMultiplier(image, pixels, duration, delay, false, curve, loop);

            var pos = Random.insideUnitCircle * 50 + rectTransform.anchoredPosition;
            Tween.AnchoredPosition(rectTransform, pos, duration, delay, false, curve, loop);
        }
    }

    float GetRandom360() => Random.Range(-360, 360);

    private void OnValidate()
    {
        if (image == null)
            image = GetComponent<UnityEngine.UI.Image>();
    }
}
