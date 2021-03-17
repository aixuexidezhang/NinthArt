using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Skill
{
    /// <summary>
    /// 技能释放器
    /// </summary>
    public class SkillDeployer : MonoBehaviour
    {
        private SkillData skillData;
        /// <summary>
        /// 由技能管理器提供
        /// </summary>
        public SkillData SkillData
        {
            get { return skillData; }
            set
            {
                skillData = value;
                //创建算法对象
                //    InitDeplopyer();
            }
        }

        //选区算法对象
        public IAttackSelector selector;

        //影响效果对象
        public IImpactEffect[] effectArray;

        //初始化释放器
        private void InitDeplopyer()
        {
            //创建算法对象

            //选区
            selector = DeployerConfigFactor.CreatAttackSelector(SkillData);
            //影响
            effectArray = DeployerConfigFactor.CreatImpactEffect(SkillData);

        }



        //执行算法对象

        //释放方式
    }
}