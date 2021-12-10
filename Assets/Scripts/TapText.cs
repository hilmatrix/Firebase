﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapText : MonoBehaviour
{
    public float SpawnTime = 0.5f;
    public Text Text;

    private float _spawnTime;

    private void OnEnable() {
        _spawnTime = SpawnTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        _spawnTime -= Time.unscaledDeltaTime;
        if (_spawnTime <= 0f) {
            gameObject.SetActive(false);
        }
        else {
            Text.CrossFadeAlpha(0f, 0.5f, false);
            if (Text.color.a == 0f) {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        _spawnTime -= Time.unscaledDeltaTime;
        if (_spawnTime <= 0f) {
            gameObject.SetActive(false);
        }
        else {
            Text.CrossFadeAlpha(0f, 0.5f, false);
            if (Text.color.a == 0f) {
                gameObject.SetActive(false);
            }
        }
    }
}
