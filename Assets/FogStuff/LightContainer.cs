using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LightContainer : MonoBehaviour
{
    [SerializeField] private float fadeDist = 1f;
    [SerializeField] private List<Transform> lightPositions;
    [SerializeField] private Renderer fogRend;

    private void Update()
    {
        if (lightPositions.Count == 0) return;

        fogRend.material.SetVector("_Light0Pos", lightPositions[0].position);
        //fogRend.material.SetVector("_Light1Pos", lightPositions[1].position);
        //fogRend.material.SetVector("_Light2Pos", lightPositions[2].position);
        //fogRend.material.SetVector("_Light3Pos", lightPositions[3].position);
        fogRend.material.SetFloat("_FadeDistance", fadeDist);
    }

}
