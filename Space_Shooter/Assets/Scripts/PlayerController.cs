using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ShipController
{

    public Space_Ship player = new Space_Ship();
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        Move(dir);
    }
    public void Dead()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector2 dir)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - player.HalfWidth;
        max.y = max.y - player.HalfHeight;

        min.x = min.x + player.HalfWidth;
        min.y = min.y + player.HalfHeight;

        Vector2 pos = transform.position;
        pos += dir * player.Speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;



    }

    public void PowerUp()
    {
        throw new System.NotImplementedException();
    }

}
