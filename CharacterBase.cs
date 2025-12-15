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

        public void HandleInput(
            IPlayerInput input,
            KeyboardState current,
            KeyboardState previous,
            CharacterBase opponent)
        {
            int dir = input.GetMoveDirection(current);
            if (dir != 0)
                Move(dir);

            if (input.JumpPressed(current, previous))
                Jump();

            if (this is ICharacterAction action)
            {
                if (input.AttackPressed(current, previous))
                    action.Attack(opponent);

                if (input.SpecialPressed(current, previous))
                    action.SpecialAttack(opponent);
            }
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
