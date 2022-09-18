using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelData {
    public static int activeBalls = 0;

    public static readonly int level1MaxBalls = 5;
    public static readonly int level2MaxBalls = 7;
    public static readonly int level3MaxBalls = 9;

    public static int level1RemainingBalls = 5;
    public static int level2RemainingBalls = 7;
    public static int level3RemainingBalls = 9;

    public static bool level1BossIsDead = false;
    public static bool level2BossIsDead = false;
    public static bool level3BossIsDead = false;

    public static List<ObstacleEnum> playerObstacleList = new() { ObstacleEnum.BOOST, ObstacleEnum.ENLARGE, ObstacleEnum.BOUNCY };

    public static void ResetLevelData() {
        level1RemainingBalls = level1MaxBalls;
        level2RemainingBalls = level2MaxBalls;
        level3RemainingBalls = level3MaxBalls;
    }
}
