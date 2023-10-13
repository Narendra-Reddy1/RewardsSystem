using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
    #region Varibales
    [SerializeField] private Image _rewardBg;
    [SerializeField] private Image _rewardImg;

    [Space(10)]
    [Header("CoolDown")]
    [SerializeField] private GameObject _coolDownStatePanel;
    [SerializeField] private TextMeshProUGUI _cooldownTimerTxt;
    [SerializeField] private Image _cooldownTimerProgressbar;

    [Space(10)]
    [Header("GoState")]
    [SerializeField] private GameObject _goStatePanel;
    [SerializeField] private TextMeshProUGUI _claimTimerTxt;
    [SerializeField] private TextMeshProUGUI _currencyCountTxt;
    [SerializeField] private Image _currencyImg;


    [Space(10)]
    [Header("Claim State")]
    [SerializeField] private GameObject _claimStatePanel;


    private Status _status;
    #endregion Varibales

    #region Unity Methods
    #endregion Unity Methods

    #region Public Methods
    public async void Init(Reward reward)
    {
        _status = System.Enum.Parse<Status>(reward.status, true);
        var tex = await MyUtils.GetTextureFromUrl(reward.image);
        Sprite sprite = MyUtils.GetSpriteFromTexture(tex);


    }
    #endregion Public Methods

    #region Private Methods
    #endregion Private Methods

    #region Callbacks
    #endregion Callbacks
}
