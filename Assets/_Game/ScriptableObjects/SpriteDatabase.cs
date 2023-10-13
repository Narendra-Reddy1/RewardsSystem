using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSpriteDatabase", menuName = "ScriptableObjects/SpriteDatabase")]
public class SpriteDatabase : ScriptableObject
{
    [SerializeField] private SerializedDictionary<Status, Sprite> _rewardItemBgDictionary;
    [SerializeField] private Sprite _goStateNoEarnedCurrencyBg;
    public Sprite GoStateNoCurrencyBG => _goStateNoEarnedCurrencyBg;
    public Sprite GetRewardItemBG(Status status)
    {
        if (_rewardItemBgDictionary.TryGetValue(status, out Sprite bg))
            return bg;
        MyUtils.Log($"NotDefined in SpriteDatabe::{status}");
        return null;
    }
}
