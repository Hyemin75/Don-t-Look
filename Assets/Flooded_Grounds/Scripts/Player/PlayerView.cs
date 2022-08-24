using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerView : MonoBehaviour
{
    public Transform enemy;

    public float angleRange = 45f;
    public float distance = 5f;
    public bool isCollision = false;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    Vector3 direction;
    float dotValue = -0f;

    private void Update()
    {
        dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange/2));
        direction = enemy.position - transform.position;

        if(direction.magnitude < distance)
        {
            if(Vector3.Dot(direction.normalized, transform.transform.forward) > dotValue)
            {
               
                isCollision = true;
            }
            else
            {
                isCollision = false;
            }
        }
        else
        {
            isCollision = false;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _blue : _red;
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange/2, distance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);
    }

}
