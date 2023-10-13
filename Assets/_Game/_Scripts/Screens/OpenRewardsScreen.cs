using BenStudios.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRewardsScreen : ScreenBase
{



    public void OnClickOpenRewards()
    {
        ScreenManager.Instance.ChangeScreen(Window.RewardPanelPopup, ScreenType.Additive);


    }


}
