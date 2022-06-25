
using UnityEngine;

namespace StardropTools.Audio
{
    [CreateAssetMenu(menuName = "Stardrop / Audio / Audio Database")]
    public class AudioDatabaseSO : ScriptableObject
    {
        [SerializeField] System.Collections.Generic.List<AudioClip> clips;
        [SerializeField] float minPitch = 1;
        [SerializeField] float maxPitch = 1.2f;
        [Space]
        [SerializeField] bool clearClips;

        public System.Collections.Generic.List<AudioClip> Clips { get => clips; }
        public float MinPitch { get => minPitch; }
        public float MaxPitch { get => maxPitch; }

        public void AddClip(AudioClip clip)
        {
            if (clips == null)
                clips = new System.Collections.Generic.List<AudioClip>();

            if (clips.Contains(clip) == false)
                clips.Add(clip);
        }

        public void SetClips(AudioClip[] clips)
        {
            if (this.clips == null)
                this.clips = new System.Collections.Generic.List<AudioClip>();

            for (int i = 0; i < clips.Length; i++)
                if (this.clips.Contains(clips[i]) == false)
                    this.clips.Add(clips[i]);
        }

        public AudioClip GetRandomClip() => clips[Random.Range(0, clips.Count)];

        public AudioClip GetClipAtIndex(int index) => clips[index];

        public float GetRandomPitch() => Random.Range(minPitch, maxPitch);

        protected virtual void OnValidate()
        {
            if (clearClips)
            {
                clips = new System.Collections.Generic.List<AudioClip>();
                clearClips = false;
            }
        }
    }
}