using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    public void _Destroy()
    {
        Invoke("DestroyExplosion", 0.5f);
    }
     void DestroyExplosion()
    {
        Debug.Log("Destroyeddddd");
        Destroy(gameObject);
    }


}
