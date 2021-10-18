using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using GraphicsModeler.Extensions;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicsModeler.Helper
{
    public class Model
    {
        public Vector3 Rotation = new Vector3();
        public float ScaleFactor = 1f;
        public Vector3 Translation = new Vector3();
        public Matrix4x4 PerspectiveMatrix = new Matrix4x4(1, 0, 0, 0,
                                                                         0, 1, 0, 0,
                                                                         0, 0, 1, 0,
                                                                         0, 0, 0, 1);


        public Model(List<List<int>> polygons, List<Vector4> vertexes)
        {
            Polygons = polygons;
            Vertexes = vertexes;
        }
        public List<List<int>> Polygons { get; set; }
        public List<Vector4> Vertexes { get; set; }
        public void DrawToBitmap(ExtendedBitmap bmp)
        {
            bmp.LockBits();
            Parallel.ForEach(Polygons, p =>
            {
                for (var i = 0; i < p.Count - 1; i++)
                {
                    var line = GetDDALine(GetCalculatedPoint(Vertexes[p[i]]), GetCalculatedPoint(Vertexes[p[i + 1]]));
                    foreach (var pt in line)
                        bmp[(int)pt.X, (int)pt.Y] = Color.FromArgb(255, Color.Green);
                }

                if (p.Count > 0)
                    foreach (var pt in GetDDALine(GetCalculatedPoint(Vertexes[p.First()]), GetCalculatedPoint(Vertexes[p.Last()])))
                        bmp[(int)pt.X, (int)pt.Y] = Color.FromArgb(255, Color.Green);
            });
            bmp.UnlockBits();
        }
        
        
        
        private Vector4 GetCalculatedPoint(Vector4 vector)
        {
            var result = vector;
            var completeMatrix = Matrixes.Matrixes.GetRotateMatrix(Rotation.X, Rotation.Y, Rotation.Z) *
                Matrixes.Matrixes.GetScaleMatrix(ScaleFactor) *
                Matrixes.Matrixes.GetTranslationMatrix(Translation.X, Translation.Y, Translation.Z) *
                Matrixes.Matrixes.GetCamera(new Vector3(0, 0, 500),
                                            new Vector3(0, 0, 0),
                                            new Vector3(0, 1f, 0)) * 
                Matrixes.Matrixes.GetPerspective((float)(Math.PI), Translation.X / Translation.Y, 0.1f, 200f);

            result = result.TransformBy(completeMatrix);
            result /= result.W;
            result = result.TransformBy(Matrixes.Matrixes.GetViewMatrix(Translation.X, Translation.Y * 2));
            return result;
        }

        private IEnumerable<Vector4> GetDDALine(Vector4 vector1, Vector4 vector2)
        {
            var dx = vector2.X - vector1.X;
            var dy = vector2.Y - vector1.Y;
            var L = Math.Max(Math.Abs(dx), Math.Abs(dy));
            var xInc = dx / L;
            var yInc = dy / L;
            var x = vector1.X;
            var y = vector1.Y;

            for (int i = 0; i <= L; i++)
            {
                yield return new Vector4(x, y, 0, 1);
                x += xInc;
                y += yInc;
            }
        }
    }
}
