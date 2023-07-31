using Ruinum.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMove : MonoBehaviour
{
    public Vector3[] points; // ������ ����� ��� ������
    public float speed = 1f; // �������� ��������
    private int currentPoint = 1; // ������� ����� � �������
    private float progress = 0f; // �������� �������� ����� �������

    public void Update()
    {
        // ���� �������� ��������� �����, �������� �������
        if (currentPoint >= points.Length)
        {
            currentPoint = 1;
        }

        // ���������� ������ � ��������� �����
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(points[currentPoint - 1], points[currentPoint], progress);

        // ���� �������� ��������� �����, ��������� � ���������
        if (progress >= 1f)
        {
            currentPoint++;
            progress = 0f;
        }
    }

}
