using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioAmplifier : MonoBehaviour
{
    public AudioSource originalAudioSource; // A fonte de áudio original
    public float amplificationFactor = 2.0f; // Fator de amplificação, 200%

    private AudioSource amplifiedAudioSource;
    private AudioClip amplifiedClip;

    void Start()
    {
        if (originalAudioSource != null && originalAudioSource.clip != null)
        {
            // Amplificar o áudio original
            amplifiedClip = AmplifyAudioClip(originalAudioSource.clip, amplificationFactor);

            // Criar uma nova fonte de áudio para o áudio amplificado
            amplifiedAudioSource = gameObject.AddComponent<AudioSource>();
            amplifiedAudioSource.clip = amplifiedClip;
        }
    }

    void Update()
    {
        if (originalAudioSource != null && amplifiedAudioSource != null)
        {
            // Sincronizar o estado de reprodução
            if (originalAudioSource.isPlaying && !amplifiedAudioSource.isPlaying)
            {
                amplifiedAudioSource.time = originalAudioSource.time;
                amplifiedAudioSource.Play();
            }
            else if (!originalAudioSource.isPlaying && amplifiedAudioSource.isPlaying)
            {
                amplifiedAudioSource.Pause();
            }
        }
    }

    AudioClip AmplifyAudioClip(AudioClip originalClip, float amplificationFactor)
    {
        float[] data = new float[originalClip.samples * originalClip.channels];
        originalClip.GetData(data, 0);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] *= amplificationFactor;
        }

        AudioClip newClip = AudioClip.Create(originalClip.name + "_amplified", originalClip.samples, originalClip.channels, originalClip.frequency, false);
        newClip.SetData(data, 0);

        return newClip;
    }
}