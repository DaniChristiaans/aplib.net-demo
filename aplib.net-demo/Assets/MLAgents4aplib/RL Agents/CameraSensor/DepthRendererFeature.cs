using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthRenderFeature : ScriptableRendererFeature
{
    // A simple renderer feature for the universal render pipeline; allowing us to apply our depth shader
    public Shader depthShader;
    public string renderPassName = "TopDownDepthPass";
    public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingOpaques;

    private DepthRenderPass renderPass;

    public override void Create()
    {
        renderPass = new DepthRenderPass(depthShader, renderPassName, renderPassEvent);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }

    class DepthRenderPass : ScriptableRenderPass
    {
        private Material depthMat;
        private FilteringSettings filteringSettings;
        private ShaderTagId shaderTagId = new ShaderTagId("DepthOnly");
        private string renderPassName;

        public DepthRenderPass(Shader depthShader, string renderPassName, RenderPassEvent renderPassEvent)
        {
            this.renderPassName = renderPassName;
            this.renderPassEvent = renderPassEvent;
            filteringSettings = new FilteringSettings(RenderQueueRange.all);

            if (depthShader != null)
                depthMat = CoreUtils.CreateEngineMaterial(depthShader);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (depthMat == null)
                return;

            var cmd = CommandBufferPool.Get(renderPassName);
            var drawingSettings = CreateDrawingSettings(shaderTagId, ref renderingData, SortingCriteria.CommonOpaque);
            drawingSettings.overrideMaterial = depthMat;

            context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
