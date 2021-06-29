using Godot;
using System;

public class Outfits : MarginContainer
{
    Player player;
    PackedScene[] outfits = new PackedScene[6];
    public override void _Ready()
    {
        player = GetNode<Player>("/root/Game/Player");
        for (int i = 0; i < 6; i++)
        {
            //outfits[i] = ResourceLoader.Load<PackedScene>("res://Prefabs/Outfits/outfit"+i);            
        }
    }

    public void _Back_pressed(){
        this.Visible = false;
        GetNode<MarginContainer>("/root/Game/Camera2D/StartButton").Visible = true;
    }

    public void _Outfit_pressed(int n){
        switch (n)
        {
            case(0):
                if(GUI.highscore >= 25)player.EquipOutfit(outfits[0],0);
            break;
            case(1):
                if(GUI.highscore >= 40)player.EquipOutfit(outfits[1],1);
            break;
            case(2):
                if(GUI.highscore >= 75)player.EquipOutfit(outfits[2],2);
            break;
            case(3):
                if(GUI.highscore >= 100)player.EquipOutfit(outfits[3],3);
            break;
            case(4):
                if(GUI.highscore >= 160)player.EquipOutfit(outfits[4],4);
            break;
            case(5):
                if(GUI.highscore >= 250)player.EquipOutfit(outfits[5],5);
            break;
            default:
            break;
        }
    }

}
