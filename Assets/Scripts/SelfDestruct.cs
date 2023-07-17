using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SlowDeath(2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator SlowDeath(int timeForDeath)
    {
        yield return new WaitForSeconds(timeForDeath);
        Destroy(this.gameObject);
    }
}
