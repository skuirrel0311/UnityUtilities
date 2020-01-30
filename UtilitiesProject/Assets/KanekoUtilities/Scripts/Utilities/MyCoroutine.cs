using System;
using System.Collections;
using System.Collections.Generic;

namespace KanekoUtilities
{
    public class MyCoroutine : IEnumerator
    {
        protected IEnumerator logic;
        Action onCompleted;
        public bool IsDone { get; private set; }

        public MyCoroutine(IEnumerator logic)
        {
            this.logic = logic;
        }

        public bool MoveNext()
        {
            Update();
            if (IsDone)
            {
                onCompleted.SafeInvoke();
                onCompleted = null;
            }
            return !IsDone;
        }
        public void Reset()
        {
            logic.Reset();
        }
        public object Current
        {
            get
            {
                return logic.Current;
            }
        }

        void Update()
        {
            IsDone = !logic.MoveNext();
        }

        public MyCoroutine OnCompleted(Action onCompleted)
        {
            this.onCompleted += onCompleted;
            return this;
        }

        public void CallCompletedSelf()
        {
            onCompleted.SafeInvoke();
            onCompleted = null;
        }
    }

    public class PalallelCoroutine : IEnumerator
    {
        List<MyCoroutine> logics = new List<MyCoroutine>();
        Action onCompleted;

        public bool IsDone
        {
            get
            {
                foreach (var l in logics) if (!l.IsDone) return false;

                return true;
            }
        }

        public PalallelCoroutine(MyCoroutine logic)
        {
            Join(logic);
        }

        public void Join(MyCoroutine logic)
        {
            logics.Add(logic);
        }

        public bool MoveNext()
        {
            Update();
            if (IsDone)
            {
                onCompleted.SafeInvoke();
                onCompleted = null;
            }
            return !IsDone;
        }
        public void Reset()
        {
            foreach (var l in logics) l.Reset();
        }
        public object Current
        {
            get
            {
                //logics[0]がnullの可能性は考えない
                return logics[0].Current;
            }
        }

        public PalallelCoroutine OnCompleted(Action onCompleted)
        {
            this.onCompleted += onCompleted;
            return this;
        }

        void Update()
        {
            foreach (var l in logics)
            {
                if (l.IsDone) continue;

                l.MoveNext();
            }
        }
    }

    public class SequenceCoroutine
    {
        Queue<PalallelCoroutine> logicQueue = new Queue<PalallelCoroutine>();
        public bool IsDone { get; private set; }
        
        IEnumerator Update()
        {
            yield return null;

        }

        //Coroutineを末尾に追加
        public void Append(MyCoroutine logic)
        {
        }

        public void Join(MyCoroutine logic)
        {

        }
    }
}