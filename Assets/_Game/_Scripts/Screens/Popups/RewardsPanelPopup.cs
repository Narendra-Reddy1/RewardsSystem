using BenStudios.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RewardsPanelPopup : PopupBase
{
    #region Varibales
    [SerializeField] private AssetReferenceGameObject _rewardItem;
    [SerializeField] private Transform _rewardsParent;
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
    private void _Init()
    {
        AddressableAssetLoader.Instance.Instantiate(_rewardItem, _rewardsParent, true, null);
    }

    #endregion Private Methods

    #region Callbacks


    #endregion Callbacks
}
