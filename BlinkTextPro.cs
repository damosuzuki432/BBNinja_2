using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkTextPro : MonoBehaviour
{
    /// <summary>
    /// attach to textMeshPro that you want to blink
    /// </summary>


    TextMeshProUGUI tmProUGUI;
    [Range(0.1f,0.5f)][SerializeField] float blinkSpeed = 0.5f;

    private void Start()
    {
        tmProUGUI = GetComponent<TextMeshProUGUI>();
        StartBlinking();
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (tmProUGUI.color.a.ToString())
            {
                case "0":
                    tmProUGUI.color = new Color(tmProUGUI.color.r, tmProUGUI.color.g, tmProUGUI.color.b, 1);
                    yield return new WaitForSeconds(blinkSpeed);
                    break;
                case "1":
                    tmProUGUI.color = new Color(tmProUGUI.color.r, tmProUGUI.color.g, tmProUGUI.color.b, 0);
                    yield return new WaitForSeconds(blinkSpeed);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}