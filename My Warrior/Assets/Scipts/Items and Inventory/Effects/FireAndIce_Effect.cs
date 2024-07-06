using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire And Ice Effect", menuName = "Data/Item Effect/Fire And Ice")]
public class FireAndIce_Effect : ItemEffect
{
    [SerializeField] private GameObject fireAndIcePrefab;
    [SerializeField] private float xVelocity;

    public override void ExecuteEffect(Transform _respawnPosition)
    {
        Player player = PlayerManager.instance.player;

        bool thirdAttack = player.GetComponent<Player>().primaryAttack.comboCounter == 2;

        if (thirdAttack)
        {
            GameObject newFireAndIce = Instantiate(fireAndIcePrefab, _respawnPosition.position, player.transform.rotation);
            newFireAndIce.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDirection, 0);

            Destroy(newFireAndIce, 10);

        }


    }
}
