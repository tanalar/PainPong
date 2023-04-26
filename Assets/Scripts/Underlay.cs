using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Underlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI underlay;
    [SerializeField] private List<Image> images;
    private float step = 0.00075f;
    private float x;
    private float y;
    private float max = 0.06f;
    private float min = -0.06f;
    private bool xySwitcher = true;
    private bool xSwitcher = true;
    private bool ySwitcher = true;

    private float currentR;
    private float currentG;
    private float currentB;
    private float rFrom = 0.5019608f;
    private float gFrom = 1;
    private float bFrom = 0.8588235f;
    private float rTo = 0.9490196f;
    private float gTo = 0;
    private float bTo = 0.5372549f;
    private float currentColor = 0;
    private bool colorSwitcher = true;

    private void OnEnable()
    {
        StartCoroutine(Delay());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Delay()
    {
        UnderlayMover();
        ColorSwitcher();

        yield return new WaitForSeconds(0.0075f);
        StartCoroutine(Delay());
    }

    private void UnderlayMover()
    {
        underlay.text = text.text;

        if (xySwitcher)
        {
            if (xSwitcher)
            {
                x += step;
                if (x >= max)
                {
                    xSwitcher = false;
                    xySwitcher = false;
                }
            }
            if (!xSwitcher)
            {
                x -= step;
                if (x <= min)
                {
                    xSwitcher = true;
                    xySwitcher = false;
                }
            }
        }
        if (!xySwitcher)
        {
            if (ySwitcher)
            {
                y += step;
                if (y >= max)
                {
                    ySwitcher = false;
                    xySwitcher = true;
                }
            }
            if (!ySwitcher)
            {
                y -= step;
                if (y <= min)
                {
                    ySwitcher = true;
                    xySwitcher = true;
                }
            }
        }
        underlay.rectTransform.position = new Vector3(text.rectTransform.position.x + x, text.rectTransform.position.y + y, 0);
    }

    private void ColorSwitcher()
    {
        if (colorSwitcher)
        {
            currentColor++;
            if (currentColor >= 100)
            {
                colorSwitcher = false;
            }
        }
        if (!colorSwitcher)
        {
            currentColor--;
            if (currentColor <= 0)
            {
                colorSwitcher = true;
            }
        }
        currentR = rFrom - ((rFrom - rTo) / 100 * (100 - currentColor));
        currentG = gFrom - ((gFrom - gTo) / 100 * (100 - currentColor));
        currentB = bFrom - ((bFrom - bTo) / 100 * (100 - currentColor));
        underlay.color = new Color(currentR, currentG, currentB);
        foreach (var item in images)
        {
            item.color = underlay.color;
        }
    }
}
