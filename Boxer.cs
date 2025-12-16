using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;using Microsoft.Xna.Framework.Graphics;

namespace Projekt_OOP
{
    public class Boxer : CharacterBase, ICharacterAction
    {
        public Boxer(Texture2D tex) : base(tex)
        {
            Speed = 4.5f;
        }

        public void Attack(CharacterBase opponent)
        {
            if(IsInRange(opponent, 100))
            {
                opponent.TakeDamage(10);
            }
        }

        public void SpecialAttack(CharacterBase opponent)
        {
            if(IsInRange(opponent, 70))
            {
                opponent.TakeDamage(20);
            }
        }
    }
}
