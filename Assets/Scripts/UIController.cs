using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private Button[] _weaponButtons;
    [SerializeField] private TMP_Text _goldAmountText;
    [SerializeField] private ResourceData _resourceData;
    
    public void ShowGameOverScreen()
    {
        if (GameManager.Instance.Winner)
        {
            _winScreen.SetActive(true);
            
        }
        else
        {
            _loseScreen.SetActive(true);
        }
    }

    public void SliderValueChange(float speed)
    {
        GameManager.Instance.GameSpeed = speed;
    }

    public void RetryGame(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }

    public void CheckIfEnoughGoldForWeapon(int currentGoldAmount)
    {
        for (int i = 0; i < 3; i++)
        {
            _weaponButtons[i].interactable = false;
        }

        for (int i = _weaponButtons.Length; i > 0; i--)
        {
            if (currentGoldAmount >= _resourceData.WeaponsCosts[i - 1].WeaponCost)
            {
                _weaponButtons[i - 1].interactable = true;
            }
        }
        
        // if (currentGoldAmount >= _resourceData.WeaponsCosts[2].WeaponCost) // 2 is laser turret
        // {
        //     for (int i = 0; i < 3; i++)
        //     {
        //         _weaponButtons[i].interactable = true;
        //     }
        // }
        // else if (currentGoldAmount >= _resourceData.WeaponsCosts[1].WeaponCost) // 1 is cannon
        // {
        //     for (int i = 0; i < 2; i++)
        //     {
        //         _weaponButtons[i].interactable = true;
        //     }
        // }
        // else if (currentGoldAmount >= _resourceData.WeaponsCosts[0].WeaponCost) // 0 is gun
        // {
        //     _weaponButtons[0].interactable = true;
        // }
    }

    public void UpdateGoldAmountUI(int currentGoldAmount)
    {
        _goldAmountText.text = "Gold: " + currentGoldAmount.ToString();
    }
}
