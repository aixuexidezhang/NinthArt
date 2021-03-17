using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mr_T;
using ARPG.Charater;

namespace ARPG.Skill
{
    [SerializeField]
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class ChacracterSkillManager : MonoBehaviour
    {
        //技能列表
        public SkillData[] skills;

        private void Start()
        {
            for (int i = 0; i < skills.Length; i++)
                InitSkill(skills[i]);
        }

        /// <summary>
        /// 技能初始化
        /// </summary>
        /// <param name="data"></param>
        private void InitSkill(SkillData data)
        {
            /*
             * 资源映射表
             * 资源名称----->资源完整路径
             */


            //data.prefab -->data.skillPrefab
            //根据资源名称获取资源预制件
            data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);
            data.owner = gameObject;
        }

        //技能释放条件:冷却、法力
        public SkillData PrepareSkill(int id)
        {
            //根据ID查找技能数据
            SkillData skillData = FindSkill(id);


            //获取当前角色法力
            float sp = GetComponent<CharacterStatus>().SP;

            //判断条件返回数据---->技能不为空、技能冷却时间为0、蓝量够
            if (skillData != null && skillData.coolRemain <= 0 && skillData.costSP <= sp)
                return skillData;
            else
                return null;
        }

        /// <summary>
        /// 根据编号查询技能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private SkillData FindSkill(int id)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i].skillID == id)
                {
                    return skills[i];
                }
            }
            return null;
        }

        //生成技能
        public void GenerateSkill(SkillData data)
        {
            //实例化一个技能
            // GameObject skillGo = Instantiate(data.skillPrefab, transform.position, transform.rotation);
            GameObject skillGo = GameObjectPool.Instance.CreatObject(data.skillName, data.skillPrefab, transform.position, transform.rotation);

            //传递技能数据
            SkillDeployer deployer = skillGo.GetComponent<SkillDeployer>();
            deployer.SkillData = data;

            //根据技能持续时间然后销毁
            //    Destroy(skillGo, data.durationTime);
            GameObjectPool.Instance.CollectObject(skillGo, data.durationTime);
            //开启技能冷却
            StartCoroutine(CoolTimeDown(data));
        }

        /// <summary>
        /// 技能倒计时
        /// </summary>
        private IEnumerator CoolTimeDown(SkillData data)
        {
            //技能冷却时间赋值给技能剩余冷却时间
            data.coolRemain = data.coolTime;
            //当技能剩余冷却时间比1大的时候,每秒钟减少一秒冷却时间
            while (data.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
        }
    }
}