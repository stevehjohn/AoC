using Microsoft.Xna.Framework.Input;

namespace AoC.Games.Infrastructure;

public class Input
{
    private Keys[] _previousKeys = [];

    private MouseState _previousMouse;

    public int MouseX => _previousMouse.X;

    public int MouseY => _previousMouse.Y;
    
    public void UpdateState()
    {
        _previousKeys = Keyboard.GetState().GetPressedKeys();

        _previousMouse = Mouse.GetState();
    }

    public bool KeyPressed(Keys key)
    {
        return Keyboard.GetState().IsKeyUp(key) && _previousKeys.Contains(key);
    }

    public bool LeftButtonClicked()
    {
        return Mouse.GetState().LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed;
    }
}