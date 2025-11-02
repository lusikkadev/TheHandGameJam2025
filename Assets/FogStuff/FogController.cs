using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class FogController : MonoBehaviour
{
    [SerializeField] private float fadeDist = 1f;
    [SerializeField] private float remDist = -1f;
    [SerializeField] private List<Transform> lightPositions;
    [SerializeField] private Renderer fogRend;

    Color cachedColor;

    float cachedXNoiseScale;
    float cachedYNoiseScale;
    float cachedNoiseMult;
    float cachedTiling;

    private void Awake()
    {
        cachedColor = fogRend.material.GetColor("_BaseColor");

        cachedNoiseMult = fogRend.material.GetFloat("_NoiseMult");
        cachedXNoiseScale = fogRend.material.GetFloat("_XNoiseScale");
        cachedYNoiseScale = fogRend.material.GetFloat("_YNoiseScale");
        cachedTiling = fogRend.material.GetFloat("_Tiling");
    }

    
    
    
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

        if (Input.GetKeyDown(KeyCode.V)) 
        {
            OnPlayerGrabbed();
        }
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            ResetGrabbed();
        }
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
    private void ResetGrabbed() 
    {
        StopAllCoroutines();

        fogRend.material.SetColor("_BaseColor", cachedColor);
        fogRend.material.SetFloat("_XNoiseScale", cachedXNoiseScale);
        fogRend.material.SetFloat("_YNoiseScale", cachedYNoiseScale);
        fogRend.material.SetFloat("_NoiseMult", cachedNoiseMult);
        fogRend.material.SetFloat("_Tiling", cachedTiling);
    }
    public void OnPlayerGrabbed() 
    {
        StartCoroutine(PlayerGrabbedEffectCo());
    }
    [SerializeField] private float alphaAddAmt = 0.1f;
    [SerializeField] private float coDelay = 0.1f;

    [SerializeField] private float xAdd = 0.1f;
    [SerializeField] private float yAdd = 0.1f;
    [SerializeField] private float mAdd = 0.1f;
    [SerializeField] private float tAdd = 0.1f;
   
    private IEnumerator PlayerGrabbedEffectCo() 
    {
        Color c = cachedColor;

        float x = cachedXNoiseScale;
        float y = cachedYNoiseScale;
        float m = cachedNoiseMult;
        float t = cachedTiling;

        while (true) 
        {
            yield return new WaitForSeconds(coDelay);

            c.a += alphaAddAmt;
            c.a = Mathf.Clamp(c.a, 0, 1);

            fogRend.material.SetColor("_BaseColor", c);

            x += xAdd;
            y += yAdd;
            m += mAdd;
            t += tAdd;

            fogRend.material.SetFloat("_XNoiseScale", x);
            fogRend.material.SetFloat("_YNoiseScale", y);
            fogRend.material.SetFloat("_NoiseMult", m);
            fogRend.material.SetFloat("_Tiling", t);
        }
    }
}
