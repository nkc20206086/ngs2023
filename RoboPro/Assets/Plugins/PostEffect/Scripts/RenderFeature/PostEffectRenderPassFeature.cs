using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostEffectRenderPassFeature : ScriptableRendererFeature
{
    [SerializeField] private Material shadergraphMaterial;

    public void SetColor(string nameID, Color color)
    {
        shadergraphMaterial.SetColor(nameID, color);
    }

    public void SetFloat(string nameID, float value)
    {
        shadergraphMaterial.SetFloat(nameID, value);
    }

    public void SetTexture(string nameID, Texture texture)
    {
        shadergraphMaterial.SetTexture(nameID, texture);
    }

    public void SetTexture(string nameID, RenderTexture rTexture, RenderTextureSubElement element)
    {
        shadergraphMaterial.SetTexture(nameID, rTexture, element);
    }

    public void SetShader(Shader shader)
    {
        shadergraphMaterial.shader = shader;
    }

    class SGRenderPass : ScriptableRenderPass
    {
        public Material material;
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null) return;
            var camera = renderingData.cameraData.camera;
            var cmd = CommandBufferPool.Get("SG_RenderPass");
            cmd.Blit(Texture2D.whiteTexture, camera.activeTexture, material);
            context.ExecuteCommandBuffer(cmd);
            context.Submit();
        }
    }

    SGRenderPass sgRenderPass;
    public override void Create()
    {
        sgRenderPass = new SGRenderPass();
        sgRenderPass.material = shadergraphMaterial;
        sgRenderPass.renderPassEvent = RenderPassEvent.AfterRendering + 2;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(sgRenderPass);
    }
}