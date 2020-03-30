using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadyText : MonoBehaviour
{
    public TextMeshProUGUI readyTextUGUI;

    // Start is called before the first frame update
    void Start()
    {
        readyTextUGUI = GetComponentInChildren<TextMeshProUGUI>();
       
    }

    // Update is called once per frame
    void Update()
    {
        readyTextUGUI.alpha = Mathf.PingPong(1f, 100f);
    }
}
