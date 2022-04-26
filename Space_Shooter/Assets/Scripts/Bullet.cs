using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int speed = 20;
    [SerializeField]
    private float damage = 2;
    public ItemType Bullettype;
    public bool fire = false;
    public void Update()
    {
        Movement(Bullettype);

    }

    public void Movement(ItemType bulletType)
    {
        if (bulletType == ItemType.PBullet)
        {
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            transform.Translate((transform.up * speed * Time.deltaTime));

            if (transform.position.y > max.y)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (bulletType == ItemType.EBullet)
        {
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            Vector2 mini = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            transform.Translate((-transform.up * speed * Time.deltaTime));

            if ((transform.position.y > max.y || transform.position.y < mini.y) /*&& (transform.position.x > max.x || transform.position.x < mini.x)*/)
            {
                Debug.Log("here lays the dead");
                this.gameObject.SetActive(false);
            }

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Bullettype == ItemType.PBullet)
        {
            if (collision.tag == "Enemy" || collision.tag == "EBullet")
            {
                this.gameObject.SetActive(false);
            }
        }
        else if(Bullettype == ItemType.EBullet)
        {
            if (collision.tag == "Player" || collision.tag == "PBullet")
            {
                this.gameObject.SetActive(false);

            }
        }
    }
}


