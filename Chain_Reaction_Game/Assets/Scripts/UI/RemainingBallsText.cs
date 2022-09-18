using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingBallsText : MonoBehaviour {
    private readonly string text = "Remaining: ";

    void FixedUpdate() {
        GetComponent<TMP_Text>().text = text + LevelData.level1RemainingBalls;
    }
}
