using UnityEngine;
using UnityEngine.Rendering;

namespace RendererRays
{

    public abstract class URPComputeAsset : ScriptableObject
    {
        public ComputeShader shader;

        public virtual void Setup() { }
        public abstract void Render(CommandBuffer commandBuffer, int kernelHandle);
        public virtual void Cleanup() { }
    }

}
