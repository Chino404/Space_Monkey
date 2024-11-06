using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : Factory<Bullet>
{
    public BulletFactory(Bullet p)//en un constructor se entra cuando se crea
    {
        prefab = p;
    }
}