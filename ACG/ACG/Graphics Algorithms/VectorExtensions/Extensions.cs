using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using GraphicsModeler.Matrixes;

namespace GraphicsModeler.Extensions
{
    public static class Extensions
    {
        public static Vector4 TransformBy(this Vector4 vector, Matrix4x4 matrix)
        {
            return Vector4.Transform(vector, matrix);
        }
    }
}
