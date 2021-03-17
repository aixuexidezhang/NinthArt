using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Charater;
using ARPG.Skill;

namespace ARPG.Charater
{
    /// <summary>
    /// 角色输入控制器
    /// </summary>
    public class CharacterInputController : MonoBehaviour
    {
        private CharacterMotor chMotor;
        private Vector3 Dir;
        private void Awake()
        {
            chMotor = GetComponent<CharacterMotor>();
        }
        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        private void OnDestroy()
        {

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnSkillButtonDown("按钮1");
            }
            //if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    GameObjectPool.Instance.Clear("普通");
            //}
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Dir = new Vector3(x, 0, y);
            chMotor.Movement(Dir);
        }
        private void FixedUpdate()
        {
         
        }
        private void OnSkillButtonDown(string name)
        {
            int id = 0;
            switch (name)
            {
                case "按钮1":
                    id = 1001;
                    break;
                default:
                    break;
            }
            //获取角色的技能管理器
            ChacracterSkillManager skillManager = GetComponent<ChacracterSkillManager>();
            //根据技能编号准备相对应的技能
            SkillData data = skillManager.PrepareSkill(id);
            //生成技能
            if (data != null)
            {
                skillManager.GenerateSkill(data);
            }
        }

        private void OnMove(Vector2 dir)
        {


        }
    }
}