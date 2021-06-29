using Godot;
using System;

public class Player : KinematicBody2D
{
    AnimatedSprite animation;
    AnimatedSprite outfit = null;
    bool canJump = true;
    bool touched = false;
    const float jumpHeight = 100f;
    int equipped = -1;
    public override void _Ready()
    {
        animation = GetChild<AnimatedSprite>(0);
        animation.Play("Idle");
        outfit?.Play("Idle");
        
    }

    public override void _Process(float delta)  {
        if(GUI.Playing){
            float speedMult = 5f-Mathf.Pow(Math.Abs(this.Position.y)/jumpHeight*2f,2f);
            Vector2 move = new Vector2(0f,3.5f);
            if(canJump && (Input.IsActionPressed("jump") || touched) && this.Position.y > -jumpHeight){
                move.y = -speedMult; 
                animation.Play("Jump"); 
                outfit?.Play("Jump");
            }
            else if(this.Position.y >= 10f){
                canJump = true;
                animation.Play("Run");
                outfit?.Play("Run");

            }
            else{
                canJump = false;
                animation.Play("Fall");
                outfit?.Play("Fall");

            }

            MoveAndCollide(move);
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event is InputEventScreenTouch eventTouch){
            if(eventTouch.IsPressed())touched = true;
            else touched = false;
        }
    }

    



    public void StandUp(){
        animation.Play("Stand");
        outfit?.Play("Stand");
    }

    public void Reset(){
        animation.Play("Idle");
        outfit?.Play("Idle");
        this.Position = new Vector2(-50f,16f);
    }

    public void Stop(){
        animation.Stop();
        outfit?.Stop();
    }

    public void EquipOutfit(PackedScene prefab,int num){
        if(equipped == num){
            UnEquip();
        }
        else if(equipped != -1){
            UnEquip();
            outfit = prefab.Instance<AnimatedSprite>();
            animation.AddChild(outfit);
            equipped = num;
            animation.Frame = 0;
            outfit.Frame = 0;
            outfit.Play("Idle");
        }
        else{
            outfit = prefab.Instance<AnimatedSprite>();
            animation.AddChild(outfit);
            equipped = num;
            animation.Frame = 0;
            outfit.Frame = 0;
            outfit.Play("Idle");
        }
    }

    void UnEquip(){
        animation.RemoveChild(outfit);
        outfit.QueueFree();
        equipped = -1;
    }

}
