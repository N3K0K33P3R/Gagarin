using System;

namespace MonoFlash.Engine
{
    public delegate double Animation(double i, double start, double target);

    public class AnimationController
    {
        private Animation animation;
        public double defaultValue;
        private Action finished;

        public double i;
        private bool isCyclic, beenStarted;
        public bool isStarted;
        private double start, target, speed = 0.1f, curent, offset;

        /// <summary>
        /// </summary>
        /// <param name="defaultValue">Значение по умолчанию</param>
        public AnimationController(float defaultValue)
        {
            i = 0;
            isStarted = false;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        ///     Запустить анимацию
        /// </summary>
        /// <param name="newAnimation">Функция анимации. Заготовки в Maths.cs</param>
        /// <param name="start">Начальное значение</param>
        /// <param name="target">Конечное</param>
        /// <param name="speed">Скорость</param>
        /// <param name="isCyclic">Повторяется ли</param>
        /// <param name="offset">Задержка</param>
        /// <param name="finished">Функция выполняющаяся в конце</param>
        public void StartAnimation(Animation newAnimation, double start, double target, double speed = 0.1f,
            bool isCyclic = false, float offset = 0, Action finished = null)
        {
            animation = newAnimation;
            this.start = start;
            this.target = target;
            this.speed = speed;
            isStarted = true;
            i = -offset;
            this.isCyclic = isCyclic;

            this.offset = offset;
            beenStarted = true;
            this.finished = finished;
        }

        /// <summary>
        ///     Запустить аниацию в обратном порядке
        /// </summary>
        /// <param name="finished">Функция, которая вызовется по завершению</param>
        public void Reverse(Action finished = null)
        {
            StartAnimation(animation, curent, start, speed, isCyclic, 0, finished);
        }

        /// <summary>
        ///     Сделать шаг анимации, возвращает значение её
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public double MakeStep(float delta)
        {
            curent = start;
            if (isStarted && i <= 1)
            {
                if (i >= 0)
                    curent = animation(i, start, target);
                //Console.WriteLine(i+ " " + start +" " + target);
                i += speed;
            }
            else if (isStarted)
            {
                curent = target;
                if (!isCyclic)
                {
                    isStarted = false;

                    finished?.Invoke();
                }
                else
                {
                    Reverse(finished);
                }
            }
            else
            {
                curent = target;
            }

            if (!beenStarted) return defaultValue;
            return curent;
        }
    }
}