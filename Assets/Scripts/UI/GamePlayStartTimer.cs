using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class GamePlayStartTimer : MonoBehaviour
{
    public event Action RaceStarted; // Событие начала гонки

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float countdownTime = 5f;

    private Coroutine timerCoroutine;

    public void StartTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerText.gameObject.SetActive(true);
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        timerText.gameObject.SetActive(false);
    }

    private IEnumerator TimerCoroutine()
    {
        timerText.gameObject.SetActive(true);
        float timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            timerText.text = Mathf.Ceil(timeLeft).ToString();
            yield return null;
            timeLeft -= Time.deltaTime;
        }

        timerText.text = "GOOO!!!";

        // Оповещаем всех, кто подписан
        RaceStarted?.Invoke();

        yield return new WaitForSeconds(0.5f);
        timerText.gameObject.SetActive(false);
        timerCoroutine = null;
    }
}
