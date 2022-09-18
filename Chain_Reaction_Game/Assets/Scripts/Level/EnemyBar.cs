using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBar : MonoBehaviour
{
    public int health = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(2);
        if (health <= 0)
        {
            LevelProgressionManager.Instance.HandleOnLevelPassed();
        }
    }

    public void TakeDamage(int val)
    {
        health = Mathf.Max(0, health - val);
    }
}
