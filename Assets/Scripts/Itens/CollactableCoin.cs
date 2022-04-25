using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableCoin : CollactableBase
{
    public Collider2D colliderCoin;

    protected override void OnCollect() {
        base.OnCollect();
        ItemManager.Instance.AddCoins();

        colliderCoin.enabled = false;
    }
}