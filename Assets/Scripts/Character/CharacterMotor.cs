using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Charater
{
    /// <summary>
    /// 角色马达:负责控制角色运动
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        private CharacterController controller; //角色控制器
        [Tooltip("旋转速度")]
        public float rotateSpeed = 10;
        [Tooltip("移动速度")]
        public float moveSpeed = 10;
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        //旋转
        public void LookAtTarget(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            Quaternion rotDir = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotDir, Time.deltaTime * rotateSpeed);
            Debug.Log(transform.localEulerAngles);
        }

        //移动
        public void Movement(Vector3 direction)
        {
            //注视方向旋转
            LookAtTarget(direction);
            //移动
            if (direction != Vector3.zero)
            {
                //Vector3 dirPos = transform.position + direction;
                //transform.position = Vector3.Lerp(transform.position, dirPos, Time.deltaTime * moveSpeed);
                controller.Move(direction * Time.deltaTime * moveSpeed);
                //   transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
            }
        }
    }
}