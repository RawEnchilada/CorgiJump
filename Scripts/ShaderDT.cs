using Godot;
using System;

public class ShaderDT : Sprite
{
    float play;
    public override void _Process(float delta)
    {
        play = 0.0f;
        if(GUI.Playing){
            play = 1.0f;
        }        

        ((ShaderMaterial)this.Material).SetShaderParam("play",play);
    }
}
