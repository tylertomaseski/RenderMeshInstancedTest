using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class RenderMeshInstancedTest : MonoBehaviour
{
	public Mesh Mesh;
	public Material Material;

	private void Awake()
	{
		RenderPipelineManager.beginCameraRendering += HandleRenderStart;

		if (Application.isPlaying == false)
			SceneView.beforeSceneGui += OnSceneGUI;
	}

	private void OnDestroy()
	{
		RenderPipelineManager.beginCameraRendering -= HandleRenderStart;

		if (Application.isPlaying == false)
			SceneView.beforeSceneGui -= OnSceneGUI;
	}

	private void OnSceneGUI(SceneView sceneView)
	{
		RenderMeshInstanced(sceneView.camera);
	}

	private void HandleRenderStart(ScriptableRenderContext context, Camera cam)
	{
		RenderMeshInstanced(cam);
	}

	void RenderMeshInstanced(Camera cam)
	{
		Matrix4x4[] instanceData = new Matrix4x4[] { transform.localToWorldMatrix };
		var renderParamsSceneView = new RenderParams(this.Material) { layer = this.gameObject.layer };
		renderParamsSceneView.camera = cam;
		Graphics.RenderMeshInstanced(renderParamsSceneView, Mesh, 0, instanceData);
	}

	//the below code works for game-view and flickers in scene view

	//void Update()
	//{
	//	//Render game view
	//	RenderParams renderParams = new RenderParams(this.Material) { camera = Camera.main, layer = this.gameObject.layer };
	//	Matrix4x4[] instanceData = new Matrix4x4[] { transform.localToWorldMatrix };

	//	Graphics.RenderMeshInstanced(renderParams, Mesh, 0, instanceData);


	//	//render scene view
	//	var cams = SceneView.GetAllSceneCameras();
	//	var renderParamsSceneView = new RenderParams(this.Material) { layer = this.gameObject.layer };
	//	foreach (var cam in cams)
	//	{
	//		renderParamsSceneView.camera = cam;
	//		Graphics.RenderMeshInstanced(renderParamsSceneView, Mesh, 0, instanceData);
	//	}
	//}

}
