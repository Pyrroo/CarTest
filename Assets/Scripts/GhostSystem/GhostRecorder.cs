using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    [HideInInspector]
    public List<GhostFrame> recordedFrames = new List<GhostFrame>();

    [SerializeField] private Transform target; // объект, за которым записываем, иначе призрак всегда будет приподнят над реальным положением

    private float startTime;
    private bool isRecording;

    public void StartRecording(float interval = 0.02f)
    {
        if (target == null) target = transform;

        recordedFrames.Clear();
        startTime = Time.time;
        isRecording = true;
        InvokeRepeating(nameof(RecordFrame), 0f, interval);
    }

    public void StopRecording()
    {
        isRecording = false;
        CancelInvoke(nameof(RecordFrame));
    }

    private void RecordFrame()
    {
        if (!isRecording) return;

        recordedFrames.Add(new GhostFrame
        {
            position = target.position,
            rotation = target.rotation,
            time = Time.time - startTime
        });
    }
}
