using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ShipType {Player,Enemy };
public enum ItemType { PBullet, Enemy ,EBullet};

[System.Serializable]
public class Space_Ship
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int speed=5;
    private float halfWidth=0.225f;//height/2/100
    private float halfheight=0.285f;//height/2/100
    [SerializeField]
    private ShipType type;

    public void SetData(string Name, int Speed, ShipType typ)
    {
        name = Name;
        speed = Speed;
        type = typ;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public float HalfWidth
    {
        get { return halfWidth; }
        set { halfWidth = value; }
    }
    public float HalfHeight
    {
        get { return halfheight; }
        set { halfheight = value; }
    }
    public ShipType Ship
    {
        get { return type; }
        set { type = value; }
    }
}
public interface ShipController {

    public abstract void Move(Vector2 dir=new Vector2());
    public abstract void PowerUp();
    public abstract void Dead();
    public abstract void Shoot();
}

[System.Serializable]
public class PoolObject
{

    public GameObject Obj;
    public bool isExpandable = true;
    public int poolSize = 20;
    public ItemType poolType;
}





