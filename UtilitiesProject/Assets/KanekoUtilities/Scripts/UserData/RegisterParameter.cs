using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public abstract class RegisterParameter<T>
    {
        protected string key;
        protected T value;
        public MyUnityEvent<T> OnValueChanged = new MyUnityEvent<T>();

        public RegisterParameter(string key, T defaultValue)
        {
            this.key = key;
            value = LoadValue(defaultValue);
        }
        public RegisterParameter(SaveKeyName key, T defaultValue)
        {
            this.key = key.ToString();
            value = LoadValue(defaultValue);
        }

        public T GetValue() { return value; }
        protected abstract T LoadValue(T defaultValue);
        public abstract void SetValue(T value);
    }

    public class RegisterIntParameter : RegisterParameter<int>
    {
        public RegisterIntParameter(string key, int defaultValue) : base(key, defaultValue) { }
        public RegisterIntParameter(SaveKeyName key, int defaultValue) : base(key, defaultValue) { }

        protected override int LoadValue(int defaultValue)
        {
            return MyPlayerPrefs.LoadInt(key, defaultValue);
        }

        public override void SetValue(int value)
        {
            if (GetValue() == value) return;

            OnValueChanged.Invoke(value);
            MyPlayerPrefs.SaveInt(key, value);
        }
    }

    public class RegisterFloatParameter : RegisterParameter<float>
    {
        public RegisterFloatParameter(string key, float defaultValue) : base(key, defaultValue) { }
        public RegisterFloatParameter(SaveKeyName key, float defaultValue) : base(key, defaultValue) { }

        protected override float LoadValue(float defaultValue)
        {
            return MyPlayerPrefs.LoadFloat(key, defaultValue);
        }

        public override void SetValue(float value)
        {
            if (GetValue() == value) return;

            OnValueChanged.Invoke(value);
            MyPlayerPrefs.SaveFloat(key, value);
        }
    }

    public class RegisterStringParameter : RegisterParameter<string>
    {
        public RegisterStringParameter(string key, string defaultValue) : base(key, defaultValue) { }
        public RegisterStringParameter(SaveKeyName key, string defaultValue) : base(key, defaultValue) { }

        protected override string LoadValue(string defaultValue)
        {
            return MyPlayerPrefs.LoadString(key, defaultValue);
        }

        public override void SetValue(string value)
        {
            if (GetValue() == value) return;

            OnValueChanged.Invoke(value);
            MyPlayerPrefs.SaveString(key, value);
        }
    }

    public class RegisterBoolParameter : RegisterParameter<bool>
    {
        public RegisterBoolParameter(string key, bool defaultValue) : base(key, defaultValue) { }
        public RegisterBoolParameter(SaveKeyName key, bool defaultValue) : base(key, defaultValue) { }

        protected override bool LoadValue(bool defaultValue)
        {
            return MyPlayerPrefs.LoadBool(key, defaultValue);
        }

        public override void SetValue(bool value)
        {
            if (GetValue() == value) return;

            OnValueChanged.Invoke(value);
            MyPlayerPrefs.SaveBool(key, value);
        }
    }
}