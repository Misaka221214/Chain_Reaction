using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabDict
{
    public static Dictionary<ObstacleEnum, string> prefabDict = new() {
        { ObstacleEnum.BASIC, "Prefabs/Obstacles/Basic Circle Obstacle" },
        { ObstacleEnum.BOOST, "Prefabs/Obstacles/Boost Circle Obstacle" },
        { ObstacleEnum.ENLARGE, "Prefabs/Obstacles/Enlarge Circle Obstacle" },
        { ObstacleEnum.BOUNCY, "Prefabs/Obstacles/Bouncy Circle Obstacle" },
    };
}
