using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using System;

public static class UserDataManager
{
    private const string PROGRESS_KEY = "Progress";
    public static UserProgressData Progress;
    public static bool loadedFromCloud = false;

    public static void LoadFromLocal() {
        if (!PlayerPrefs.HasKey(PROGRESS_KEY)) {
            Progress = new UserProgressData();
            Save();
        }
        else {
            string json = PlayerPrefs.GetString(PROGRESS_KEY);
            Progress = JsonUtility.FromJson<UserProgressData>(json);
        }
    }

    public static void Save(bool uploadToCloud = false) {
        string json = JsonUtility.ToJson(Progress);
        PlayerPrefs.SetString(PROGRESS_KEY, json);
    }

    public static bool HasResources(int index) {
        return index < Progress.ResourceLevels.Count;
    }
}
