using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assembly_CSharp
{
    public class unit
    {
        public string name;
        public int damage;
        public int techDamage;
        public int HP;
        public int maxHP;
        public string spritePath;
        public string description;
        public int quantity;


        public static unit Copy(unit u)
        {
            return new unit(name: u.name, damage: u.damage, techDamage: u.techDamage, hP: u.HP, maxHP: u.maxHP, spritePath: u.spritePath, description: u.description, quantity: u.quantity);
        }

        public unit(string name, int damage, int techDamage, int hP, int maxHP, string spritePath, string description, int quantity)
        {
            this.name = name;
            this.damage = damage;
            this.techDamage = techDamage;
            HP = hP;
            this.maxHP = maxHP;
            this.spritePath = spritePath;
            this.description = description;
            this.quantity = quantity;
        }
    }
}