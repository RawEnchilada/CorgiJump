using Godot;
using System;

public class Ground : StaticBody2D
{
    const float segmentSize = 144f;
    const int segmentCount = 6;
    const int decorationCount = 4;
    static Random random = new Random();
    string[] decorationPaths = {
        "res://Assets/Ground/bush1.png",
        "res://Assets/Ground/bush2.png",
        "res://Assets/Ground/tree1.png",
        "res://Assets/Ground/tree2.png"
    };


    bool placeObstacle = true;
    Sprite[] grounds = new Sprite[segmentCount];
    Texture[] decorations = new Texture[decorationCount];
    PackedScene obstacle;
    public override void _Ready()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            grounds[i] = GetChild<Sprite>(i);
        }
        for (int i = 0; i < decorationCount; i++)
        {
            decorations[i] = (Texture)ResourceLoader.Load(decorationPaths[i]);
        }
        obstacle = (PackedScene)ResourceLoader.Load("res://Prefabs/Obstacle.tscn");
        
        #if DEBUG
        GD.Print("Ground: Object loaded");
        #endif

    }

    public override void _Process(float delta)
    {
        if(GUI.Playing){
            foreach (var segment in grounds)
            {
                segment.Translate(new Vector2(-3f,0));
                if(segment.Position.x <= -((segmentCount/2-1)*segmentSize+segmentSize/2f)){
                    segment.Position = (new Vector2(segmentSize*(segmentCount/2),0f));
                    ClearSegment(segment);
                    GenerateProps(segment);

                    #if DEBUG
                    GD.Print("Ground: Segment updated");
                    #endif
                }
            }
        }
    }

    void GenerateProps(Sprite segment){
        if(placeObstacle && random.Next(0,2) == 1){
            placeObstacle = false;
            Node2D instance = obstacle.Instance<Node2D>();
            segment.AddChild(instance);
            instance.Translate(new Vector2(random.Next(-50,60),-52));
            instance.ZIndex = -2;

            #if DEBUG
            GD.Print("Ground: Obstacle placed");
            #endif
        }
        else{
            placeObstacle = true;
        }
        var sprite = new Sprite();
        sprite.Texture = decorations[random.Next(0,4)];
        segment.AddChild(sprite);
        sprite.Translate(new Vector2(random.Next(-72,72),random.Next(-100,-80)));
        sprite.ZIndex = -4;

        #if DEBUG
        GD.Print("Ground: Decoration placed");
        #endif
        
    }

    void ClearSegment(Sprite segment){
        foreach (Node2D child in segment.GetChildren())
        {
            segment.RemoveChild(child);
            child.QueueFree();
        }

        #if DEBUG
        GD.Print("Ground: Segment cleared");
        #endif
    }

    public void ClearAll(){
        foreach (var segment in grounds){
            foreach (Node2D child in segment.GetChildren()){
                segment.RemoveChild(child);
                child.QueueFree();
            }
        }

        #if DEBUG
        GD.Print("Ground: All segments cleared");
        #endif
    }
}
