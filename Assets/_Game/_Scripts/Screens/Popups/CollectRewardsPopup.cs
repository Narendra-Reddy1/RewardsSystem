using BenStudios.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectRewardsPopup : PopupBase
{
    #region Varibales
    public static UnityAction OnRewardsCollected = default;
    #endregion Varibales

    #region Unity Methods
    #endregion Unity Methods

    #region Public Methods
    public override void OnCloseClick()
    {
        OnRewardsCollected?.Invoke();
        OnRewardsCollected = null;
        ScreenManager.Instance.CloseLastAdditiveScreen();
    }
    #endregion Public Methods

    #region Private Methods
    #endregion Private Methods

    #region Callbacks
    #endregion Callbacks
}
struct ClaimRewardsData
{
    //Just a dummy struct 
    //to showcase the possibility to send the required data
    //to show on the collectRewardsScreen.
    //This struct instance is passed to the Init() to fill the data in this popup.
}
