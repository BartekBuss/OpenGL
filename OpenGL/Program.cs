using System;
using Tao.FreeGlut;
using OpenGL;
using System.Media;

namespace OpenGL
{
    class Program
    {
        private static int width = 850, height = 600;
        private static ShaderProgram program;
        private static VBO<Vector3> trojkat, kwadrat, kwadrat2, kwadrat3, kwadrat4, kwadrat5, kwadrat6, kwadrat7, kwadrat8;
        private static VBO<Vector3> trojkatKolor, kwadratKolor, kwadratKolor2, kwadratKolor3, kwadratKolor4, kwadratKolor5, kwadratKolor6, kwadratKolor7, kwadratKolor8;
        private static VBO<uint> trojkat3D, kwadrat3D, kwadrat23D, kwadrat33D, kwadrat43D;
        private static System.Diagnostics.Stopwatch zegar;
        private static double kat = -2.5, kat2 = -2.5;
        private static int status = 1, status1 = 1;
        private static SoundPlayer muzyka;
        private static double x1, x2, x3, x4, x5, x6, x7, y1, y2, y3, y4, y5, y6, y7, z1, z2, z3, z4, z5, z6, z7;
        private static double OdlX12, OdlY67;

        private static void update(int value)
        {
            x1 = kat;
            x2 = -1 * kat;
            x3 = kat;
            x4 = -1 * kat;
            x5 = 0;
            x6 = 1.5;
            x7 = 1.5;
            y1 = -1 * kat;
            y2 = kat;
            y3 = kat;
            y4 = -1 * kat;
            y5 = 0;
            y6 = kat;
            y7 = -1 * kat;
            z1 = -1 * kat;
            z2 = kat;
            z3 = -1 * kat;
            z4 = -1 * kat;
            z5 = kat;
            z6 = -1 * kat;
            z7 = kat;
            OdlX12 = Math.Abs(x1 - x2);
            OdlY67 = Math.Abs(y6 - y7);

            Glut.glutPostRedisplay();
            Glut.glutTimerFunc(1000 / 60, update, 0);

            switch (status)
            {
                case 1:
                    if (kat < 2.3 & OdlX12 > 0.4)
                    {
                        kat += 0.02;
                    }
                    else
                    {
                        status = -1;
                    }
                    break;
                case -1:
                    if (kat > -2.3)
                    {
                        kat -= 0.02;
                    }
                    else
                    {
                        status = 1;
                    }
                    break;
            }
            
            switch (status1)
            {
                case 1:
                    if (kat2 < 2.3 & OdlY67 > 0.4)
                    {
                        kat2 += 0.02;
                    }
                    else
                    {
                        status1 = -1;
                    }
                    break;
                case -1:
                    if (kat2 > -2.3)
                    {
                        kat2 -= 0.02;
                    }
                    else
                    {
                        status1 = 1;
                    }
                    break;
            }
        }
        
        static void Main(string[] args)
        {
            // create an OpenGL window
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("OpenGL");

            muzyka = new SoundPlayer(@"C:\Users\Bartosz\source\repos\OpenGL\OpenGL\plikmuz.wav");
            muzyka.Play();

            // provide the Glut callbacks that are necessary for running this tutorial
            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);
            Glut.glutCloseFunc(OnClose);
            
            // enable depth testing to ensure correct z-ordering of our fragments
            Gl.Enable(EnableCap.DepthTest);

            // compile the shader program
            program = new ShaderProgram(VertexShader, FragmentShader);

            // set the view and projection matrix, which are static throughout this tutorial
            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 100f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(5, 0, 14), Vector3.Zero, new Vector3(0, 1, 0)));

            trojkat = new VBO<Vector3>(new Vector3[] {
                new Vector3(0f, 0.5f, 0f), new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, -0.5f, 0.5f),        
                new Vector3(0f, 0.5f, 0f), new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0.5f, -0.5f, -0.5f),        
                new Vector3(0f, 0.5f, 0f), new Vector3(0.5f, -0.5f, -0.5f), new Vector3(-0.5f, -0.5f, -0.5f),      
                new Vector3(0f, 0.5f, 0f), new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-0.5f, -0.5f, 0.5f) });   
            trojkatKolor = new VBO<Vector3>(new Vector3[] {
                new Vector3(0, 0, 0), new Vector3(1, 50, 0), new Vector3(1, 50, 0),
                new Vector3(1, 255, 50), new Vector3(0, 0, 0), new Vector3(1, 255, 50),
                new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(255, 255, 255),
                new Vector3(0, 0, 70), new Vector3(0, 25, 70), new Vector3(0, 25, 90) });
            trojkat3D = new VBO<uint>(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}, BufferTarget.ElementArrayBuffer);

            kwadrat = new VBO<Vector3>(new Vector3[] {
                new Vector3(2.5f, 2.5f, -2.5f), new Vector3(-2.5f, 2.5f, -2.5f), new Vector3(-2.5f, 2.5f, 2.5f), new Vector3(2.5f, 2.5f, 2.5f),
                new Vector3(2.5f, -2.5f, 2.5f), new Vector3(-2.5f, -2.5f, 2.5f), new Vector3(-2.5f, -2.5f, -2.5f), new Vector3(2.5f, -2.5f, -2.5f),
                new Vector3(2.5f, -2.5f, -2.5f), new Vector3(-2.5f, -2.5f, -2.5f), new Vector3(-2.5f, 2.5f, -2.5f), new Vector3(2.5f, 2.5f, -2.5f),
                new Vector3(-2.5f, 2.5f, 2.5f), new Vector3(-2.5f, 2.5f, -2.5f), new Vector3(-2.5f, -2.5f, -2.5f), new Vector3(-2.5f, -2.5f, 2.5f),
                new Vector3(2.5f, 2.5f, -2.5f), new Vector3(2.5f, 2.5f, 2.5f), new Vector3(2.5f, -2.5f, 2.5f), new Vector3(2.5f, -2.5f, -2.5f) });
            kwadratKolor = new VBO<Vector3>(new Vector3[] {
                new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), //góra
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), //dół
                new Vector3(1, 0, 50), new Vector3(1, 0, 50), new Vector3(1, 0, 50), new Vector3(1, 0, 50), //tył
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), //lewa
                new Vector3(0, 38, 70), new Vector3(0, 38, 70), new Vector3(0, 38, 70), new Vector3(0, 38, 70) }); //prawa
            kwadrat3D = new VBO<uint>(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, BufferTarget.ElementArrayBuffer);

            kwadrat2 = new VBO<Vector3>(new Vector3[] {
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f),
                new Vector3(0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, -0.2f),
                new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, 0.2f),
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, 0.2f)});
            kwadratKolor2 = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0), //góra
                new Vector3(1, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 0, 0), //dół
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0), //tył
                new Vector3(1, 1, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0), //lewa
                new Vector3(0, 1, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 1), //prawa
                new Vector3(1, 0, 1), new Vector3(0, 0, 1), new Vector3(1, 1, 1), new Vector3(1, 0, 0)}); //przód

            kwadrat23D = new VBO<uint>(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, BufferTarget.ElementArrayBuffer);

            kwadrat3 = new VBO<Vector3>(new Vector3[] {
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f),
                new Vector3(0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, -0.2f),
                new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, 0.2f),
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, 0.2f)});
            kwadratKolor3 = new VBO<Vector3>(new Vector3[] {
                new Vector3(0, 255, 255), new Vector3(255, 255, 1), new Vector3(255, 1, 255), new Vector3(1, 255, 255), //góra
                new Vector3(0, 0, 70), new Vector3(1, 38, 70), new Vector3(0, 1, 70), new Vector3(0, 38, 70), //dół
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 0), //tył
                new Vector3(1, 1, 0), new Vector3(0, 1, 1), new Vector3(0, 1, 0), new Vector3(0, 1, 1), //lewa
                new Vector3(1, 0, 1), new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(0, 0, 0), //prawa
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0)}); //przód

            kwadrat33D = new VBO<uint>(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, BufferTarget.ElementArrayBuffer);

            kwadrat4 = new VBO<Vector3>(new Vector3[] {
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f),
                new Vector3(0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, -0.2f),
                new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, 0.2f),
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, 0.2f)});
            kwadratKolor4 = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //góra
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //dół
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), //tył
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //lewa
                new Vector3(0, 1, 1), new Vector3(0, 1, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 1), //prawa
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0)}); //przód

            kwadrat43D = new VBO<uint>(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, BufferTarget.ElementArrayBuffer);

            kwadrat5 = new VBO<Vector3>(new Vector3[] {
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f),
                new Vector3(0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, -0.2f),
                new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, 0.2f),
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, 0.2f)});
            kwadratKolor5 = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //góra
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //dół
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), //tył
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //lewa
                new Vector3(0, 1, 1), new Vector3(0, 1, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 1), //prawa
                new Vector3(1, 1, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(1, 0, 0)}); //przód

            kwadrat6 = new VBO<Vector3>(new Vector3[] {
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f),
                new Vector3(0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, -0.2f),
                new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, 0.2f),
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, 0.2f)});
            kwadratKolor6 = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //góra
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //dół
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), //tył
                new Vector3(1, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), //lewa
                new Vector3(0, 1, 1), new Vector3(0, 1, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 1), //prawa
                new Vector3(1, 0, 0), new Vector3(1, 0, 1), new Vector3(1, 1, 0), new Vector3(1, 0, 1)}); //przód

            kwadrat7 = new VBO<Vector3>(new Vector3[] {
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f),
                new Vector3(0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, -0.2f),
                new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, 0.2f),
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, 0.2f)});
            kwadratKolor7 = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), //góra
                new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), //dół
                new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), //tył
                new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), //lewa
                new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), //prawa
                new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 0, 1), new Vector3(0, 1, 1)}); //przód

            kwadrat8 = new VBO<Vector3>(new Vector3[] {
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f),
                new Vector3(0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, -0.2f),
                new Vector3(-0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, -0.2f), new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(-0.2f, -0.2f, 0.2f),
                new Vector3(0.2f, 0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, -0.2f),
                new Vector3(-0.2f, -0.2f, 0.2f), new Vector3(0.2f, -0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(-0.2f, 0.2f, 0.2f)});
            kwadratKolor8 = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0), //góra
                new Vector3(1, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 0, 0), //dół
                new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0), //tył
                new Vector3(1, 1, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0), //lewa
                new Vector3(0, 1, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 1), //prawa
                new Vector3(1, 0, 1), new Vector3(0, 0, 1), new Vector3(1, 1, 1), new Vector3(1, 0, 0)}); //przód

            zegar = System.Diagnostics.Stopwatch.StartNew();

            Glut.glutTimerFunc(0, update, 0);

            Glut.glutMainLoop();
        }

        private static void OnClose()
        {
            trojkat.Dispose();
            trojkatKolor.Dispose();
            trojkat3D.Dispose();
            kwadrat.Dispose();
            kwadratKolor.Dispose();
            kwadrat3D.Dispose();
            kwadrat2.Dispose();
            kwadratKolor2.Dispose();
            kwadrat23D.Dispose();
            kwadrat3.Dispose();
            kwadratKolor3.Dispose();
            kwadrat33D.Dispose();
            kwadrat4.Dispose();
            kwadratKolor4.Dispose();
            kwadrat43D.Dispose();
            kwadrat5.Dispose();
            kwadratKolor5.Dispose();
            kwadrat6.Dispose();
            kwadratKolor6.Dispose();
            kwadrat7.Dispose();
            kwadratKolor7.Dispose();
            kwadrat8.Dispose();
            kwadratKolor8.Dispose();
            program.DisposeChildren = true;
            program.Dispose();
            muzyka.Stop();
        }

        private static void OnDisplay()
        {
            
        }
        
        private static void OnRenderFrame()
        {
            // calculate how much time has elapsed since the last frame
            zegar.Stop();
             
            zegar.Restart();
            
            // use the deltaTime to adjust the angle of the cube and pyramid
            // set up the OpenGL viewport and clear both the color and depth bits
            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            // use our shader program
            Gl.UseProgram(program);
             
            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3 (-80, -80,-80)));
            Gl.BindBufferToShaderAttribute(trojkat, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(trojkatKolor, program, "vertexColor");
            Gl.BindBuffer(trojkat3D);

            Gl.DrawElements(BeginMode.Triangles, trojkat3D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            
            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(0, 0 , 0)));
            Gl.BindBufferToShaderAttribute(kwadrat, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(kwadratKolor, program, "vertexColor");
            Gl.BindBuffer(kwadrat3D);
            
            Gl.DrawElements(BeginMode.Quads, kwadrat3D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(x1,  y1, z1)));
            Gl.BindBufferToShaderAttribute(kwadrat2, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(kwadratKolor2, program, "vertexColor");
            Gl.BindBuffer(kwadrat23D);

            Gl.DrawElements(BeginMode.Quads, kwadrat23D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(x2,  y2, z2)));
            Gl.BindBufferToShaderAttribute(kwadrat3, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(kwadratKolor3, program, "vertexColor");
            Gl.BindBuffer(kwadrat33D);

            Gl.DrawElements(BeginMode.Quads, kwadrat33D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(x3, y3, z3)));
            Gl.BindBufferToShaderAttribute(kwadrat4, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(kwadratKolor4, program, "vertexColor");
            Gl.BindBuffer(kwadrat43D);

            Gl.DrawElements(BeginMode.Quads, kwadrat43D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(x4, y4, z4)));
            Gl.BindBufferToShaderAttribute(kwadrat5, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(kwadratKolor5, program, "vertexColor");
            Gl.BindBuffer(kwadrat43D);

            Gl.DrawElements(BeginMode.Quads, kwadrat43D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(x5, y5, z5)));
            Gl.BindBufferToShaderAttribute(kwadrat6, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(kwadratKolor6, program, "vertexColor");
            Gl.BindBuffer(kwadrat43D);

            Gl.DrawElements(BeginMode.Quads, kwadrat43D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(x6, y6, z6)));
            Gl.BindBufferToShaderAttribute(kwadrat7, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(kwadratKolor7, program, "vertexColor");
            Gl.BindBuffer(kwadrat43D);

            Gl.DrawElements(BeginMode.Quads, kwadrat43D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(x7, y7, z7)));
            Gl.BindBufferToShaderAttribute(kwadrat8, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(kwadratKolor8, program, "vertexColor");
            Gl.BindBuffer(kwadrat43D);

            Gl.DrawElements(BeginMode.Quads, kwadrat43D.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Glut.glutSwapBuffers();
        }
        
        public static string VertexShader = @"
#version 130

in vec3 vertexPosition;
in vec3 vertexColor;

out vec3 color;

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

void main(void)
{
    color = vertexColor;
    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
}
";

        public static string FragmentShader = @"
#version 130

in vec3 color;

out vec4 fragment;

void main(void)
{
    fragment = vec4(color, 1);
}
";
    }
}
