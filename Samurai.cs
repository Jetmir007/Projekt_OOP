using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;using Microsoft.Xna.Framework.Graphics;

namespace Projekt_OOP
{
    public class Samurai : CharacterBase, ICharacterAction
    {
        public Samurai(Texture2D tex) : base(tex)
        {
            Speed = 5f;
        }

        public void Attack(CharacterBase opponent)
        {
            opponent.TakeDamage(15);
        }

        public void SpecialAttack(CharacterBase opponent)
        {
            opponent.TakeDamage(30);
        }
    }
}
