// Code taken from this video: https://youtu.be/gIjajeyjRfE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour {
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;

    private void Update() {

        foreach (Intervals i in intervals)
        {
            float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * i.GetIntervalLength(bpm)));
            i.CheckForNewInterval(sampledTime);
        }
    }
}

[System.Serializable]
public class Intervals {
    [SerializeField] private float steps;
    [SerializeField] private UnityEvent trigger;
    private int lastInterval;

    public float GetIntervalLength(float bpm) {
        return 60f / (bpm * steps);
    }

    public void CheckForNewInterval(float interval) {
        if (Mathf.FloorToInt(interval) != lastInterval) {
            lastInterval = Mathf.FloorToInt(interval);
            trigger.Invoke();
        }
    }
}