using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPlayer : MonoBehaviour
{
    public AudioClip[] Clips;
    public float PitchMin = 1.0f;
    public float PitchMax = 1.0f;

    public AudioSource source => m_Source;

    AudioSource m_Source;

    void Awake()
    {
        m_Source = GetComponent<AudioSource>();
    }

    public AudioClip GetRandomClip()
    {
        return Clips[Random.Range(0, Clips.Length)];
    }

    public void PlayRandom()
    {
        if (Clips.Length == 0)
            return;

        PlayClip(GetRandomClip(), PitchMin, PitchMax);
    }

    public void PlayClip(AudioClip clip, float pitchMin, float pitchMax)
    {
        m_Source.pitch = Random.Range(pitchMin, pitchMax);
        m_Source.PlayOneShot(clip);
    }
    public bool step = true; 
    float audioStepLengthWalk = 0.45f; 
    float audioStepLengthRun = 0.25f;
    public AudioClip[] concrete;
    public void yuru(float hiz)
    {
        WalkOnConcrete(hiz);
    }
    IEnumerator WaitForFootSteps(float stepsLength) {
        step = false; 
        yield return new WaitForSeconds(stepsLength);
        step = true; 
    } 
    /////////////////////////////////// CONCRETE //////////////////////////////////////// 
    void WalkOnConcrete(float hiz) {
        if (step)
        {
            m_Source.clip = concrete[Random.Range(0, concrete.Length)];
            m_Source.volume = 0.1f;
            m_Source.Play();
            StartCoroutine(WaitForFootSteps(hiz));
        }

    }

}
