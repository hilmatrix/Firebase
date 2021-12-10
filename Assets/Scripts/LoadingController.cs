using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start() {
        UserDataManager.LoadFromLocal();
        SceneManager.LoadScene(1);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
