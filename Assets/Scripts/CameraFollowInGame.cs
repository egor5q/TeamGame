/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowInGame : MonoBehaviour
{
    public Transform target; // Ссылка на объект, за которым камера должна следовать
    public float smoothSpeed = 1.0f; // Скорость плавного движения камеры
    public Vector3 addoffset;
    public Quaternion addRotation;

    public float eulerX;
    public float eulerY;

    private Vector3 offset;
    private Quaternion initialRotation;

    public Quaternion addidionalRotation;
    private void Start()
    {
        // Вычисляем начальное смещение между камерой и целевым объектом
        addoffset = new Vector3(-2.88f, 3.68f, -0.72f);
        addRotation = new Quaternion(23f, 90f, 0f, 0f);
        Debug.Log(addRotation.x);
        Debug.Log(addRotation.y);
        Debug.Log(addRotation.z);
        offset = transform.position - target.position;
        initialRotation = transform.rotation;

        Quaternion addidionalRotation = Quaternion.Euler(23f, 90f, 0);
    }

    private void LateUpdate()
    {
        //addRotation = new Quaternion(11f, 45f, 0f, 0f);
        Debug.Log("AddRotation");
        Debug.Log(addRotation.x);
        Debug.Log(addRotation.y);
        Debug.Log(addRotation.z);

        Debug.Log("AddRotation");
        Debug.Log(initialRotation.x);
        Debug.Log(initialRotation.y);
        Debug.Log(initialRotation.z);
        Quaternion combineRotation = target.rotation*addidionalRotation;
        //transform.rotation = x;//target.rotation;//*initialRotation*addRotation;
        //transform.rotation = Quaternion.AngleAxis(eulerY+x.eulerAngles.y, Vector3.up);
        //transform.rotation = Quaternion.AngleAxis(eulerX+x.eulerAngles.x, Vector3.right);
        transform.rotation = combineRotation;
        Vector3 desiredPosition = target.position + target.rotation*(offset+addoffset);

        // Плавно перемещаем камеру к желаемой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //smoothedPosition += addvector;
        transform.position = smoothedPosition;
        
    }
}
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowInGame : MonoBehaviour
{
    public Transform target; // Ссылка на объект, за которым камера должна следовать
    public float smoothSpeed = 1.0f; // Скорость плавного движения камеры

    public Vector3 addvector = new Vector3(0f, 90f, 0f);
    public Vector3 addoffset;

    public float addrotationY;
    public float addrotationX;
    public float addrotationZ;
    
    

    //public Quaternion addRotation = new Quaternion(30f, 0f, 0f, 0f);

    private Vector3 offset;
    private Quaternion initialRotation;
    private void Start()
    {
        // Вычисляем начальное смещение между камерой и целевым объектом
        offset = transform.position - target.position;
        initialRotation = transform.rotation;
        addoffset = new Vector3(-3.19f, 3.57f, -0.66f);
        addrotationX = -9.04f;

    }

    private void LateUpdate()
    {
        //transform.rotation = target.rotation*initialRotation;
        
        //transform.SetLocalPositionAndRotation();
        Vector3 desiredPosition = target.position + target.rotation*(offset+addoffset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
        transform.Rotate(addrotationX, addrotationY, addrotationZ, Space.Self);
        
    }
}
