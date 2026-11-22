using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Цель (игрок)
    public Vector2 minBounds; // Минимальные границы (слева, снизу)
    public Vector2 maxBounds; // Максимальные границы (справа, сверху)

    [Header("Настройки плавности")]
    public float smoothTime = 0.2f; // Время сглаживания (чем больше, тем плавнее и медленнее)

    private float halfHeight;
    private float halfWidth;
    private Vector3 velocity = Vector3.zero; // Служебная переменная для SmoothDamp

    void Start()
    {
        Camera cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        // 1. Сглаживаем позицию игрока (вычисляем, куда камера ХОТЕЛА БЫ поехать)
        Vector3 smoothedTargetPos = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);

        // 2. Ограничиваем УЖЕ СГЛАЖЕННУЮ позицию рамками карты
        float clampedX = Mathf.Clamp(smoothedTargetPos.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(smoothedTargetPos.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        // 3. Устанавливаем итоговую позицию камеры
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}