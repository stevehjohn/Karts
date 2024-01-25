using Microsoft.Xna.Framework.Input;

namespace Karts.Engine;

public class Player
{
    public double Angle { get; private set; }
    
    public double SteeringAngle { get; private set; }

    public Point2D Position { get; private set; } = new(0.11d, 0.56d);

    private double _speed;
    
    public void Update()
    {
        var state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.P))
        {
            if (SteeringAngle < 0.02d)
            {
                SteeringAngle += 0.0005d;
            }
        }
        else if (state.IsKeyDown(Keys.O))
        {
            if (SteeringAngle > -0.02d)
            {
                SteeringAngle -= 0.0005d;
            }
        }
        else
        {
            switch (SteeringAngle)
            {
                case > 0:
                    SteeringAngle -= 0.001d;
                    break;
                case < 0:
                    SteeringAngle += 0.001d;
                    break;
            }
        }

        if (state.IsKeyDown(Keys.Q))
        {
            if (_speed < 0.004d)
            {
                _speed += 0.00001d;
            }
        }
        else if (state.IsKeyDown(Keys.A))
        {
            if (_speed > 0)
            {
                _speed -= 0.00004d;
            }
        }
        else if (_speed > 0)
        {
            _speed -= 0.00001d;
        }

        Position = Position.MoveBy(SteeringAngle, _speed);

        if (_speed > 0)
        {
            Angle += SteeringAngle;
        }
    }
}