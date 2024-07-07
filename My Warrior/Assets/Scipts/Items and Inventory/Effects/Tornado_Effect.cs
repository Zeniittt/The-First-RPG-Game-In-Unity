using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Fly Up Effect", menuName = "Data/Item Effect/Fly Up")]
public class Tornado_Effect : ItemEffect
{
    [SerializeField] private GameObject tornadoPrefab;
    [SerializeField] private float xVelocity;

    public override void ExecuteEffect(Transform _respawnPosition)
    {
        Player player = PlayerManager.instance.player;

        bool thirdAttack = player.GetComponent<Player>().primaryAttack.comboCounter == 2;

        if (thirdAttack)
        {
            GameObject newTornado = Instantiate(tornadoPrefab, _respawnPosition.position, player.transform.rotation);
            newTornado.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDirection, 0);

            Destroy(newTornado, 2);

        }
    }
}
