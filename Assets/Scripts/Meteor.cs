using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    float maxLifetime = 5.0f;
    public int lifePoints = 3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifetime);
    }
}
