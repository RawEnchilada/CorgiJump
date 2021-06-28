using Godot;
using System;

public class Highscore : HSplitContainer
{
    uint birthTime = 0;
    static bool counting = false;
    static Highscore current;

    public override void _Ready()
    {
        if(current == null){
            current = this;
        }
        else QueueFree();
    }

    public override void _Process(float delta)
    {
        if(counting && GUI.Playing){
            var time = OS.GetTicksMsec() - birthTime;
            if(time <= 2000){
                MarginBottom++;
                MarginTop++;
            }
            else if(time >= 4000 & time < 6000)
            {
                MarginBottom--;
                MarginTop--;
            }
            else if(time > 6000)counting = false;
        }
    }

    public static void Display(){
        counting = true;
        current.birthTime = OS.GetTicksMsec();
    }

}
