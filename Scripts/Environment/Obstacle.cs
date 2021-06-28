using Godot;
using System;

public class Obstacle : Area2D
{
    static Random random = new Random();
    Sprite top;
    Sprite body;
    CollisionPolygon2D collider;
    public static int max = 10;
    float size;
    bool growing = true;

    uint time;
    public override void _Ready()
    {
        size = (float)random.Next(1,max);
        top = this.GetChild<Sprite>(0);
        body = this.GetChild<Sprite>(1);
        collider = this.GetChild<CollisionPolygon2D>(3);

        body.Scale = new Vector2(1,size);
        body.Translate(new Vector2(0,-((size-1)*1.5f)));
        top.Translate(new Vector2(0,-((size-1)*3)));
        collider.Scale = new Vector2(1,0.9f+size*0.05f);
        collider.Translate(new Vector2(0,-((size-1)*1.5f)));
    }

    public override void _Process(float delta)
    {
        
    }

    public void _on_Area2D_body_exited(Node body){
        GUI.UpdateScore();
    }

    public void _on_Body_body_entered(Node body){
        if(body is Player){
            GUI.Playing = false;
            ((Player)body).Stop();
        }
    }

}
