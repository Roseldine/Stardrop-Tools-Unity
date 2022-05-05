
using UnityEngine;

[AddComponentMenu("Stadrop / Tween / Tween Cluster")]
public class TweenCluster : MonoBehaviour
{
    public int index;
    [SerializeField] float delay;
    [SerializeField] TweenActionBase[] tweenActions;
    [SerializeField] bool getData;

    public virtual void InitializeTweens()
    {
        if (delay > 0)
            Invoke(nameof(ValidateTweens), delay);
        else
            ValidateTweens();
    }

    void ValidateTweens()
    {
        if (tweenActions != null && tweenActions.Length > 0)
            foreach (var data in tweenActions)
                TweenActionValidator.ValidateTween(data);
    }

    public void GetSequenceData()
    {
        if (tweenActions != null && tweenActions.Length != transform.childCount)
            tweenActions = Utilities.GetItems<TweenActionBase>(transform);

        if (tweenActions != null && tweenActions.Length > 0)
            for (int i = 0; i < tweenActions.Length; i++)
                tweenActions[i].index = i;
    }

    private void OnValidate()
    {
        if (getData)
        {
            GetSequenceData();
            getData = false;
        }
    }
}
