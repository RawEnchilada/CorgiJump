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

        #if DEBUG
        GD.Print("Obstacle: Object created");
        #endif
    }

    public override void _Process(float delta)
    {
        if(GUI.Playing && GUI.score > 75){
            
            body.Scale = new Vector2(1,size);
            body.Translate(new Vector2(0,-9.5f-((size-1)*1.5f))-body.Position);
            top.Translate(new Vector2(0,-35.5f-((size-1)*3))-top.Position);
            collider.Scale = new Vector2(1,0.9f+size*0.05f);
            collider.Translate(new Vector2(0,-24-((size-1)*1.5f))-collider.Position);

            if(growing)size+=0.1f;
            else size-=0.1f;
            if(size >= 20f)growing = false;
            else if(size <= 1f)growing = true;

            #if DEBUG
            GD.Print("Obstacle: Resized");
            #endif
        }
    }

    public void _on_Area2D_body_exited(Node body){
        GUI.UpdateScore();

        #if DEBUG
        GD.Print("Obstacle: Player exited collider");
        #endif
    }

    public void _on_Body_body_entered(Node body){
        if(body is Player){
            GUI.Playing = false;
            ((Player)body).Stop();

            #if DEBUG
            GD.Print("Obstacle: Player hit spike collider");
            #endif
        }
    }

}
