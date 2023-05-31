using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image expBarSprite;
    [SerializeField] private float reduceSpeed = 2f;
    private float target = 1f;
    public void UpDateExpBar(float realExp, float exp)
    {
        target = exp / realExp;
    }

    private void LateUpdate()
    {
        expBarSprite.fillAmount = Mathf.MoveTowards(expBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
