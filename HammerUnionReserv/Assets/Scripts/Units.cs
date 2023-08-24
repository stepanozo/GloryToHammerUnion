using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assembly_CSharp
{
    public class unit
    {
        public string name;
        public int damage;
        public int techDamage;
        public int HP;
        public string spritePath;
        public string description;
        public int quantity;

        public unit(string name, int damage, int techDamage, int hP, string spritePath, string description, int quantity)
        {
            this.name = name;
            this.damage = damage;
            this.techDamage = techDamage;
            HP = hP;
            this.spritePath = spritePath;
            this.description = description;
            this.quantity = quantity;
        }
    }
}