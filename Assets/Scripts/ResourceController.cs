using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public Button ResourceButton;
    public Image ResourceImage;
    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;
    private GameManager.ResourceConfig _config;
    private int _index;
    private int _level {
        set {
            UserDataManager.Progress.ResourceLevels[_index] = value;
            UserDataManager.Save(true);
        }
        get {
            if (!UserDataManager.HasResources(_index)) {
                return 1;
            }

            return UserDataManager.Progress.ResourceLevels[_index];
        }
    }

    public bool IsUnlocked { get; private set; }

    private void Start() {
        ResourceButton.onClick.AddListener(() => {
            if (IsUnlocked)
                UpgradeLevel();
            else
                UnlockResource();
        });
    }

    public void UpgradeLevel() {
        double upgradeCost = GetUpgradeCost();

        if (GameManager.Instance.TotalGold < upgradeCost)
            return;

        GameManager.Instance.AddGold(-upgradeCost);

        _level++;

        ResourceUpgradeCost.text = $"Upgrade Cost\n{ GetUpgradeCost() }";
        ResourceDescription.text = $"{ _config.Name } Lv. { _level }\n+{ GetOutput().ToString("0") }";

        AnalyticsManager.LogUpgradeEvent(_index, _level);
    }

    public void SetConfig(int index, GameManager.ResourceConfig config) {
        _index = index;
        _config = config;
        ResourceDescription.text = $"{ _config.Name } Lv. { _level }\n+{ GetOutput().ToString("0") }";
        ResourceUnlockCost.text = $"Unlock Cost\n{ _config.UnlockCost }";
        ResourceUpgradeCost.text = $"Upgrade Cost\n{ GetUpgradeCost() }";

        SetUnlocked((_config.UnlockCost == 0) || (UserDataManager.HasResources(_index)));
    }

    public double GetOutput() {
        return _config.Output * _level;
    }

    public double GetUpgradeCost() {
        return _config.UpgradeCost * _level;
    }

    public double GetUnlockCost() {
        return _config.UnlockCost;
    }

    public void UnlockResource() {
        double unlockCost = GetUnlockCost();
        if (GameManager.Instance.TotalGold < unlockCost)
            return;
        SetUnlocked(true);
        GameManager.Instance.ShowNextResource();

        AchievementController.Instance.UnlockAchievement(AchievementController.AchievementType.UnlockResource, _config.Name);
        AnalyticsManager.LogUnlockEvent(_index);
    }

    public void SetUnlocked(bool unlocked) {
        IsUnlocked = unlocked;

        if (unlocked) {
            if (!UserDataManager.HasResources(_index)) {
                UserDataManager.Progress.ResourceLevels.Add(_level);
                UserDataManager.Save(true);
            }
        }

        ResourceImage.color = IsUnlocked ? Color.white : Color.grey;
        ResourceUnlockCost.gameObject.SetActive(!unlocked);
        ResourceUpgradeCost.gameObject.SetActive(unlocked);
    }
}
