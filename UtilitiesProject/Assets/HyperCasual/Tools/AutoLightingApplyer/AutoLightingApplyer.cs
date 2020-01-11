using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoLightingApplyer : MonoBehaviour {

    public bool AutoDestroy = true;

    public bool enableDirLight = true;
    public Color DirLightColor = Color.white;

    public bool enableClearColor = true;
    public int selection = 0;
    public Material SkyBoxMaterial = null;
    public Color SolidClearColor = Color.black;

    public bool enableFog = false;
    public FogMode FogMode = FogMode.Exponential;
    public Color FogColor = Color.white;
    public float FogDensity = 0.01f;
    public float FogStart = 0;
    public float FogEnd = 300;

    public void Store()
    {
        this.DirLightColor = FindObjectsOfType<Light>()
            .First(o => o.type == LightType.Directional)
            .color;
        this.selection = Camera.main.clearFlags == CameraClearFlags.Skybox ? 0 : 1;
        this.SkyBoxMaterial = RenderSettings.skybox;
        this.SolidClearColor = RenderSettings.ambientSkyColor;
        this.enableFog = RenderSettings.fog;
        this.FogMode = RenderSettings.fogMode;
        this.FogColor = RenderSettings.fogColor;
        this.FogStart = RenderSettings.fogStartDistance;
        this.FogEnd = RenderSettings.fogEndDistance;
        this.FogDensity = RenderSettings.fogDensity;
    }

    public void Apply()
    {
        Debug.Log("!! : " + gameObject.scene.name);
        if(enableDirLight)
        {
            var dirLight = FindObjectsOfType<Light>()
                .First(o => o.type == LightType.Directional);
            dirLight.color = this.DirLightColor;
        }

        if(enableClearColor)
        {
            switch(selection)
            {
                case 0: 
                    Camera.main.clearFlags = CameraClearFlags.Skybox;
                    RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
                    RenderSettings.skybox = this.SkyBoxMaterial;
                    break;
                case 1:
                    Camera.main.clearFlags = CameraClearFlags.Skybox;
                    RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
                    RenderSettings.ambientSkyColor = this.SolidClearColor;
                    break;
            }
        }
        if(enableFog)
        {
            RenderSettings.fog = this.enableFog;
            RenderSettings.fogMode = this.FogMode;
            RenderSettings.fogColor = this.FogColor;
            RenderSettings.fogDensity = this.FogDensity;
            RenderSettings.fogStartDistance = this.FogStart;
            RenderSettings.fogEndDistance = this.FogEnd;
        }

        //if (AutoDestroy && Application.isEditor) DestroyImmediate(this.gameObject);
        //if (AutoDestroy && Application.isPlaying) Destroy(this.gameObject);
    }
}
