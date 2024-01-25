using System;

namespace Karts.Engine;

public class Point2D
{
    private readonly double _x;

    private readonly double _y;

    public double X => _x;

    public double Y => _y;
    
    public Point2D(double x, double y)
    {
        _x = x;
        
        _y = y;
    }

    public Point2D MoveBy(double angle, double distance)
    {
        return new Point2D(_x + Math.Cos(angle) * distance, _y + Math.Sin(angle) * distance);
    }

    public static Point2D operator + (Point2D left, Point2D right)
    {
        return new Point2D(left.X + right.X, left.Y + right.Y);
    }

    public static Point2D operator - (Point2D left, Point2D right)
    {
        return new Point2D(left.X - right.X, left.Y - right.Y);
    }

    public static Point2D operator / (Point2D left, double right)
    {
        return new Point2D(left.X / right, left.Y / right);
    }

    public static Point2D operator * (Point2D left, double right)
    {
        return new Point2D(left.X * right, left.Y * right);
    }
}