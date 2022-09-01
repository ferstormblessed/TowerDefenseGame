using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceController : MonoBehaviour
{
    private int _gold = 100;

    public int Gold
    {
        get => _gold;
        set => _gold = value;
    }

    [SerializeField] private int _startGoldAmount = 100;
    [SerializeField] private int _increaseGoldAmount = 10;
    [SerializeField] private int _increaseGoldDelayTime = 1;
    private int _cost;
    [SerializeField] private UnityEvent<int> OnGoldAmountChange;
    [SerializeField] private ResourceData _resourceData;
    
    private void Start()
    {
        StartCoroutine(IncreaseGoldRoutine());
    }


    IEnumerator IncreaseGoldRoutine()
    {
        while (GameManager.Instance.CurrentGameState == GameManager.GameState.Playing)
        {
            yield return new WaitForSeconds(_increaseGoldDelayTime);
            Gold += _increaseGoldAmount;
            OnGoldAmountChange?.Invoke(_gold);
        }
    }

    public void SubtractWeaponCost(string weaponType)
    {
        switch (weaponType)
        {
            case "Gun":
                _cost = _resourceData.WeaponsCosts[0].WeaponCost; // 0 is gun
                break;
            case "Cannon":
                _cost = _resourceData.WeaponsCosts[1].WeaponCost; // 1 is cannon
                break;
            case "LaserTurret":
                _cost = _resourceData.WeaponsCosts[2].WeaponCost; // 2 is laser turret
                break;
            default:
                _cost = 0;
                break;
        }

        Gold -= _cost;
        OnGoldAmountChange?.Invoke(Gold);
    }
}
