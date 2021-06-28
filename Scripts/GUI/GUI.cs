using Godot;
using System;
using System.Threading.Tasks;

public class GUI : MarginContainer
{
    Label counter;
    public static int score = 0;
    public static int highscore;
    static public int Score{
        get{return score;}
    }
    static bool playing = false;
    public static bool Playing{
        set{
            playing = value;
            if(!value){
                Task.Delay(1000).ContinueWith(t=> Reset());             
            }
        }
        get{return playing;}
    }

    static GUI current = null;
    bool popup = true;


    public override void _Ready()
    {
        if(current == null)current = this;
        else{
            QueueFree();
        }
        counter = GetChild<Control>(0).GetChild<Control>(0).GetChild<Label>(0);
        counter.Text = "";
        LoadGame();
    }


    public static void UpdateScore(){
        score++;
        current.counter.Text = ""+score;
        if(score > highscore)current.NewHighscore();
        if(score > 20)Obstacle.max = 15;
        else if(score > 40)Obstacle.max = 20;
    }

    public static void Reset(){
        score = 0;
        current.counter.Text = "";
        current.counter.AddColorOverride("font_color",new Color(1,1,1,1));
        current.popup = true;
        current.GetNode<Ground>("/root/Game/Ground").ClearAll();
        current.GetNode<Start>("/root/Game/Camera2D/StartButton").Visible = true;
        Start.Reset();  
        Obstacle.max = 10;
        current.GetNode<Player>("/root/Game/Player").Reset();
        SaveGame();
    }

    void NewHighscore(){
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
