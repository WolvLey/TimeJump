﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public ItemObject Item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.SendMessage("CalculateHealth", Item.damage);
        Destroy(gameObject);
    }
}
