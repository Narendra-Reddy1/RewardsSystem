using BenStudios.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RewardsPanelPopup : PopupBase
{
    #region Varibales
    [SerializeField] private string _jsonDataApiUrl = "https://epicmindarena.com/inteview_api/get_hourly_rewards_2.json";
    [SerializeField] private AssetReferenceGameObject _rewardItem;
    [SerializeField] private Transform _rewardsParent;

    [SerializeField] RewardData data;
    private List<GameObject> _rewardItemsList = new();
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
    #endregion Public Methods

    #region Private Methods
    private async void _Init()
    {
        string jsonData = await MyUtils.GetJsonDataFromUrl(_jsonDataApiUrl);
        data = MyUtils.GetObjectFromJsonString<RewardData>(jsonData);
        foreach (Reward reward in data.rewards)
        {
            AddressableAssetLoader.Instance.Instantiate(_rewardItem, _rewardsParent, true, (status, handle) =>
            {

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
