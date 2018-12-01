using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Data",menuName ="ItemObject", order=1)]
public class ItemObject : ScriptableObject {

    public SpriteRenderer icon;
    public new string name;
    public string description;
    public float damage;
    public float travelTime;
    public bool isStackable = false;
    public bool destroyOnUse = false;
    public GameObject projectile;
}
