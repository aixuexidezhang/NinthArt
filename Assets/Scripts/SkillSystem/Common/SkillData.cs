using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ARPG.Skill
{
    [Serializable]
    public class SkillData
    {
        /// <summary> 技能ID </summary>
        public int skillID;

        /// <summary> 技能名称 </summary>
        public string skillName;

        /// <summary> 技能描述 </summary>
        public string description;

        /// <summary> 技能剩余 </summary>
        public float coolRemain;

        /// <summary> 冷却时间 </summary>
        public float coolTime;

        /// <summary> 蓝量消耗 </summary>
        public float costSP;

        /// <summary> 攻击距离 </summary>
        public float attackDistance;

        /// <summary> 攻击角度 </summary>
        public float attackAngle;


        /// <summary> 攻击目标 </summary>
        public string[] attackTargetTag = { "Empty" };

        /// <summary> 技能影响类型 </summary>
        public string[] impactType = { "CostSP", "Damage" };

        /// <summary> 连击的下一个技能编号 </summary>
        public int newxBatterId;

        /// <summary> 伤害比率 </summary>
        public float atkRatio;

        /// <summary> 持续时间 </summary>
        public float durationTime;

        /// <summary> 伤害间隔 </summary>
        public int atkInterval;

        [HideInInspector]
        /// <summary> 技能所属 </summary>
        public GameObject owner;

        /// <summary> 技能预制件名字 </summary>
        public string prefabName;

        /// <summary> 技能预制件 </summary>
        public GameObject skillPrefab;

        /// <summary> 动画名称 </summary>
        public string animationName;

        /// <summary> 受击特效名字 </summary>
        public string hitFxName;

        [HideInInspector]
        /// <summary> 受击特效预制件 </summary>
        public GameObject hitFxPrefab;

        /// <summary> 技能等级 </summary>
        public int level;

        /// <summary> 技能攻击类型 单体、群攻 </summary>
        public SkillAttackType attackType;

        /// <summary> 选择类型 扇形、矩形 </summary>
        public SelectorType selectorType;
    }
}