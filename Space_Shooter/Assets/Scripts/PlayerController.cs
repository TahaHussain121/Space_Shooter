using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ShipController
{

    public static event ValueUpdator _PLiveUpdate;
    public static event Base _Dead;


    [SerializeField]
    private Space_Ship player = new Space_Ship();
    [SerializeField]
    private GameObject nozel1;
    [SerializeField]
    private GameObject nozel2;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int lives=6;
    [SerializeField]
    private int score;
    [SerializeField]
    private const int maxLives=6;


    public void OnEnable()
    {
        GameManager._ScoreUpdate += UpdateScore;
        GameManager._LiveUpdate += UpdateLives;


    }
    public void OnDisable()
    {

        GameManager._ScoreUpdate -= UpdateScore;
        GameManager._LiveUpdate -= UpdateLives;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        Move(dir);
    }
    public void Dead()
    {
        _Dead();
        PlayExplosion();
        Destroy(this.gameObject,0.5f);
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

    private void UpdateScore(int _score)
    {
        score = _score;
    }
    private void UpdateLives(int _lives)
    {
        lives = _lives;
    }

    public void Shoot()
    {
        GameObject bullet = ObjectPool.GetPooledObject(ItemType.PBullet);
        if (bullet != null)
        {
            bullet.transform.position = nozel1.transform.position;
            bullet.transform.rotation = nozel1.transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().fire = true;
        }
        GameObject bullet2 = ObjectPool.GetPooledObject(ItemType.PBullet);
        if (bullet2 != null)
        {
            bullet2.transform.position = nozel2.transform.position;
            bullet2.transform.rotation = nozel2.transform.rotation;
            bullet2.SetActive(true);
            bullet2.GetComponent<Bullet>().fire = true;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "EBullet")
        {

            Debug.Log("lives" + lives);
            if (lives <= maxLives && lives != 0)
            {
                lives = lives - 1;
                _PLiveUpdate(lives);

            }
            else if(lives == 0)
            {
                Debug.Log("Palyer are dead");

                Dead();
            }
          
        }
    }
    public void PowerUp()
    {
        throw new System.NotImplementedException();
    }
    public void PlayExplosion()
    {
        GameObject ex = Instantiate(explosion, this.transform);
        ex.GetComponent<Explosion>()._Destroy();


    }
}
