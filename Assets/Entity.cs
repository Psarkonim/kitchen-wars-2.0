using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class Entity
    {
        [SerializeField] public float maxHp;
        [SerializeField] public float curHp;
        [SerializeField] public float attackStrength;
        [SerializeField] public float attackSpeed;
        Rigidbody2D rb;
    }
}
