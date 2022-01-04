﻿using UnityEngine;
using System.Collections;

public class TypeObjectPattern : MonoBehaviour
{
    /* 通过Json创建新类型，更能体现类型对象的优势
    {
      "Troll": {
        "health": 25,
        "attack": "The troll hits you!"
      },
      "Troll Archer": {
        "parent": "Troll",
        "health": 0,
        "attack": "The troll archer fires an arrow!"
      },
      "Troll Wizard": {
        "parent": "Troll",
        "health": 0,
        "attack": "The troll wizard casts a spell on you!"
      }
    }
    */

    void Start()
    {
        //创建种类，生命值填0表示从父类继承。
        Breed troll = new Breed(null, 25, "The troll hits you!");

        Breed trollArcher = new Breed(troll, 0, "The troll archer fires an arrow!");

        Breed trollWizard = new Breed(troll, 0, "The troll wizard casts a spell on you!");

        //通过种类创建monster对象
        Monster trollMonster = troll.NewMonster();
        trollMonster.ShowAttack();

        Monster trollArcherMonster = trollArcher.NewMonster();
        trollArcherMonster.ShowAttack();

        Monster trollWizardMonster = trollWizard.NewMonster();
        trollWizardMonster.ShowAttack();

    }

}


/// <summary>
/// 品种类Breed
/// </summary>
public class Breed
{
    private int health_;
    private string attack_;
    Breed parent_;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="parent">父类</param>
    /// <param name="health">生命值，填0表示从父类继承</param>
    /// <param name="attack">攻击表现</param>
    public Breed(Breed parent, int health, string attack)
    {
        health_ = health;
        attack_ = attack;
        parent_ = null;

        //复制“代理”，在创建一个类型时将继承的特性复制到类的内部
        //注意我们不再需要parent类中的属性，一旦构造结束，就可以忘掉基类
        if (parent != null)
        {
            parent_ = parent;

            //是0，从父层拿
            if (health == 0)
            {
                health_ = parent.GetHealth();
            }
            //是null，从父层拿
            if (attack == null)
            {
                attack_ = parent.GetAttack();
            }
        }
    }

    public Monster NewMonster()
    {
        return new Monster(this);
    }

    public int GetHealth()
    {
        return health_;
    }

    public string GetAttack()
    {
        return attack_;
    }
}



/// <summary>
/// Monster类“ has a”Breed类
/// </summary>
public class Monster
{
    private int health_;
    private Breed breed_;
    private string attack_;

    public Monster(Breed breed)
    {
        health_ = breed.GetHealth();
        breed_ = breed;
        attack_ = breed.GetAttack();
    }

    public string GetAttack()
    {
        return attack_;
    }

    public void ShowAttack()
    {
        Debug.Log(attack_);
    }
}





