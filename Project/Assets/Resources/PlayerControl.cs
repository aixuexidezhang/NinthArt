using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	RaycastHit hit;
	Camera cm;
	Ray ray;
	bool move;
	Vector3 movePoint;
    public float speed;
    public Animator am;
    public Rigidbody rb;
    Coroutine C;
    bool roll;
    private void Awake()
	{
		cm = Camera.main;
	}
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = cm.ScreenPointToRay(Input.mousePosition);
            var value = Physics.Raycast(ray, out hit);
            if (value)
            {
                movePoint = hit.point;
				if (C==null)
				{
                    C = StartCoroutine(Torsion(Quaternion.AngleAxis(GetAngle(transform.position, hit.point), Vector3.up)));
				}
				else
				{
                    StopCoroutine(C);
                    C = StartCoroutine(Torsion(Quaternion.AngleAxis(GetAngle(transform.position, hit.point), Vector3.up)));
                }
            }
        }
		if (Input.GetKeyDown(KeyCode.Q))
		{
            roll = true;
        }

        IEnumerator Torsion(Quaternion target)
        {
            float t = 0;
            Quaternion nowQ = transform.rotation;
            while (t<1)
            {
                t += Time.deltaTime*10f;
                transform.rotation = Quaternion.Lerp(nowQ, target, t);
				yield return null;
            }
            move = true;
        }

    }
	private void FixedUpdate()
	{
        if (move)
        {
            transform.Translate(Vector3.forward * speed * .1f);
            move = Vector3.Distance(transform.position, movePoint) > 2f;
        }
        am.SetBool("Run",move);
		if (roll)
		{
            am.SetTrigger("Roll");
            roll = false;
        }
    }
	private float GetAngle(Vector3 a, Vector3 target)
    {
        target.x -= a.x;
        target.z -= a.z;
        float deltaAngle = 0;
        if (target.x == 0 && target.z == 0)
        {
            return 0;
        }
        else if (target.x > 0 && target.z > 0)
        {
            deltaAngle = 0;
        }
        else if (target.x > 0 && target.z == 0)
        {
            return 90;
        }
        else if (target.x > 0 && target.z < 0)
        {
            deltaAngle = 180;
        }
        else if (target.x == 0 && target.z < 0)
        {
            return 180;
        }
        else if (target.x < 0 && target.z < 0)
        {
            deltaAngle = -180;
        }
        else if (target.x < 0 && target.z == 0)
        {
            return -90;
        }
        else if (target.x < 0 && target.z > 0)
        {
            deltaAngle = 0;
        }
        return Mathf.Atan(target.x / target.z) * Mathf.Rad2Deg + deltaAngle;
    }
}
