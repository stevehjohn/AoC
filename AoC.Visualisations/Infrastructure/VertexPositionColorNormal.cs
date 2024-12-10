using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace AoC.Visualisations.Infrastructure;

public struct VertexPositionColorNormal : IVertexType
{
    [UsedImplicitly] 
    public Vector3 Position;
    [UsedImplicitly] 
    public Color Color;
    [UsedImplicitly] 
    public Vector3 Normal;

    public VertexPositionColorNormal(Vector3 position, Color color, Vector3 normal)
    {
        Position = position;
        Color = color;
        Normal = normal;
    }

    public static readonly VertexDeclaration VertexDeclaration = new
    (
        new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
        new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
        new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
    );

    VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
}