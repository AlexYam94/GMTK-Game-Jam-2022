using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] int _totalHealth = 2;
    [SerializeField] GameObject _deathEffect;
    [SerializeField] int _score = 100;

    public void Damage(int damageAmount)
    {
        if((_totalHealth -= damageAmount) <= 0)
        {
            if (_deathEffect != null)
            {
                Instantiate(_deathEffect, transform.position, transform.rotation);
                GetComponent<DropitemController>()?.DropItem();
                ScoreController.GetInstance().Add(_score);
            }
            Destroy(gameObject);
        }
    }
}
