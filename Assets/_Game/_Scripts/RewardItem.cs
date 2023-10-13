using BenStudios.ScreenManagement;
using Coffee.UIEffects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
    #region Varibales
    [SerializeField] private Image _rewardBg;
    [SerializeField] private Image _rewardImg;
    [SerializeField] private SpriteDatabase _rewardSpriteDatabase;
    [SerializeField] private UIEffect _uiEffect;

    [Space(10)]
    [Header("CoolDown")]
    [SerializeField] private GameObject _coolDownStatePanel;
    [SerializeField] private TextMeshProUGUI _cooldownTimerTxt;
    [SerializeField] private Image _cooldownTimerProgressbar;
    [SerializeField] private GenericTimer _cooldownTimer;

    [Space(10)]
    [Header("GoState")]
    [SerializeField] private GameObject _goStatePanel;
    [SerializeField] private TextMeshProUGUI _claimTimerTxt;
    [SerializeField] private TextMeshProUGUI _currencyCountTxt;
    [SerializeField] private Image _currencyImg;
    [SerializeField] private GenericTimer _goStateTimer;


    [Space(10)]
    [Header("Claim State")]
    [SerializeField] private GameObject _claimStatePanel;
    [SerializeField] private Transform _rewardImgHopRefPose;

    private Vector3 _rewardImgEuler = Vector3.zero;
    private Reward _myRewardData;
    private Status _status;
    private int _currencyCount = 0;
    #endregion Varibales

    #region Unity Methods
    private void OnDisable()
    {
        _cooldownTimer.OnTimerComplete -= OnCooldownTimerComplete;
        _cooldownTimer.OnTimerTick -= _UpdateFillBar;
        _goStateTimer.OnTimerComplete -= _OnGoStateTimerCompleted;
    }
    #endregion Unity Methods

    #region Public Methods
    public async void Init(Reward reward)
    {
        _myRewardData = reward;
        _UpdateState(System.Enum.Parse<Status>(_myRewardData.status, true));
        Texture2D tex = await MyUtils.GetTextureFromUrl(_myRewardData.image);
        Sprite sprite = MyUtils.GetSpriteFromTexture(tex);
        _rewardImg.sprite = sprite;
        Texture2D tex1 = await MyUtils.GetTextureFromUrl(_myRewardData.currency_image);
        Sprite sprite1 = MyUtils.GetSpriteFromTexture(tex1);
        _currencyImg.sprite = sprite1;
    }
    public void OnClickClaim()
    {
        ScreenManager.Instance.ChangeScreen(Window.CollectRewardsPopup, ScreenType.Additive, false);
        MyUtils.DelayedCallback(.75f, _UpdateState, Status.cooling);
    }
    public void DeductCurrency(int currency)
    {

        if (_status == Status.cooling || _status == Status.claim) return;
        _currencyCount -= currency;
        if (_currencyCount <= 0)
        {
            _currencyCount = 0;
            if (_goStateTimer.IsTimerCompleted)
                _UpdateState(Status.claim);
        }
        _currencyCountTxt.SetText(_currencyCount.ToString());
    }
    #endregion Public Methods

    #region Private Methods
    private void _HandleCoolDownState()
    {
        _coolDownStatePanel.SetActive(true);
        _uiEffect.effectMode = EffectMode.Grayscale;
        _rewardBg.gameObject.SetActive(false);
        _cooldownTimer.InitTimer(_myRewardData.award_every_minutes * 60, _myRewardData.cool_down_minutes_passed * 60);
        _cooldownTimer.StartTimer();
        _cooldownTimer.OnTimerTick += _UpdateFillBar;
        _cooldownTimer.OnTimerComplete += OnCooldownTimerComplete;

    }
    void OnCooldownTimerComplete()
    {
        _cooldownTimer.OnTimerTick -= _UpdateFillBar;
        _UpdateState(Status.go);
    }
    private void _UpdateFillBar()
    {

        float fillAmount = (float)_cooldownTimer.GetElapsedTimeInSeconds() / (float)(_myRewardData.award_every_minutes * 60);
        _cooldownTimerProgressbar.fillAmount = fillAmount;
        if (_cooldownTimer.GetRemaingTimeInSeconds() % 2 == 0)
        {
            _rewardImgEuler = _rewardImg.transform.eulerAngles;
            _rewardImgEuler.z -= 90;
            _rewardImg.transform.DORotate(_rewardImgEuler, .75f);
        }
    }
    private void _HandleGoState()
    {
        _goStatePanel.SetActive(true);
        if (_myRewardData.curr_earned <= 0)
            _rewardBg.sprite = _rewardSpriteDatabase.GoStateNoCurrencyBG;
        else
            _rewardImg.transform.DOScale(0.9f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease
                .Linear);
        _goStateTimer.InitTimerAndStartTimer(_myRewardData.minimum_connection_minutes * 60 - _myRewardData.loggedin_seconds);
        _goStateTimer.OnTimerComplete += _OnGoStateTimerCompleted;
        _currencyCount = _myRewardData.currency_required - _myRewardData.curr_earned;
        _currencyCountTxt.SetText(_currencyCount.ToString());
    }
    private void _OnGoStateTimerCompleted()
    {
        if (_currencyCount <= 0)
            _UpdateState(Status.claim);
    }

    private void _HandleClaimState()
    {
        _claimStatePanel.SetActive(true);
        _rewardImg.transform.DOKill();
        _rewardImg.transform.DOMoveY(_rewardImgHopRefPose.position.y, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);


    }
    private void _UpdateState(Status status)
    {
        _status = status;
        _coolDownStatePanel.SetActive(false);
        _goStatePanel.SetActive(false);
        _claimStatePanel.SetActive(false);
        DOTween.Kill(_rewardImg.transform);
        _rewardBg.sprite = _rewardSpriteDatabase.GetRewardItemBG(_status);
        switch (_status)
        {
            case Status.claim:
                _HandleClaimState();
                break;
            case Status.go:
                _HandleGoState();
                break;
            case Status.cooling:
                _HandleCoolDownState();
                break;
            default:
                MyUtils.Log($"Default Case::RewardItem_Init()");
                break;
        }
    }
    #endregion Private Methods

    #region Callbacks

    #endregion Callbacks
}
