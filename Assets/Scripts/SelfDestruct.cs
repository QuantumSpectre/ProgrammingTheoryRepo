using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelfDestruct : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SlowDeath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual IEnumerator SlowDeath()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
