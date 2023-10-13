using BenStudios;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GenericTimer : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// This event will triggered for each second elapsed.
    /// </summary>
    public UnityAction OnTimerTick = default;
    public UnityAction OnTimerComplete = default;

    public bool IsTimerCompleted => _isTimerCompleted;

    [SerializeField] private TextMeshProUGUI m_timerTxt;
    private int _timerCounter = 0;
    private int _totalTimeInSeconds;
    private bool _isTimerCompleted;
    private bool _isTimerRunning = false;

    #endregion Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Public Methods 
    public void InitTimer(int timeInSeconds)
    {
        _totalTimeInSeconds = _timerCounter = timeInSeconds;
        _isTimerCompleted = false;
        _isTimerRunning = false;
        if (m_timerTxt)
            m_timerTxt.text = MyUtils.GetFormattedSeconds(_timerCounter);
    }
    public void InitTimer(int totalTimeInSeconds, int elapsedSeconds)
    {
        _totalTimeInSeconds = totalTimeInSeconds;
        _timerCounter = _totalTimeInSeconds - elapsedSeconds;
        if (_timerCounter < 0) _timerCounter = 0;
    }
    public void InitTimerAndStartTimer(int timeInSeconds)
    {
        _totalTimeInSeconds = _timerCounter = timeInSeconds;
        _isTimerCompleted = false;
        _isTimerRunning = false;
        StartTimer();
    }
    public void StartTimer()
    {
        if (_isTimerCompleted || _isTimerRunning) return;
        _isTimerRunning = true;
        MyUtils.Log($"Timer Started::");
        InvokeRepeating(nameof(_Tick), 1, 1);
    }
    public void StopTimer()
    {
        MyUtils.Log($"Timer Stopped::");
        _isTimerRunning = false;
        CancelInvoke(nameof(_Tick));
    }
    public void RestartTimer()
    {
        StopTimer();
        InitTimerAndStartTimer(_totalTimeInSeconds);
    }
    public void RestartTimer(int newTimeInSeconds)
    {
        _totalTimeInSeconds = newTimeInSeconds;
        StopTimer();
        InitTimerAndStartTimer(_totalTimeInSeconds);
    }
    public int GetRemaingTimeInSeconds()
    {
        return _timerCounter;
    }
    public int GetElapsedTimeInSeconds() => _totalTimeInSeconds - _timerCounter;
    #endregion Public Methods 

    #region Private Methods 
    private void _Tick()
    {
        _timerCounter--;
        if (_timerCounter <= 0)
        {
            _timerCounter = 0;
            _isTimerCompleted = true;
            OnTimerComplete?.Invoke();
            StopTimer();
        }
        OnTimerTick?.Invoke();
        if (m_timerTxt)
            m_timerTxt.text = MyUtils.GetFormattedSeconds(_timerCounter);
    }
    #endregion Private Methods 

    #region Callbacks
    #endregion Callbacks
}
