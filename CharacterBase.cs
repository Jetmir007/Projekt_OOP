using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projekt_OOP
{
    public abstract class CharacterBase
    {
        public Vector2 Position;
        protected Texture2D Texture;

        public int Hp { get; private set; } = 100;

        protected float Speed = 4f;
        protected float VelocityY = 0f;
        protected bool IsGrounded = true;
        protected bool Kanattackera = true;

        public CharacterBase(Texture2D texture)
        {
            Texture = texture;
        }

        public void TakeDamage(int dmg)
        {
            Hp -= dmg;
            if (Hp < 0) Hp = 0;
        }

        public virtual void Move(int dir)
        {
            Position.X += dir * Speed;
        }

        public virtual void Jump()
        {
            if (IsGrounded)
            {
                VelocityY = -12f;
                IsGrounded = false;
            }
        }

        public void HandleInput( IPlayerInput input, KeyboardState current, KeyboardState previous, CharacterBase opponent)
        {
            int dir = input.GetMoveDirection(current);
            if (dir != 0){
                Move(dir);
            }
            if (input.JumpPressed(current, previous)){
                Jump();
            }
            if (this is ICharacterAction action)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.E)&&Kanattackera||Keyboard.GetState().IsKeyDown(Keys.O)&&Kanattackera){
                    Kanattackera=false;
                    action.Attack(opponent);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.F)&&Kanattackera||Keyboard.GetState().IsKeyDown(Keys.P)&&Kanattackera){
                    Kanattackera=false;
                    action.SpecialAttack(opponent);
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.E) && Keyboard.GetState().IsKeyUp(Keys.O))
            {
                Kanattackera = true;
            }
            if( Keyboard.GetState().IsKeyUp(Keys.F) && Keyboard.GetState().IsKeyUp(Keys.P))
            {
                Kanattackera = true;
            }
        }

        public bool IsInRange(CharacterBase opp, int range)
        {   
            float distance = 0f;
            if(opp.Position.X < this.Position.X)
            {
                distance = this.Position.X - opp.Position.X;
            }
            else
            {
                distance = opp.Position.X - this.Position.X;
            }
            return distance <= range;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!IsGrounded)
            {
                VelocityY += 0.6f;
                Position.Y += VelocityY;

                if (Position.Y >= 600)
                {
                    Position.Y = 600;
                    VelocityY = 0;
                    IsGrounded = true;
                }
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Position, Color.White);
        }
    }
}
