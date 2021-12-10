using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    static Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
        textComponent.text = UserDataManager.loadedFromCloud ? "Loaded from cloud" : "Loaded from local";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setText(string _text) {
        if (textComponent != null) {
            textComponent.text = _text;
        }
    }
}
