using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    [Header("Точки маршрута")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    
    [Header("Настройки движения")]
    [SerializeField] public float speed = 0.1f; // Уменьшил скорость по умолчанию
    [SerializeField] public AnimationCurve trajectoryCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    private float progress = 0f;
    private bool movingToB = true;
    
    private void Start()
    {
        // Ставим объект в точку A при старте
        transform.position = pointA.position;
    }
    
    private void Update()
{
    if (progress < 1f)
    {
        progress += Time.deltaTime * speed;
        float curveValue = trajectoryCurve.Evaluate(progress);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, curveValue);
    }
}
}