using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Skill
{
    /// <summary>
    /// 释放器配置工厂
    /// 提供创建释放器各种算法对象的功能
    /// </summary>
    public class DeployerConfigFactor
    {
        public static IAttackSelector CreatAttackSelector(SkillData data)
        {
            //选区
            string classNameSelector = string.Format("ARPG.Skill.{0}AttackSelector", data.selectorType);
            return CreatObject<IAttackSelector>(classNameSelector);
        }

        public static IImpactEffect[] CreatImpactEffect(SkillData data)
        {
            IImpactEffect[] iImpactEffects = new IImpactEffect[data.impactType.Length];

            //影响
            string classNameEffect = string.Format("ARPG.Skill.{0}Impact", data.impactType);
            for (int i = 0; i < data.impactType.Length; i++)
            {
                iImpactEffects[i] = CreatObject<IImpactEffect>(classNameEffect);
            }
            return iImpactEffects;
        }

        private static T CreatObject<T>(string className) where T : class
        {
            Type type = Type.GetType(className);
            return Activator.CreateInstance(type) as T;
        }
    }
}