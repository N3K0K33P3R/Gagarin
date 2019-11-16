using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFlash.Engine
{
    public enum Transitions
    {
        position,
        rotation,
        alpha,
        none
    }

    public class Sprite : IDrawable
    {
        private AnimationController acAlpha;
        private AnimationController acRot;

        private AnimationController acX;
        private AnimationController acY;
        protected double alpha;

        protected Color colorAlpha = Color.White;
        protected RenderTarget2D data;

        private double scale;

        private double scaleX;

        private double scaleY;


        public Sprite()
        {
            childs = new List<IDrawable>();
            x = 0;
            y = 0;
            Scale = 1;
            Alpha = 1;
        }

        public double x { get; set; }
        public double y { get; set; }

        public double globalX
        {
            get
            {
                if (parent != null)
                    return parent.globalX + x;
                return x;
            }
        }

        public double globalY
        {
            get
            {
                if (parent != null)
                    return parent.globalY + y;
                return y;
            }
        }

        public double width { get; set; }
        public double height { get; set; }

        public double rotation { get; set; }

        public double Alpha
        {
            get
            {
                if (parent == null) return alpha;
                return alpha * parent.Alpha;
            }
            set
            {
                if (value > 1)
                    value = 1;
                else if (value < 0) value = 0;
                alpha = value;
                //Console.WriteLine(value);
                colorAlpha.A = (byte) (alpha * 255);
            }
        }

        public List<IDrawable> childs { get; }

        public IDrawable parent { get; set; }

        public double Scale
        {
            get
            {
                if (parent == null) return 1;
                return scale * parent.Scale;
            }

            set
            {
                if (value < 0) value = 0;

                scale = value;
                scaleX = value;
                scaleY = value;
            }
        }

        public double ScaleX
        {
            get
            {
                if (parent == null) return 1;
                return scaleX * parent.ScaleX;
            }

            set
            {
                if (value < 0) value = 0;
                scaleX = value;
            }
        }

        public double ScaleY
        {
            get
            {
                if (parent == null) return 1;
                return scaleY * parent.ScaleY;
            }

            set
            {
                if (value < 0) value = 0;
                scaleY = value;
            }
        }

        public virtual void Update(float delta)
        {
            if (acX != null) x = acX.MakeStep(delta);
            if (acY != null) y = acY.MakeStep(delta);
            if (acRot != null) rotation = acRot.MakeStep(delta);
            if (acAlpha != null) alpha = acAlpha.MakeStep(delta);
            //double minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
            //foreach (var item in childs)
            //{
            //    double curX = item.x - item.width / 2;
            //    double curY = item.y - item.height / 2;

            //    if (curX < minX)
            //    {
            //        minX = curX;
            //    }

            //    if (curY < minY)
            //    {
            //        minY = curY;
            //    }

            //    curX = item.x + item.width / 2;
            //    curY = item.y + item.height / 2;

            //    if (curX > maxX)
            //    {
            //        maxX = curX;
            //    }

            //    if (curY > maxY)
            //    {
            //        maxY = curY;
            //    }
            //}

            //width = Math.Abs(maxX - minX);
            //height = Math.Abs(maxX - minX);
            for (var i = childs.Count - 1; i >= 0; i--)
                if (i < childs.Count)
                    childs[i].Update(delta);
        }

        public virtual void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            foreach (var item in childs) item.Draw(sb, gameTime);
        }

        public void AddChild(IDrawable child)
        {
            if (child != null)
                if (!childs.Contains(child))
                {
                    childs.Add(child);
                    child.parent = this;
                }
        }

        public void RemoveChild(IDrawable child)
        {
            childs.Remove(child);
        }

        public void RemoveChildren()
        {
            childs.RemoveRange(0, childs.Count);
        }

        public void RemoveChildAt(int id)
        {
            childs.RemoveAt(id);
        }

        public void AddChildAt(IDrawable child, int id)
        {
            if (!childs.Contains(child))
            {
                childs.Insert(id, child);
                child.parent = this;
            }
            else
            {
                throw new ArgumentException("Object is already has that child");
            }
        }

        /// <summary>
        ///     Вывод в консоль
        /// </summary>
        /// <param name="list">Выводимые объекты</param>
        public void Trace(params object[] list)
        {
            foreach (var item in list)
                if (item != null)
                    Console.Write(item + " ");
            Console.WriteLine();
        }

        /// <summary>
        ///     Сделать переход определённого типа плавным
        /// </summary>
        /// <param name="type">Тип перехода</param>
        public void SetTransition(Transitions type)
        {
            switch (type)
            {
                case Transitions.position:
                    acX = new AnimationController((float) x);
                    acY = new AnimationController((float) y);
                    break;
                case Transitions.rotation:
                    acRot = new AnimationController((float) rotation);
                    break;
                case Transitions.alpha:
                    acAlpha = new AnimationController((float) alpha);
                    break;
                case Transitions.none:
                    acX = acY = acRot = acAlpha = null;
                    break;
            }
        }

        /// <summary>
        ///     Переместить спрайт.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="speed"></param>
        /// <param name="onEnd">Действие при завершении анимации</param>
        /// <param name="offset">Задержка</param>
        public void SetPosition(float x, float y, float speed = 1, Action onEnd = null, float offset = 0)
        {
            if (acX != null)
            {
                acX.StartAnimation(Maths.easeInOutQuad, this.x, x, speed, finished: onEnd, offset: offset);
                acY.StartAnimation(Maths.easeInOutQuad, this.y, y, speed, offset: offset);
            }
            else
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}