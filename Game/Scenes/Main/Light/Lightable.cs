using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace Game.Scenes.Main.Light;

internal class Lightable : Component
{
    private static readonly float minimumLightIntensity = 0.3f;

    private SpriteRenderer renderer;

    private Dictionary<float, int> lightIntensities = new Dictionary<float, int>();

    public float LightIntensity
    {
        get
        {
            if (lightIntensities.Count > 0)
            {
                return Math.Max(lightIntensities.Keys.Max(), minimumLightIntensity);
            }
            return minimumLightIntensity;
        }
    }

    public void AddLight(float lightIntensity)
    {
        if (!lightIntensities.ContainsKey(lightIntensity))
        {
            lightIntensities[lightIntensity] = 0;
        }
        lightIntensities[lightIntensity]++;
        SetRendererColor();
    }

    private void SetRendererColor()
    {
        var intensity = LightIntensity;
        renderer.Color = new Color(intensity, intensity, intensity);
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
