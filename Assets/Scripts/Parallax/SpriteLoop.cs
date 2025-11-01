using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteLoop : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    [SerializeField] private float wrapThreshold = 1f;

    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    private void Start()
    {
        if (cam == null) cam = Camera.main;

        SpriteRenderer[] rend = GetComponentsInChildren<SpriteRenderer>();
        if (rend.Length == 0) return;
        for (int i = 0; i < rend.Length; i++)
        {
            sprites.Add(rend[i]);
        }
        sprites = sprites.OrderBy(x => x.transform.position.x).ToList();

        // snap sprites together
        for (int i = 0; i < sprites.Count - 1; i++)
        {
            float prevHalf = sprites[i].bounds.extents.x;
            float nextHalf = sprites[i + 1].bounds.extents.x;
            Vector3 targetPos = sprites[i].transform.position + Vector3.right * (prevHalf + nextHalf);
            sprites[i + 1].transform.position = targetPos;
        }
    }

    private void Update()
    {
        float camX = cam.transform.position.x;
        float hcw = cam.orthographicSize * cam.aspect;

        float camLeft = camX - hcw;
        float camRight = camX + hcw;

        // right
        while (sprites.Count > 0 && sprites[0].bounds.max.x < camLeft - wrapThreshold)
        {
            SpriteRenderer first = sprites[0];
            SpriteRenderer last = sprites[sprites.Count - 1];

            float newX = last.bounds.max.x + first.bounds.extents.x;
            Vector3 p = first.transform.position;
            first.transform.position = new Vector3(newX, p.y, p.z);

            sprites.RemoveAt(0);
            sprites.Add(first);
        }
        // left
        while (sprites.Count > 0 && sprites[sprites.Count - 1].bounds.min.x > camRight + wrapThreshold)
        {
            SpriteRenderer last = sprites[sprites.Count - 1];
            SpriteRenderer first = sprites[0];

            float newX = first.bounds.min.x - last.bounds.extents.x;
            Vector3 p = last.transform.position;
            last.transform.position = new Vector3(newX, p.y, p.z);

            sprites.RemoveAt(sprites.Count - 1);
            sprites.Insert(0, last);
        }
    }
}