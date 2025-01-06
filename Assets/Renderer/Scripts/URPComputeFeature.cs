using System;
using UnityEngine.Rendering.Universal;

using RendererRays;

namespace RaymarchingSphere.ComputeAssets
{

    public class URPComputeFeature : ScriptableRendererFeature
    {
        [Serializable]
        public class URPComputeSettings
        {
            public RenderPassEvent passEvent = RenderPassEvent.AfterRenderingOpaques;
            public URPComputeAsset computeAsset;
        }

        public URPComputeSettings settings = new URPComputeSettings();

        private URPComputePass computePass;

        public override void Create()
        {
            if (settings.computeAsset == null) { return; }

            settings.computeAsset.Setup();
            computePass = new URPComputePass(name, settings);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (settings.computeAsset == null) { return; }
            renderer.EnqueuePass(computePass);
        }

        private void OnDisable()
        {
            settings.computeAsset?.Cleanup();
        }
    }

}
