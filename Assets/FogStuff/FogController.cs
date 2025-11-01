using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FogController : MonoBehaviour
{
    [SerializeField] private float fadeDist = 1f;
    [SerializeField] private float remDist = -1f;
    [SerializeField] private List<Transform> lightPositions;
    [SerializeField] private Renderer fogRend;
    private void Update()
    {
        fogRend.material.SetVector("_Light0Pos", lightPositions[0].position);
        fogRend.material.SetVector("_Light1Pos", lightPositions[1].position);
        fogRend.material.SetVector("_Light2Pos", lightPositions[2].position);
        fogRend.material.SetVector("_Light3Pos", lightPositions[3].position);
        fogRend.material.SetVector("_Light4Pos", lightPositions[4].position);
        fogRend.material.SetVector("_Light5Pos", lightPositions[5].position);
        fogRend.material.SetVector("_Light6Pos", lightPositions[6].position);
        
        fogRend.material.SetFloat("_FadeDistance", fadeDist);
        fogRend.material.SetFloat("_Rem", remDist);
    }

    public void SetPlayerLights(bool value) 
    {
        if (!value)
        {
            fogRend.material.SetFloat("_Light0FadeDist", 0f);
            fogRend.material.SetFloat("_Light0Rem", 0f);
        }
        else 
        {
            fogRend.material.SetFloat("_Light0FadeDist", fadeDist);
            fogRend.material.SetFloat("_Light0Rem", remDist);
        }
        
    }
}
