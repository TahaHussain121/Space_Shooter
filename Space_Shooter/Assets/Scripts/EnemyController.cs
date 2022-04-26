using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ShipController
{
    public Space_Ship enemy = new Space_Ship();
    public GameObject nozel1;
    public GameObject explosion;
    public Animator anim;

    public void Start()
    {
       
        //InvokeRepeating("Shoot", 0f, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
      
    }
    public void Dead()
    {
        //CancelInvoke("Shoot");
      
        this.gameObject.SetActive(false);
    }

    public void Move(Vector2 dir = default)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));



        Vector2 pos = transform.position;
        pos = new Vector2(pos.x, pos.y - enemy.Speed * Time.deltaTime);

        transform.position = pos;

        if (transform.position.y < min.y)
        {
            Dead();
        }
    }

    public void PowerUp()
    {
        throw new System.NotImplementedException();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "PBullet")
        {
            Debug.Log("here");
            PlayExplosion();
            Invoke ("Dead",0.5f);
        }
    }

    public void Shoot()
    {
        GameObject bullet = ObjectPool.GetPooledObject(ItemType.EBullet);
        if (bullet != null)
        {
            bullet.transform.position = nozel1.transform.position;
            bullet.transform.rotation = nozel1.transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().fire = true;

        }
       
    }

    public void PlayExplosion()
    {
        GameObject ex = Instantiate(explosion, this.transform);
        ex.GetComponent<Explosion>()._Destroy();
      
    }
}
