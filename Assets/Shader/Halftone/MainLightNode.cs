//This API to create new nodes has been deprecated in early 2019.
//This means that if you are on a more recent version of Shader Graph and Universal Render Pipeline, you should use instead a pre-made node, which acts like a container that allows you to inject custom HLSL code into Shader Graph without the need to create a node from scratch.
//Please use the new node, not the C# API described here.
//
//You can find more details about that node in the Docs: https://docs.unity3d.com/Packages/com.unity.shadergraph@6.7/manual/Custom-Function-Node.html
//I also blogged about it here: https://connect.unity.com/p/adding-your-own-hlsl-code-to-shader-graph-the-custom-function-node
//
//If you are still on a very old version of Lightweight Render Pipeline (you shouldn't!!), you can still see the C# code for this custom node by rolling back to a previous version.