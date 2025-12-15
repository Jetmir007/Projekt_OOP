using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;using Microsoft.Xna.Framework.Graphics;

namespace Projekt_OOP
{
    public class Ninja : CharacterBase, ICharacterAction
    {
        public Ninja(Texture2D tex) : base(tex)
        {
            Speed = 6f;
        }

        public void Attack(CharacterBase opponent)
        {
            opponent.TakeDamage(12);
        }

        public void SpecialAttack(CharacterBase opponent)
        {
            opponent.TakeDamage(25);
        }
    }
}
