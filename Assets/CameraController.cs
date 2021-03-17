using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offect;
    public Transform thirdPersonPlayer;    //角色
    void Update()
    {
        //相机应该到的点
        Vector3 targetPos = thirdPersonPlayer.position - transform.position + offect;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2);
    }
}
