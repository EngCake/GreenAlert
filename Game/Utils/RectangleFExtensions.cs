using Microsoft.Xna.Framework;
using Nez;

namespace Game.Utils;

public static class RectangleFExtensions
{
    // Extension method to calculate the minimum translation vector
    public static Vector2 GetMinimumTranslationVector(this RectangleF rect1, RectangleF rect2)
    {
        // Calculate the half widths and half heights of both rectangles
        float halfWidth1 = rect1.Width / 2f;
        float halfHeight1 = rect1.Height / 2f;
        float halfWidth2 = rect2.Width / 2f;
        float halfHeight2 = rect2.Height / 2f;

        // Calculate the centers of both rectangles
        Vector2 center1 = new Vector2(rect1.X + halfWidth1, rect1.Y + halfHeight1);
        Vector2 center2 = new Vector2(rect2.X + halfWidth2, rect2.Y + halfHeight2);

        // Calculate the distance between the centers along both axes
        float deltaX = center2.X - center1.X;
        float deltaY = center2.Y - center1.Y;

        // Calculate the minimum translation vector components along both axes
        float offsetX = Math.Abs(deltaX) - (halfWidth1 + halfWidth2);
        float offsetY = Math.Abs(deltaY) - (halfHeight1 + halfHeight2);

        // Initialize the translation vector
        float translationX = 0;
        float translationY = 0;

        // If there is a collision, calculate the minimum translation vector
        if (offsetX < 0 && offsetY < 0)
        {
            // Determine which direction to move the rectangle to separate them
            if (Math.Abs(offsetX) < Math.Abs(offsetY))
            {
                // Collision on X-axis
                translationX = deltaX < 0 ? -offsetX : offsetX;
            }
            else
            {
                // Collision on Y-axis
                translationY = deltaY < 0 ? -offsetY : offsetY;
            }
        }

        // Construct and return the minimum translation vector
        return new Vector2(translationX, translationY);
    }
}
