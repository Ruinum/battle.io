using Ruinum.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMove : MonoBehaviour
{
    public Vector3[] points; // массив точек для обхода
    public float speed = 1f; // скорость движения
    private int currentPoint = 1; // текущая точка в массиве
    private float progress = 0f; // прогресс движения между точками

    public void Update()
    {
        // если достигли последней точки, начинаем сначала
        if (currentPoint >= points.Length)
        {
            currentPoint = 1;
        }

        // перемещаем объект к следующей точке
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(points[currentPoint - 1], points[currentPoint], progress);

        // если достигли следующей точки, переходим к следующей
        if (progress >= 1f)
        {
            currentPoint++;
            progress = 0f;
        }
    }

}
