using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ссылка на объект, за которым камера должна следовать
    public float smoothSpeed = 1.0f; // Скорость плавного движения камеры

    public Vector3 addvector = new Vector3(0f, 0f, 0f);
    public Vector3 addoffset;

    //public Quaternion addRotation = new Quaternion(30f, 0f, 0f, 0f);

    private Vector3 offset;
    private Quaternion initialRotation;
    private void Start()
    {
        // Вычисляем начальное смещение между камерой и целевым объектом
        offset = transform.position - target.position;
        initialRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = target.rotation*initialRotation;
        // Вычисляем позицию, к которой должна двигаться камера
        Vector3 desiredPosition = target.position + target.rotation*(offset+addoffset);

        // Плавно перемещаем камеру к желаемой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //smoothedPosition += addvector;
        transform.position = smoothedPosition;
        
    }
}
