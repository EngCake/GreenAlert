using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace Game.Components.Light;

internal class Lightable : Component
{
    private SpriteRenderer renderer;

    private Dictionary<float, int> lightIntensities = new Dictionary<float, int>();

    public float LightIntensity => lightIntensities.Count > 0 ? lightIntensities.Keys.Max() : 0;

    public void AddLight(float lightIntensity)
    {
        lightIntensities.TryGetValue(lightIntensity, out var count);
        lightIntensities[lightIntensity] = count + 1;
        SetRendererColor();
    }

    private void SetRendererColor()
    {
        renderer.Color = Color.White; return;
        var intensity = LightIntensity;
        renderer.Color = new Color(intensity, intensity, intensity, intensity);
    }

    public void RemoveLight(float lightIntensity)
    {
        lightIntensities[lightIntensity]--;
        if (lightIntensities[lightIntensity] == 0)
        {
            lightIntensities.Remove(lightIntensity);
        }
        SetRendererColor();
    }

    public Lightable(SpriteRenderer renderer)
    {
        this.renderer = renderer;
        SetRendererColor();
    }
}
