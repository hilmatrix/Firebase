using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private Button _localButton;
    [SerializeField] private Button _cloudButton;

    // Start is called before the first frame update
    void Start() {
        _localButton.onClick.AddListener(() => {
            SetButtonInteractable(false);
            UserDataManager.LoadFromLocal();
            SceneManager.LoadScene(1);
        });

        _cloudButton.onClick.AddListener(() => {
            SetButtonInteractable(false);
            StartCoroutine(UserDataManager.LoadFromCloud(() =>
                SceneManager.LoadScene(1)
            ));
        });
    }

        // Update is called once per frame
        void Update()
    {
        
    }

    private void SetButtonInteractable(bool value) {
        _localButton.interactable = value;
        _cloudButton.interactable = value;
    }
}
