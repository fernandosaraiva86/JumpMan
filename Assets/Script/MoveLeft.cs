using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{


    [SerializeField] private float speed = 10f;

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
