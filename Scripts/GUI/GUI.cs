using Godot;
using System;

public class GUI : MarginContainer
{
    static Label counter;
    public static int score = 0;
    public static int highscore;
    static public int Score{
        get{return score;}
    }
    static bool playing = false;
    static bool doReset = false;
    static uint resetRequest = 0;
    public static bool Playing{
        set{
            playing = value;
            if(!value){
                doReset = true;
                resetRequest = OS.GetTicksMsec();
            }

        }
        get{return playing;}
    }

    static bool popup = true;


    public override void _Ready()
    {
        counter = GetChild<Control>(0).GetChild<Control>(0).GetChild<Label>(0);
        counter.Text = " ";
        LoadGame();

    }

    public override void _Process(float delta)
    {
        if(doReset){
            if(OS.GetTicksMsec()-resetRequest > 1000)
                Reset();
            
        }
    }


    public static void UpdateScore(){
        score++;
        counter.Text = ""+score;
        if(score > highscore)NewHighscore();
        if(score > 20)Obstacle.max = 15;
        else if(score > 40)Obstacle.max = 20;

    }

    public static void Reset(){
        score = 0;
        counter.Text = " ";
        counter.AddColorOverride("font_color",new Color(1,1,1,1));
        popup = true;
        counter.GetNode<Ground>("/root/Game/Ground").ClearAll();
        counter.GetNode<Start>("/root/Game/Camera2D/StartButton").Visible = true;
        Start.Reset();  
        Obstacle.max = 10;
        counter.GetNode<Player>("/root/Game/Player").Reset();
        SaveGame();
        doReset = false;
    }

    static void NewHighscore(){
        highscore = score;
        counter.AddColorOverride("font_color",new Color(0,1,0.2f,1));
        if(popup){
            popup = false;
            Highscore.Display();
        }

    }

    static void SaveGame()
    {
        var saveGame = new File();
        saveGame.Open("user://savegame.save", File.ModeFlags.Write);
        saveGame.StoreLine(""+highscore);
        saveGame.Close();
    }

    static void LoadGame(){
        var saveGame = new File();
        if (!saveGame.FileExists("user://savegame.save")){
            highscore = 0;
            return;
        }
        saveGame.Open("user://savegame.save", File.ModeFlags.Read);

        highscore = Int32.Parse(saveGame.GetLine());

        saveGame.Close();
        
    }


}
