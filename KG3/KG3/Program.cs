using System;
using Tao.FreeGlut;
using OpenGL;

namespace KG3
{
    class Program
    {
        public static int width = 1280;
        public static int height = 720;

        private static ShaderProgram pr;

        //Пирамида 
        private static VBO<Vector3> Pyramid;
        private static VBO<Vector3> PyramidDno;
        private static VBO<Vector3> PColor;
        private static VBO<Vector3> PDColor;
        private static VBO<uint> Triangles;
        private static VBO<uint> PyramidQuad;

         //Куб 
        private static VBO<Vector3> Cube;
        //private static VBO<Vector3> CColor;
        private static VBO<uint> CubeQuads;

        //Для вращения  
        private static float xa, ya, za;
        private static bool left, right, up, down;
        private static bool closer, further; // Ближе и дальше 
        private static bool auto = true; // Запрограммированное вращение 

        private static bool lighting = false; // Освещение 

        private static System.Diagnostics.Stopwatch timer; // для вращения 

        // Текстура куба 
        private static Texture texture; 
        private static VBO<Vector2> CubeT;
        private static VBO<Vector3> CubeN;

        //Сфера  
        private static VBO<Vector3> Sphere;
        private static VBO<uint> SphereTrapezoids;
        private static Texture textureSphere;
        private static VBO<Vector2> SphereT;
        private static VBO<Vector3> SphereN;

        //Трапеция 
        private static VBO<Vector3> Trapezoid;
        private static VBO<uint> Trapezoids;

        static void Main(string[] args)
        {
            //Создаем окно OpenGL
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("KG3");

            //Функция, которая вызывается, когда приложение простаивает
            Glut.glutIdleFunc(OnRenderFrame);
            //Рисование и перерисование окна
            Glut.glutDisplayFunc(OnDisplay);

            //Взаимодействие с клавиатурой
            Glut.glutKeyboardFunc(OnKeyboard);
            Glut.glutKeyboardUpFunc(OffKeyboard);

            //Устанавливает обратный вызов закрытия окна. freeglut вызывает обратный вызов закрытия, когда окно вот-вот будет уничтожено.
            Glut.glutCloseFunc(OnClose);

            //Включим тест глубины для правильного z-порядка фрагментов 
            Gl.Disable(EnableCap.DepthTest);
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //Скомпилируем шейдерную программу 
            pr = new ShaderProgram(VertexShader, FragmentShader);

            //Установим камеру и матрицу проекции
            pr.Use();
            pr["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f));
            pr["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 35), new Vector3(0, 0, 0), new Vector3(0, 1, 0)));

            //Освещение
            pr["light_direction"].SetValue(new Vector3(1, 1, 0));
            pr["enable_lighting"].SetValue(lighting);

            //Для материала 
            pr["FragPos"].SetValue(new Vector3(0, 0, 1));// положение зрителя
            pr["lightPos"].SetValue(new Vector3(0, 100, 1)); // положение источника света 


            //Текстура
            texture = new Texture("Texture.jpeg");
            textureSphere = new Texture("Texture2.jpg");

            // Пирамида
            BuildPyramid();
            // Куб
            BuildCube();
            //Сфера
            BuildSphere();
            //Трапеция
            BuildTrapezoid();

            timer = System.Diagnostics.Stopwatch.StartNew();

            //Вход в главный цикл GLUT.
            Glut.glutMainLoop();
        }

        //Пирамида 
        private static void BuildPyramid()
        {
            Pyramid = new VBO<Vector3>(new Vector3[] {
                new Vector3(0, 1, 0), new Vector3(-1, -1, 1), new Vector3(1, -1, 1),        // лицевая
                new Vector3(0, 1, 0), new Vector3(1, -1, 1), new Vector3(1, -1, -1),        // правая
                new Vector3(0, 1, 0), new Vector3(1, -1, -1), new Vector3(-1, -1, -1),      // задняя
                new Vector3(0, 1, 0), new Vector3(-1, -1, -1), new Vector3(-1, -1, 1) });    // левая
            
            PColor = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 0.5f, 1), new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0),
                new Vector3(1, 0.5f, 1), new Vector3(1, 0.5f, 0.6f), new Vector3(1, 0.5f, 0),
                new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0.7f), new Vector3(1, 0.5f, 0),
                new Vector3(1, 0.5f, 1), new Vector3(0.8f, 0.5f, 0), new Vector3(1, 0.5f, 1)});
             
            Triangles = new VBO<uint>(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, BufferTarget.ElementArrayBuffer);

            PyramidDno = new VBO<Vector3>(new Vector3[] { new Vector3(-1, -1, 1), new Vector3(1, -1, 1), new Vector3(1, -1, -1), new Vector3(-1, -1, -1) });//Низ
            PDColor = new VBO<Vector3>(new Vector3[] { new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0)});
            PyramidQuad = new VBO<uint>(new uint[] {0, 1, 2, 3}, BufferTarget.ElementArrayBuffer);
        }

        private static void DrawPyramid()
        {
           // pr["enable_lighting"].SetValue(lighting);
            //Составление пирамиды и ее отрисовка 
            pr["model_matrix"].SetValue( Matrix4.CreateTranslation(new Vector3((float)(r), (float)(r), (float)r)) *
                 Matrix4.CreateRotationY(ya) * Matrix4.CreateRotationZ(xa));
            pr["enable_texture"].SetValue(false);
            pr["enable_lighting"].SetValue(lighting);
            pr["enable_material"].SetValue(true);
            MaterialEmerald();

            Gl.BindBufferToShaderAttribute(Pyramid, pr, "vertexPosition");
            Gl.BindBufferToShaderAttribute(PColor, pr, "vertexColor");
            Gl.BindBuffer(Triangles);
            Gl.DrawElements(BeginMode.Triangles, Triangles.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Gl.BindBufferToShaderAttribute(PyramidDno, pr, "vertexPosition");
            Gl.BindBufferToShaderAttribute(PDColor, pr, "vertexColor");
            Gl.BindBuffer(PyramidQuad);
            Gl.DrawElements(BeginMode.Quads, PyramidQuad.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        //Куб
        private static void BuildCube()
        {
            Cube = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 1, -1), new Vector3(-1, 1, -1), new Vector3(-1, 1, 1), new Vector3(1, 1, 1),         
                new Vector3(1, -1, 1), new Vector3(-1, -1, 1), new Vector3(-1, -1, -1), new Vector3(1, -1, -1),     
                new Vector3(1, 1, 1), new Vector3(-1, 1, 1), new Vector3(-1, -1, 1), new Vector3(1, -1, 1),         
                new Vector3(1, -1, -1), new Vector3(-1, -1, -1), new Vector3(-1, 1, -1), new Vector3(1, 1, -1),     
                new Vector3(-1, 1, 1), new Vector3(-1, 1, -1), new Vector3(-1, -1, -1), new Vector3(-1, -1, 1),     
                new Vector3(1, 1, -1), new Vector3(1, 1, 1), new Vector3(1, -1, 1), new Vector3(1, -1, -1) });     
            CubeN = new VBO<Vector3>(new Vector3[] {
                new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), 
                new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0), 
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), 
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), 
                new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), 
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0) });
            CubeT = new VBO<Vector2>(new Vector2[] {
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) });

            CubeQuads = new VBO<uint>(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, BufferTarget.ElementArrayBuffer);

            /*
            CColor = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 0.8f, 0.5f), new Vector3(1, 0.1f, 0.5f), new Vector3(1, 0.7f, 0.5f), new Vector3(1, 0, 0.5f), 
                new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0),
                new Vector3(1, 0, 0.5f), new Vector3(1, 0, 0.5f), new Vector3(1, 0, 0.5f), new Vector3(1, 0, 0.5f),
                new Vector3(1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0.5f), 
                new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0.6f), new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0.3f),
                new Vector3(1, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0.5f, 1), new Vector3(1, 0, 1) });
             */
            
        }

        private static void DrawCube()
        {
            Gl.BindTexture(texture);
            //Составление куба и его отрисовка 
            pr["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3((float)(r), (float)(r), (float)r)) *
                 Matrix4.CreateRotationZ(xa) * Matrix4.CreateRotationY(ya));

            pr["enable_texture"].SetValue(true);
            pr["enable_lighting"].SetValue(lighting);
            pr["enable_material"].SetValue(false);
            Gl.BindBufferToShaderAttribute(Cube, pr, "vertexPosition");
            Gl.BindBufferToShaderAttribute(CubeN, pr, "vertexNormal");
            Gl.BindBufferToShaderAttribute(CubeT, pr, "vertexUV");
            Gl.BindBuffer(CubeQuads);
            Gl.DrawElements(BeginMode.Quads, CubeQuads.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        //Сфера 
        public static double r = 4; // Радиус сферы
        public static int nx = 16; // Число разбиений сферы по X и Y
        public static int ny = 16;

        private static void BuildSphere()
        {
            // r - радиус сферы
            // nx - число полигонов (сегментов) по X
            // ny - число полигонов (сегментов) по Y
            int ix, iy;
            double x, y, z, sy, cy, sy1, cy1, sx, cx, piy, pix, ay, ay1, ax, tx, ty, ty1, dnx, dny, diy;
            dnx = 1.0 / (double)nx;
            dny = 1.0 / (double)ny;

            int size = 2 * (nx * ny + nx);
            Vector3[] S = new Vector3[size];
            Vector3[] N = new Vector3[size];
            Vector2[] T = new Vector2[size];

            // Рисуем полигональную модель сферы
            // Каждый полигон - это трапеция. Трапеции верхнего и нижнего слоев вырождаются в треугольники
            piy = Math.PI * dny;
            pix = Math.PI * dnx;
            int col = 0; 
            for (iy = 0; iy < ny; iy++)
            {
                diy = (double)iy;
                ay = diy * piy;
                sy = Math.Sin(ay);
                cy = Math.Cos(ay);
                ty = diy * dny;

                ay1 = ay + piy;
                sy1 = Math.Sin(ay1);
                cy1 = Math.Cos(ay1);
                ty1 = ty + dny;

                for (ix = 0; ix <= nx; ix++)
                {
                    ax = 2.0 * ix * pix;
                    sx = Math.Sin(ax);
                    cx = Math.Cos(ax);
                    tx = (double)ix * dnx;

                    x = r * sy * cx;
                    y = r * sy * sx;
                    z = r * cy;

                    // Координаты нормали в текущей вершине. Нормаль направлена от центра
                    // Координаты текстуры в текущей вершине
                    S[col] = new Vector3((float)x, (float)y, (float)z);
                    N[col] = new Vector3((float)x, (float)y, (float)z);
                    T[col] = new Vector2((float)tx, (float)ty);
                    col++; 

                    x = r * sy1 * cx;
                    y = r * sy1 * sx;
                    z = r * cy1;

                    S[col] = new Vector3((float)x, (float)y, (float)z);
                    N[col] = new Vector3((float)x, (float)y, (float)z);
                    T[col] = new Vector2((float)tx, (float)ty1);
                    col++;
                }
            }

            Sphere = new VBO<Vector3>(S);
            SphereN = new VBO<Vector3>(N);
            SphereT = new VBO<Vector2>(T);
            uint[] Posled = new uint[size];
            for (int i = 0; i < size; i++)
                Posled[i] = (uint)i; 

            SphereTrapezoids = new VBO<uint>(Posled, BufferTarget.ElementArrayBuffer);
        }

        private static void DrawSphere()
        {
            Gl.BindTexture(textureSphere);
            pr["model_matrix"].SetValue(Matrix4.CreateRotationZ(ya) * Matrix4.CreateRotationY(ya) * Matrix4.CreateRotationX(xa) * Matrix4.CreateTranslation(new Vector3(0, 0, (Single)za)));
            pr["enable_texture"].SetValue(true);
            pr["enable_lighting"].SetValue(lighting);
            pr["enable_material"].SetValue(false);
            Gl.BindBufferToShaderAttribute(Sphere, pr, "vertexPosition");
            Gl.BindBufferToShaderAttribute(SphereN, pr, "vertexNormal");
            Gl.BindBufferToShaderAttribute(SphereT, pr, "vertexUV");
            Gl.BindBuffer(SphereTrapezoids);
            Gl.DrawElements(BeginMode.Quads, SphereTrapezoids.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        //Трапеция 
        private static void BuildTrapezoid()
        {
            Trapezoid = new VBO<Vector3>(new Vector3[] {
                new Vector3(1.5f, 1f, -1f), new Vector3(-1.5f, 1f, -1f), new Vector3(-1.5f, 1f, 1f), new Vector3(1.5f, 1f, 1f),   //Верх      
                new Vector3(3f, 0, 1f), new Vector3(-3f, 0, 1f), new Vector3(-3f, 0, -1f), new Vector3(3f, 0, -1f),     //Низ
                new Vector3(1.5f, 1f, 1f), new Vector3(-1.5f, 1f, 1f), new Vector3(-3f, 0, 1f), new Vector3(3f, 0, 1f),   // Лицевая сторона       
                new Vector3(3f, 0, -1f), new Vector3(-3f, 0, -1f), new Vector3(-1.5f, 1, -1f), new Vector3(1.5f, 1, -1f),     //Задняя сторона 
                new Vector3(-1.5f, 1, 1f), new Vector3(-1.5f, 1, -1f), new Vector3(-3f, 0, -1f), new Vector3(-3f, 0, 1f),     //Левая сторона
                new Vector3(1.5f, 1, -1f), new Vector3(1.5f, 1, 1f), new Vector3(3f, 0, 1f), new Vector3(3f, 0, -1f) }); //Правая сторона 
            /*
            TColor = new VBO<Vector3>(new Vector3[] {
                new Vector3(1, 0.8f, 0.5f), new Vector3(1, 0.1f, 0.5f), new Vector3(1, 0.7f, 0.5f), new Vector3(1, 0, 0.5f), 
                new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0),
                new Vector3(1, 0, 0.5f), new Vector3(1, 0, 0.5f), new Vector3(1, 0, 0.5f), new Vector3(1, 0, 0.5f),
                new Vector3(1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 0.5f), 
                new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0.6f), new Vector3(1, 0.5f, 0), new Vector3(1, 0.5f, 0.3f),
                new Vector3(1, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0.5f, 1), new Vector3(1, 0, 1) });
            */

            Trapezoids = new VBO<uint>(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, BufferTarget.ElementArrayBuffer);

        }

        private static void DrawCTrapezoid()
        {
            //Gl.BindTexture(texture);
            pr["model_matrix"].SetValue(Matrix4.CreateRotationY(ya) * Matrix4.CreateTranslation(new Vector3(0, (float)(-r - 1), (Single)za)));
            pr["enable_texture"].SetValue(false);
            pr["enable_lighting"].SetValue(lighting);
            pr["enable_material"].SetValue(true);
            MaterialSilver();
            Gl.BindBufferToShaderAttribute(Trapezoid, pr, "vertexPosition");
            //Gl.BindBufferToShaderAttribute(TColor, pr, "vertexColor");
            Gl.BindBuffer(Trapezoids);
            Gl.DrawElements(BeginMode.Quads, Trapezoids.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }


        //
        private static void OnDisplay()
        {

        }

        private static void OnRenderFrame()
        {
            timer.Stop();
            float deltaT = (float)timer.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency;
            timer.Restart();

            if (auto == false)
            {
                if (right) ya += deltaT;
                if (left) ya -= deltaT;
                if (up) xa -= deltaT;
                if (down) xa += deltaT;
            }
            else
            {
                xa += deltaT;
                ya += deltaT;
            }

            if (further) za -= 2 * deltaT;
            if (closer) za += 2 * deltaT; 

            // Настройка окна просмотра OpenGL и очистка битов, цвета и глубины
            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //
            Gl.UseProgram(pr);

            //Пирамида 
            DrawPyramid();

            //Куб 
            DrawCube();

            //Сфера 
            DrawSphere();

            //Трапеция 
            DrawCTrapezoid();
            
            Glut.glutSwapBuffers();
        }

        private static void OnClose()
        {
            //Уничтожаем все, что было создано   
            Pyramid.Dispose();
            PColor.Dispose();
            Triangles.Dispose();

            PyramidDno.Dispose();
            PDColor.Dispose();
            PyramidQuad.Dispose();

            Cube.Dispose();
            //CColor.Dispose();
            CubeN.Dispose();
            CubeT.Dispose();
            CubeQuads.Dispose();
            texture.Dispose();

            Sphere.Dispose();
            SphereN.Dispose();
            SphereT.Dispose();
            SphereTrapezoids.Dispose();
            textureSphere.Dispose();

            Trapezoid.Dispose();
            Trapezoids.Dispose();

            pr.DisposeChildren = true;
            pr.Dispose();       
        }

        private static void OnKeyboard(byte key, int x, int y)
        {
            
            if (key == 'u') auto = !auto;
            else if (key == 'w') up = true;
            else if (key == 's') down = true;
            else if (key == 'd') right = true;
            else if (key == 'a') left = true;
            else if (key == 'q') further = true;
            else if (key == 'e') closer = true;
            else if (key == 'l') lighting = !lighting;
            else if (key == 0) Glut.glutLeaveMainLoop();
        }

        private static void OffKeyboard(byte key, int x, int y)
        {

            if (key == 'w') up = false;
            else if (key == 's') down = false;
            else if (key == 'd') right = false;
            else if (key == 'a') left = false;
            else if (key == 'q') further = false;
            else if (key == 'e') closer = false;
            //else if (key == 'l') lighting = !lighting;
        }
        
        //Материалы 
        private static void MaterialSilver() // Серебро 
        {
            /*
                vec3 ambientM; //окпужающий свет
                vec3 diffuseM; // рассеянный 
                vec3 specularM; // бликовый 
                float shininessM; // сила блеска 
             */
            pr["ambientM"].SetValue(new Vector3(0.19f, 0.19f, 0.19f));
            pr["diffuseM"].SetValue(new Vector3(0.5f, 0.5f, 0.5f));
            pr["specularM"].SetValue(new Vector3(0.5f, 0.5f, 0.5f));
            pr["shininessM"].SetValue(0.4f);

        }

        private static void MaterialEmerald() // Изумрудный  
        {
            /*
                vec3 ambientM; //окпужающий свет
                vec3 diffuseM; // рассеянный 
                vec3 specularM; // бликовый 
                float shininessM; // сила блеска 
             */
            pr["ambientM"].SetValue(new Vector3(0.0215f, 0.1745f, 0.0215f));
            pr["diffuseM"].SetValue(new Vector3(0.07568f,	0.61424f,	0.07568f));
            pr["specularM"].SetValue(new Vector3(0.633f,	0.727811f,	0.633f));
            pr["shininessM"].SetValue(0.6f);

        }

        private static void MaterialObsidian() // Обсидиан  
        {
            /*
                vec3 ambientM; //окпужающий свет
                vec3 diffuseM; // рассеянный 
                vec3 specularM; // бликовый 
                float shininessM; // сила блеска 
             */
            pr["ambientM"].SetValue(new Vector3(0.05375f,	0.05f,	0.06625f));
            pr["diffuseM"].SetValue(new Vector3(0.18275f,	0.17f,	0.22525f));
            pr["specularM"].SetValue(new Vector3(0.332741f,	0.328634f,	0.346435f));
            pr["shininessM"].SetValue(0.3f);

        }
        //

        public static string VertexShader = @"
#version 130

in vec3 vertexPosition;
in vec3 vertexColor;
in vec3 vertexNormal;
in vec2 vertexUV;

out vec3 color;
out vec3 normal;
out vec3 position;
out vec2 uv;
out vec3 FragPos;

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

void main(void)
{
    color = vertexColor;

    normal = normalize((model_matrix * vec4(floor(vertexNormal), 0)).xyz);
    uv = vertexUV;
    FragPos = vec3(model_matrix * vec4(position, 1.0f));

    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
    position = gl_Position.xyz;
}
";

        public static string FragmentShader = @"
#version 130

in vec3 normal;
in vec2 uv;
in vec3 color;
in vec3 position;

out vec4 fragment;

uniform sampler2D texture;
uniform bool enable_texture;

uniform vec3 light_direction;
uniform bool enable_lighting;

uniform bool enable_material;

//Материал 
    uniform vec3 ambientM; //окружающий свет
    uniform vec3 diffuseM; // рассеянный 
    uniform vec3 specularM; // бликовый 
    uniform float shininessM; // сила блеска 

// Mы определяем цветовой вектор для каждого компонента освещения по Фонгу.
// Вектор ambient определяет, какой цвет объект отражает под фоновым освещением. Обычно это цвет самого объекта. 
// Вектор diffuse определяет цвет объекта под рассеянным освещением. Также, как и фоновый, он определяет желаемый цвет объекта. 
// Вектор specular устанавливает цвет блика на объекте, а переменная shininess — радиус этого блика.
  
uniform vec3 lightPos;  // положение источника света
uniform vec3 FragPos;  // положение зрителя

void main(void)
{
    float lighting = 1.0f;

    //Материал
    if (enable_material)
    {
        // ambient
        vec3 ambient = lighting * ambientM;

        // diffuse 
        vec3 norm = normalize(normal);
        vec3 lightDir = normalize(lightPos - FragPos);
        float diff = max(dot(norm, light_direction), 0.0);
        vec3 diffuse = lighting * (diff * diffuseM);

        // specular
        vec3 viewDir = normalize(position - FragPos);
        vec3 reflectDir = reflect(-light_direction, norm);  
        float spec = pow(max(dot(viewDir, reflectDir), 0.0), shininessM);
        vec3 specular = lighting * (spec * specularM);  

        vec3 result = ambient + diffuse + specular;
        fragment = vec4(result, 1.0);
    }
    else 
    {
        float diffuse = max(dot(normal, light_direction), 0);
        float ambient = 0.4f;
        lighting = (enable_lighting ? max(diffuse, ambient) : 1);
    }   

    //Текстура или цвет 
    if (!enable_material)
    {
        if (enable_texture) fragment = vec4(lighting * texture2D(texture, uv).xyz, 0.5);  // используем текстуру
            else fragment = lighting * vec4(color, 1);  // иначе используем заданный цвет
    }
}
";
    }
}
