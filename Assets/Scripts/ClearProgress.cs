using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            UserDataManager.Progress.Gold = 0;
            UserDataManager.Progress.ResourceLevels.Clear();
            UserDataManager.Save(true);
            SceneManager.LoadScene(0);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
