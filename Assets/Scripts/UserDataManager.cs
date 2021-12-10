using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Firebase.Storage;
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

    public static void Save(bool uploadToCloud = false, string textInfo = "Autosave progress..") {
        string json = JsonUtility.ToJson(Progress);
        PlayerPrefs.SetString(PROGRESS_KEY, json);
        DebugText.setText(textInfo);

        if (uploadToCloud) {
            AnalyticsManager.SetUserProperties("gold", Progress.Gold.ToString());
            byte[] data = Encoding.Default.GetBytes(json);
            StorageReference targetStorage = GetTargetCloudStorage();
            targetStorage.PutBytesAsync(data).ContinueWith((Task<StorageMetadata> task) => {
                if (task.IsFaulted || task.IsCanceled) {
                    DebugText.setText("Failed to save progres.");
                }
                else {
                    // Metadata contains file metadata such as size, content-type, and download URL.
                    DebugText.setText("Progress Saved ! (cloud)");
                }
            });
        } else {
            DebugText.setText("Progress Saved ! (local)");
        }
    }

    public static bool HasResources(int index) {
        return index < Progress.ResourceLevels.Count;
    }

    public static IEnumerator LoadFromCloud(System.Action onComplete) {
        bool isCompleted = false;
        bool isSuccesful = false;
        StorageReference targetStorage = GetTargetCloudStorage();

        if (targetStorage != null) {
            const long maxAllowedSize = 1024 * 1024;
            targetStorage.GetBytesAsync(maxAllowedSize).ContinueWith
                ((Task<byte[]> task) => {
                    if (!task.IsFaulted) {
                        string json = Encoding.Default.GetString(task.Result);
                        Progress = JsonUtility.FromJson<UserProgressData>(json);
                        isSuccesful = true;
                    }
                    isCompleted = true;
                });
            while (!isCompleted) {
                yield return null;
            }
        }

        if (isSuccesful) {
            Save();
            loadedFromCloud = true;
        }
        else {
            LoadFromLocal();
        }

        onComplete?.Invoke();
    }

    private static StorageReference GetTargetCloudStorage() {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference sr = null;
        try {
            sr = storage.GetReferenceFromUrl($"{storage.RootReference}/{deviceID}");
        } catch(Exception e) {
            //DebugText.setText("GetTargetCloudStorage error " + e.ToString() );
        }
        
        return sr;
    }
}
