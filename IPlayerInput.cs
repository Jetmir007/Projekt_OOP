using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace Projekt_OOP
{
    public interface IPlayerInput
    {
        int GetMoveDirection(KeyboardState current);
        bool JumpPressed(KeyboardState current, KeyboardState previous);
        bool AttackPressed(KeyboardState current, KeyboardState previous);
        bool SpecialPressed(KeyboardState current, KeyboardState previous);
    }
}
