namespace Eltitnu.Common
{
	public class TexturedModel
	{
		public ModelData model;

		public Shader shader;

		public Texture texture;

		// TODO:? Is it proper to init in the constructor
		//      If we construct it in wrong places, there will be a binding error
		public TexturedModel(string modelPath, string vertexShaderPath, string fragmentShaderPath, string texturePath)
        {
			model = new ModelData();
			model.LoadFromCOLLADA(modelPath);
			shader = new Shader(vertexShaderPath, fragmentShaderPath);
			texture = Texture.LoadFromFile(texturePath);
		}
	}
}

