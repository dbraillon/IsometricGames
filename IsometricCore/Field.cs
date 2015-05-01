using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric.Core
{
    public class Field
    {
        public const int EMPTY_CUBE = 0;

        public int XSize { get; set; }
        public int YSize { get; set; }
        public int ZSize { get; set; }
        public int[, ,] FieldArray { get; set; }

        public Field(int xSize, int ySize, int zSize)
        {
            XSize = xSize;
            YSize = ySize;
            ZSize = zSize;

            FieldArray = new int[XSize, YSize, ZSize];
            FillWithEmptyCube();
        }

        public void FillWithEmptyCube()
        {
            for (int z = 0; z < FieldArray.GetLength(2); z++)
            {
                for (int y = 0; y < FieldArray.GetLength(1); y++)
                {
                    for (int x = 0; x < FieldArray.GetLength(0); x++)
                    {
                        FieldArray[x, y, z] = EMPTY_CUBE;
                    }
                }
            }
        }

        public void FillWithCube(int cube)
        {
            for (int z = 0; z < FieldArray.GetLength(2); z++)
            {
                for (int y = 0; y < FieldArray.GetLength(1); y++)
                {
                    for (int x = 0; x < FieldArray.GetLength(0); x++)
                    {
                        FieldArray[x, y, z] = cube;
                    }
                }
            }
        }

        public void Load2dFieldFile(FileInfo fieldFile)
        {
            if (!fieldFile.Exists)
            {
                throw new ArgumentException("File doesn't exists.");
            }

            string[] fieldLines = File.ReadAllLines(fieldFile.FullName);
            for (int y = 0; y < YSize; y++)
            {
                string fieldLine = fieldLines[y];
                for (int x = 0; x < XSize; x++)
                {
                    string cubeString = fieldLine.Substring(x, 1);
                    int cube = -1;

                    if (!int.TryParse(cubeString, out cube))
                    {
                        throw new ArgumentException(string.Format("The char {0} is an invalid cube integer.", cubeString));
                    }

                    SetCube(new Point(x, y), cube);
                }
            }
        }

        public void SetCube(Point point, int cube)
        {
            if (point.X < 0 || point.X > XSize)
            {
                throw new ArgumentOutOfRangeException("x");
            }

            if (point.Y < 0 || point.Y > YSize)
            {
                throw new ArgumentOutOfRangeException("y");
            }

            if (point.Z < 0 || point.Z > ZSize)
            {
                throw new ArgumentOutOfRangeException("z");
            }

            FieldArray[point.X, point.Y, point.Z] = cube;
        }
    }
}
