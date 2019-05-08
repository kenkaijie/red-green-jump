using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompanyNameTextUGUI : MonoBehaviour
{
    public string format;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = string.Format(format, Application.companyName);
    }
}
