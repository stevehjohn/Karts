using System;

namespace Karts.Engine;

public class Point2D
{
    public double X { get; }

    public double Y { get; }

    public Point2D(double x, double y)
    {
        X = x;
        
        Y = y;
    }

    public Point2D MoveBy(double angle, double distance)
    {
        return new Point2D(X + Math.Cos(angle) * distance, Y + Math.Sin(angle) * distance);
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