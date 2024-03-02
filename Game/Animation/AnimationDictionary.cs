using Nez.Sprites;
using System.Collections;

namespace Game.Animation;

internal class AnimationDictionary : IEnumerable<Tuple<string, SpriteAnimation>>
{
    private readonly Dictionary<string, SpriteAnimation> animations = new();

    public string? DefaultAnimation { get; set; }

    public SpriteAnimation? this[string animationName]
    {
        get => animations[animationName];

        set
        {
            animations[animationName] = value ?? throw new ArgumentNullException($"Attempting to set animation '{animationName}' to null");
            if (animations.Count == 1)
            {
                DefaultAnimation = animationName;
            }
        }
    }

    public IEnumerator<Tuple<string, SpriteAnimation>> GetEnumerator()
    {
        foreach (var (key, value) in animations)
        {
            yield return new Tuple<string, SpriteAnimation>(key, value);
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
}
