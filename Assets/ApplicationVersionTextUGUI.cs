using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApplicationVersionTextUGUI : MonoBehaviour
{
    public string format;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = string.Format(format, Application.version);
    }
}
