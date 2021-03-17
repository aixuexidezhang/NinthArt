using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Charater
{
    public class CharacterStatus : MonoBehaviour
    {
        /// <summary> 当前生命值 /// </summary>
        public float HP;
        /// <summary> 最大生命值 /// </summary>
        public float maxHP;
        /// <summary> 当前法力值 /// </summary>
        public float SP;
        /// <summary> 最大法力值 /// </summary>
        public float maxSP;
        /// <summary> 攻击力 /// </summary>
        public float baseATK;
        /// <summary> 防御力 /// </summary>
        public float defence;
        /// <summary> 攻击间隔 /// </summary>
        public float attackInterval;
        /// <summary> 攻击距离 /// </summary>
        public float attackDistance;
    }
}