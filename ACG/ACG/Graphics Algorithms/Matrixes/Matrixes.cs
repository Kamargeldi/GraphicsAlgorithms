using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GraphicsModeler.Matrixes
{
    public static class Matrixes
    {
        public static Matrix4x4 GetRotateMatrix(float xDegree, float yDegree, float zDegree)
        {
            return Matrix4x4.CreateRotationX(xDegree) * Matrix4x4.CreateRotationY(yDegree) * Matrix4x4.CreateRotationZ(zDegree);
        }

        public static Matrix4x4 GetCamera(Vector3 position, Vector3 target, Vector3 up)
        {
            return Matrix4x4.CreateLookAt(position, target, up);
        }

        public static Matrix4x4 GetPerspective(float fOV, float aspectRatio, float near, float far)
        {
            return Matrix4x4.CreatePerspectiveFieldOfView(fOV / 3f, aspectRatio, near, far);
        }

        public static Matrix4x4 GetViewMatrix(float width, float height, float Xmin = 0, float Ymin = 0)
        {
            return new Matrix4x4(
                width / 2, 0, 0, 0,
                0, -height / 2, 0, 0,
                0, 0, 1, 0,
                Xmin + width / 2, Ymin + height / 2, 0, 1
            );
        }

        public static Matrix4x4 GetTranslationMatrix(float deltaX, float deltaY, float deltaZ)
        {
            return Matrix4x4.CreateTranslation(new Vector3(deltaX, deltaY, deltaZ));
        }

        public static Matrix4x4 GetScaleMatrix(float scaleCofficient)
        {
            return Matrix4x4.CreateScale(scaleCofficient);
        }
    }
}
