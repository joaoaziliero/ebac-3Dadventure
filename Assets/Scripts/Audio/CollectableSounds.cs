using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableSounds : MonoBehaviour
{
    [Serializable]
    public class SoundPairings
    {
        public TextMeshProUGUI display;
        public AudioClip sound;
    }

    public List<SoundPairings> soundPairings;

    private void Start()
    {
        soundPairings.ForEach(pair =>
        {
            Observable
                .EveryValueChanged(pair.display, display => display.text)
                .Skip(1)
                .Subscribe(_ =>
                {
                    var audioSource = gameObject.AddComponent<AudioSource>();

                    audioSource.clip = pair.sound;
                    audioSource.Play();

                    Observable
                        .Timer(TimeSpan.FromSeconds(pair.sound.length))
                        .Subscribe(_ => Destroy(audioSource))
                        .AddTo(audioSource);
                })
                .AddTo(this);
        });
    }
}
