using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingBallsText : MonoBehaviour {
    private readonly string text = "Remaining: ";

    public LevelEnum level;

    void FixedUpdate() {
        switch (level) {
            case LevelEnum.LEVEL_1:
                GetComponent<TMP_Text>().text = text + LevelData.level1RemainingBalls;
                break;
            case LevelEnum.LEVEL_2:
                GetComponent<TMP_Text>().text = text + LevelData.level2RemainingBalls;
                break;
            case LevelEnum.LEVEL_3:
                GetComponent<TMP_Text>().text = text + LevelData.level3RemainingBalls;
                break;
            default:
                break;
        }
    }
}
