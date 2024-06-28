using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public Route route;
    public Transform bottomTransform;
    public Transform bodyTransform;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem Smokefx;
    [SerializeField] Rigidbody rb;
    [SerializeField] float danceValue;
    [SerializeField] float durationMultiplier;

    private void Start()
    {
        bodyTransform.DOLocalMoveY(danceValue, .1f)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.Linear);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Car othercar))
        {
            HandleCarCollision(collision);
        }
        else if (collision.transform.CompareTag("Obstacle"))
        {
            HandleObstacleCollision(collision);
        }
    }

    private void HandleCarCollision(Collision collision)
    {
        StopAllCoroutines();
        rb.DOKill(false);

        Vector3 hitPoint = collision.contacts[0].point;
        AddExplosionForce(hitPoint);
        Smokefx.Play();

        Game.Instance.OnCCollision?.Invoke();
    }

    private void HandleObstacleCollision(Collision collision)
    {
        StopAllCoroutines();
        rb.DOKill(false);

        Vector3 hitPoint = collision.contacts[0].point;
        AddExplosionForce(hitPoint);
        Smokefx.Play();

        Game.Instance.OnCCollision?.Invoke();

        // Optionally, you can invoke another event for obstacle collision
        // Game.Instance.OnObstacleCollision?.Invoke();
    }

    private float GetRandomAngle()
    {
        float angle = 10f;
        float rand = Random.value;
        return rand > .5f ? angle : -angle;
    }

    private void AddExplosionForce(Vector3 point)
    {
        rb.AddExplosionForce(400f, point, 3f);
        rb.AddForceAtPosition(Vector3.up * 2f, point, ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle()));
    }

    public void Move(Vector3[] path)
    {
        rb.DOLocalPath(path, 2f * durationMultiplier * path.Length)
            .SetLookAt(.01f, false)
            .SetEase(Ease.Linear);
    }

    public void StopDancingAnim()
    {
        bodyTransform.DOKill(true);
    }

    public void SetColor(Color color)
    {
        meshRenderer.sharedMaterials[0].color = color;
    }
}
