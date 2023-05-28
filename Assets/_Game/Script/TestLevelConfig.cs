using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelConfig : MonoBehaviour
{
    public AnimationCurve playerLevelCurve;

    private int currentExperience;
    private int maxExperience = 100;

    private int currentLevel;

    private void Start()
    {
        currentLevel = CalculatePlayerLevel();
        //Debug.Log("Player Level: " + currentLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentExperience += 20; // Gán giá trị tùy ý cho lượng kinh nghiệm
            currentLevel = CalculatePlayerLevel();
            Debug.Log(currentExperience);
            Debug.Log("Player Level: " + currentLevel);
        }
    }

    private int CalculatePlayerLevel()
    {
        float progress = (float)currentExperience / maxExperience;
        float level = playerLevelCurve.Evaluate(progress);
        return Mathf.RoundToInt(level);
    }
}
