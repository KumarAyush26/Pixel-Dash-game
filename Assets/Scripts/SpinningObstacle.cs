using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    [SerializeField] private float speed =0.2f;
    void Update()
    {
        transform.Rotate( 360 * speed * Time.deltaTime,0,0);
    }

}
