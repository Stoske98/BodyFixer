using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Slider barSlider;
    public Gradient gradient;
    public Image fillImage;
    private int fill = 0;
    private int currentFill;
    private int desiredFill;
    private int difference;
    public float timer = 1;
    private float time = 0;
    void Start()
    {
        barSlider = GetComponent<Slider>();
    }

    public void SetBar(int number)
    {
        desiredFill = number;
        fill = (int)barSlider.value;
        currentFill = fill;
        difference = desiredFill - fill;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(desiredFill != currentFill)
        {
            time += Time.deltaTime;
            barSlider.value += (time / (timer / 100 * difference));
            currentFill = (int)barSlider.value;
            fillImage.color = gradient.Evaluate(barSlider.normalizedValue);
        }
    }
}
