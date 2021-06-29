using Godot;
using System.Threading.Tasks;

public class Start : MarginContainer
{
    static Label highscore;
    public override void _Ready()
    {
        highscore = GetChild<Control>(0).GetChild<Control>(0).GetChild<Label>(0);
        highscore.Text = "highscore: "+GUI.highscore;
    }

    public void _on_Button_pressed(){
        GetNode<Player>("/root/Game/Player").StandUp();
        StartGame();
    }
    public void _on_Outfits_pressed(){
        GetNode<MarginContainer>("/root/Game/Camera2D/Outfits").Visible = true;
        this.Visible = false;
    }

    void StartGame()
    {
        GUI.Playing = true;
        this.Visible = false;
    }

    public static void Reset(){
        highscore.Text = "highscore: "+GUI.highscore;
    }
    
}

