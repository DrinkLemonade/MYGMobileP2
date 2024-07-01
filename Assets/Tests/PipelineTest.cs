using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineTest
{

    //Check that we're using URP and the SRP asset hasn't changed.
    [Test]
    public void DoTest()
    {
        string name = UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.GetType().Name;
        Debug.Log($"PIPELINE NAME: {name}");
        Assert.IsTrue(name == "UniversalRenderPipelineAsset");
    }
}
