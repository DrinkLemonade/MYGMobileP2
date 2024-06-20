using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Unity.UIElements;
using System;
public class EnemyTest
{
    
    [Test]
    public void DoTest()
    {
        //Arrange
        GameObject go = new();
        EnemyScript enemy = go.AddComponent<EnemyScript>();
        enemy.health = 100f;
        float initialHealth = enemy.health;

        //Act
        enemy.TakeDamage(10f);

        //Assert
        Assert.Greater(initialHealth, enemy.health);
        Assert.IsTrue(enemy.health == 90f);
    }
    
}
