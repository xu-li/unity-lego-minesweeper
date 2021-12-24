using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.LEGO.Behaviours.Triggers;
using Unity.LEGO.UI;
using UnityEngine;

public class NumberedColumn : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI text;
    
    [SerializeField]
    public InputPrompt inputPrompt;

    private InputTrigger _inputTrigger;

    void OnInputTriggered()
    {
        inputPrompt.gameObject.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _inputTrigger = GetComponentInChildren<InputTrigger>();
        _inputTrigger.OnActivate += OnInputTriggered;
    }

    private void OnDestroy()
    {
        _inputTrigger.OnActivate -= OnInputTriggered;
    }
}
