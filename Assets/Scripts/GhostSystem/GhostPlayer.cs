using UnityEngine;
using System.Collections;

public class GhostPlayer : MonoBehaviour
{
    public GhostRecorder sourceRecorder;

    public void StartPlayback()
    {
        StartCoroutine(PlaybackCoroutine());
    }

    private IEnumerator PlaybackCoroutine()
    {
        if (sourceRecorder.recordedFrames.Count == 0)
            yield break;

        if (!GameController.isMenu)
        {
            float startTime = Time.time;

            for (int i = 0; i < sourceRecorder.recordedFrames.Count; i++)
            {
                GhostFrame frame = sourceRecorder.recordedFrames[i];
                transform.position = frame.position;
                transform.rotation = frame.rotation;

                if (i < sourceRecorder.recordedFrames.Count - 1)
                {
                    float waitTime = sourceRecorder.recordedFrames[i + 1].time - frame.time;
                    yield return new WaitForSeconds(waitTime);
                }
            }
        }

        
    }
}
