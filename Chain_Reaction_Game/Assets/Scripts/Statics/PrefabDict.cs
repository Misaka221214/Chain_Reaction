using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabDict
{
    public static Dictionary<ObstacleEnum, string> prefabDict = new() {
        { ObstacleEnum.BASIC, "Prefabs/Obstacles/Basic Circle Obstacle" },
        { ObstacleEnum.BOOST, "Prefabs/Obstacles/Boost Circle Obstacle" },
        { ObstacleEnum.JUMP, "Prefabs/Obstacles/Jump Circle Obstacle" },
        { ObstacleEnum.DAMAGE_MULTIPLY, "Prefabs/Obstacles/Damage Multiply Circle Obstacle" },
        { ObstacleEnum.DAMAGE_ADDITION, "Prefabs/Obstacles/Damage Addition Circle Obstacle" },
        { ObstacleEnum.DUPLICATE, "Prefabs/Obstacles/Duplicate Circle Obstacle" },
        { ObstacleEnum.ENLARGE, "Prefabs/Obstacles/Enlarge Circle Obstacle" },
        { ObstacleEnum.BOUNCY, "Prefabs/Obstacles/Bouncy Circle Obstacle" },
        { ObstacleEnum.HEAVY, "Prefabs/Obstacles/Heavy Circle Obstacle" },
        { ObstacleEnum.TWICE, "Prefabs/Obstacles/Twice Circle Obstacle" },
        { ObstacleEnum.FALL_STRAIGHT, "Prefabs/Obstacles/Fall Straight Circle Obstacle" },
    };
}
