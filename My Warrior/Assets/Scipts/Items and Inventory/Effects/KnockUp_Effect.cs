using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnockUp_Effect : MonoBehaviour
{
    [SerializeField] private int yKnockUp;
    [SerializeField] private float knockUpDuration;
    [SerializeField] private int damageTornado;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            Transform enemy = collision.GetComponent<Transform>();
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(damageTornado);
            StartCoroutine(KnockUpEnemy(enemy));
        }
    }

    private IEnumerator KnockUpEnemy(Transform enemy)
    {
        float duration = knockUpDuration; // Th?i gian ?? h?t tung l�n v? tr� y = 5
        float elapsedTime = 0;

        Vector2 startPosition = enemy.position;
        Vector2 targetPosition = new Vector2(startPosition.x, yKnockUp);

        while (elapsedTime < duration)
        {
            enemy.position = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Ch? frame ti?p theo
        }

        enemy.position = targetPosition; // ??m b?o v? tr� cu?i c�ng l� v? tr� mong mu?n
    }
}
