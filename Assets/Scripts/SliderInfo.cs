using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderInfo : MonoBehaviour
{

    public TMP_Text text;
    public Slider slider;
    // Start is called before the first frame update
    public void SetValues(int current, int max)
    {
        text.text = current.ToString() + "/" + max.ToString();
        slider.maxValue = max;
        slider.value = current;

    }
}
