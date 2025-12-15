using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Projekt_OOP
{
    public class KeyboardInput : IPlayerInput
    {
        Keys _left, _right, _jump, _attack, _special;

        public KeyboardInput(Keys left, Keys right, Keys jump, Keys attack, Keys special)
        {
            _left = left;
            _right = right;
            _jump = jump;
            _attack = attack;
            _special = special;
        }

        public int GetMoveDirection(KeyboardState current)
        {
            if (current.IsKeyDown(_left)) return -1;
            if (current.IsKeyDown(_right)) return 1;
            return 0;
        }

        public bool JumpPressed(KeyboardState c, KeyboardState p)
            => c.IsKeyDown(_jump) && !p.IsKeyDown(_jump);

        public bool AttackPressed(KeyboardState c, KeyboardState p)
            => c.IsKeyDown(_attack) && !p.IsKeyDown(_attack);

        public bool SpecialPressed(KeyboardState c, KeyboardState p)
            => c.IsKeyDown(_special) && !p.IsKeyDown(_special);
    }
}
