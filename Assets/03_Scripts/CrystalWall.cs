using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalWall : MonoBehaviour
{
    [SerializeField] GameObject _crystalWall; 
    [SerializeField] private WayPoints _point;

    [SerializeField]private int _cantEnemies;
    public List<Enemy> enemies = new List<Enemy>();

    private Collider _myCollider;

    private void Awake()
    {
        _myCollider = GetComponent<Collider>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<Enemy>())
    //        _crystalWall.SetActive(true);
    //}

    private void Update()
    {
        //if (GameManager.instance.enemies.Count < 1)
        //{
        //    _point.action = false;
        //    _crystalWall.SetActive(false);
        //}
        if(enemies.Count >= _cantEnemies) _myCollider.enabled = false;
    }

    public void DesactivarColision() => _myCollider.enabled = false;


    public void DesactivarMuro()
    {
        _point.action = false;
        _crystalWall.SetActive(false);
    }
}
