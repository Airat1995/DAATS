using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Fog : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        private ProfilingSampler _profilingSampler = new ProfilingSampler("Fog");
        private Material _material;
        private RenderTargetIdentifier _cameraColorTarget;

        public CustomRenderPass(Material material)
        {
            _material = material;
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }
        
        public void SetTarget(RenderTargetIdentifier colorHandle)
        {
            _cameraColorTarget = colorHandle;
        }

        
        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in a performant manner.
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ConfigureTarget(new RenderTargetIdentifier(_cameraColorTarget, 0, CubemapFace.Unknown, -1));
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var camera = renderingData.cameraData.camera;
            if (camera.cameraType != CameraType.Game)
                return;

            if (_material == null)
                return;

            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, _profilingSampler))
            {
                cmd.SetRenderTarget(new RenderTargetIdentifier(_cameraColorTarget, 0, CubemapFace.Unknown, -1));
                //The RenderingUtils.fullscreenMesh argument specifies that the mesh to draw is a quad.
                cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, _material);
            }
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
        }
    }

    CustomRenderPass _scriptablePass;
    
    public Material _material;

    /// <inheritdoc/>
    public override void Create()
    {
        _scriptablePass = new CustomRenderPass(_material);

        // Configures where the render pass should be injected.
        _scriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        //if (renderingData.cameraData.cameraType == CameraType.Game)
        {
            //Calling ConfigureInput with the ScriptableRenderPassInput.Color argument ensures that the opaque texture is available to the Render Pass
            _scriptablePass.ConfigureInput(ScriptableRenderPassInput.Color);
            _scriptablePass.SetTarget(renderer.cameraColorTarget);
            renderer.EnqueuePass(_scriptablePass);
        }
    }
}


