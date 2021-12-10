using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            UserDataManager.Save(true, "Manual save progress..");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
