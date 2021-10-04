using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;

namespace FaceDetector
{
    public class FaceSearcher
    {
        public FaceSearcher()
        {
            
        }

        public bool Detect(string imagePath)
        {
            using (var fd = Dlib.GetFrontalFaceDetector())
            {
                var img = Dlib.LoadImage<RgbPixel>(imagePath);

                // find all faces in the image
                var faces = fd.Operator(img);
                foreach (var face in faces)
                {
                    // draw a rectangle for each face
                    Dlib.DrawRectangle(img, face, color: new RgbPixel(0, 255, 255), thickness: 4);
                }
                Dlib.SaveJpeg(img,@"C:\Users\PenisDeathRow\Desktop\test.jpg");

                return faces.Length > 0;
            }

            

            
        }

        
    }
}
