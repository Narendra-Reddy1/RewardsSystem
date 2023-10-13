using BenStudios.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RewardsPanelPopup : PopupBase
{
    #region Varibales
    [SerializeField] private string _jsonDataApiUrl = "https://epicmindarena.com/inteview_api/get_hourly_rewards_2.json";
    [SerializeField] private AssetReferenceGameObject _rewardItem;
    [SerializeField] private Transform _rewardsParent;
    [SerializeField] private SpriteDatabase _rewardSpriteDatabase;
    // [SerializeField] private TextMeshProUGUI _subtractBtnTxt;
    [SerializeField] RewardData data;
    private List<RewardItem> _rewardItemsList = new();

    private const int CURRENCY_TO_DEDUCT = 5;
    #endregion Varibales

    #region Unity Methods

    private void Start()
    {
        _Init();
    }
    #endregion Unity Methods

    #region Public Methods

    public override void OnCloseClick()
    {
        ScreenManager.Instance.CloseLastAdditiveScreen();
    }
    public void OnClickSubtractCurrency()
    {
        for (int i = 0, count = _rewardItemsList.Count; i < count; i++)
        {
            _rewardItemsList[i].DeductCurrency(CURRENCY_TO_DEDUCT);
        }
    }
    #endregion Public Methods

    #region Private Methods
    private async void _Init()
    {
        // _subtractBtnTxt.SetText($"Subtract {CURRENCY_TO_DEDUCT} currencies");
        string jsonData = await MyUtils.GetJsonDataFromUrl(_jsonDataApiUrl);
        data = MyUtils.GetObjectFromJsonString<RewardData>(jsonData);
        foreach (Reward reward in data.rewards)
        {
            AddressableAssetLoader.Instance.Instantiate(_rewardItem, _rewardsParent, true, (status, handle) =>
            {
                GameObject item = handle.Result;
                RewardItem rewardItem = item.GetComponent<RewardItem>();
                rewardItem.Init(reward);
                _rewardItemsList.Add(rewardItem);
            });
        }
    }

    #endregion Private Methods

    #region Callbacks

    #endregion Callbacks
}

[System.Serializable]
public class RewardData
{
    public string status;
    public List<Reward> rewards;
}

[System.Serializable]
public class Reward
{
    public int id;
    public string image;
    public string status;
    public int award_every_minutes;
    public int minimum_connection_minutes;
    public int loggedin_seconds;
    public int curr_earned;
    public int currency_required;
    public string currency_image;
    public int cool_down_minutes_passed;


}
public enum Status
{
    claim,
    go,
    cooling,
    success,
}
