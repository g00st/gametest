namespace Gametest;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

public class Texture
{
    private uint _handle;


    public Texture(string name)
    {
       ImageResult image =  Loader.LoadTexture(name);
        GL.CreateTextures(TextureTarget.Texture2D, 1,out _handle);

        GL.TextureParameter(_handle, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        GL.TextureParameter(_handle, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TextureParameter(_handle, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TextureParameter(_handle, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.TextureStorage2D(_handle, 1, SizedInternalFormat.Rgba8,  image.Width, image.Height);
        GL.TextureSubImage2D(_handle, 0, 0, 0, image.Width, image.Height, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        GL.GenerateTextureMipmap(_handle);

    }

    public void Bind()
    {
        GL.BindTextureUnit(0,_handle);
    }
}