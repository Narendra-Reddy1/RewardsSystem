using BenStudios.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyManger : MonoBehaviour
{
    private void Start()
    {
        ScreenManager.Instance.ChangeScreen(Window.OpenRewardsScreen);
    }
}
