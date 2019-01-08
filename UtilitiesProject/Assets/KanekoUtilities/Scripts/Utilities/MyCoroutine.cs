using System;
using System.Collections;

namespace KanekoUtilities
{
    public class MyCoroutine : IEnumerator
    {
        protected IEnumerator logic;
        Action onCompleted;
        Action onStart;
        Action onUpdate;

        public MyCoroutine(IEnumerator logic)
        {
            this.logic = logic;
        }

        public bool MoveNext()
        {
            return Start().MoveNext();
        }
        public void Reset()
        {
            Start().Reset();
        }
        public object Current
        {
            get
            {
                return Start().Current;
            }
        }

        IEnumerator Start()
        {
            onStart.SafeInvoke();
            while (logic.MoveNext())
            {
                onUpdate.SafeInvoke();
                yield return null;
            }

            onCompleted.SafeInvoke();
            onCompleted = null;
        }

        public MyCoroutine OnCompleted(Action onCompleted)
        {
            this.onCompleted += onCompleted;
            return this;
        }
        public MyCoroutine OnStart(Action onStart)
        {
            this.onStart += onStart;
            return this;
        }
        public MyCoroutine OnUpdate(Action onUpdate)
        {
            this.onUpdate += onUpdate;
            return this;
        }

        public void CallCompletedSelf()
        {
            onCompleted.SafeInvoke();
            onCompleted = null;
        }
    }
}