using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Kryz.CharacterStats {
    [Serializable]
    public class CharacterStat : Observer, Observable {
        public GameObject owner;

        [SerializeField] private float _baseValue; //Поле обязательно должно быть приватное но сериализованное (Чтобы иметь доступ назначать базовое значение в инспекторе но не иметь доступа обращаться к этой переменной не из свойства)
        public float BaseValue {
            get { return _baseValue; }
            set {
                _baseValue = value;
                NotifyObservers();
            }
        }

        protected bool isDirty = true;
        protected float lastBaseValue;

        [SerializeField] protected float _value; //Убрать сериализацию, нужно просто для дебага

        public virtual float Value {
            get {
                if (isDirty || lastBaseValue != BaseValue) {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            }
            set {
                _value = value;
                NotifyObservers();
            }
        }

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat() {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this() { //Это вообще не нужно судя по всему, так как не создаю через new, возможно Unity использует new в инспекторе
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod) {
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
            lastBaseValue = BaseValue;
            Value = CalculateFinalValue();
        }

        public virtual bool RemoveModifier(StatModifier mod) {
            if (statModifiers.Remove(mod)) {
                lastBaseValue = BaseValue;
                Value = CalculateFinalValue();
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source) {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--) {
                if (statModifiers[i].Source == source) {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
                lastBaseValue = BaseValue;
                Value = CalculateFinalValue();
            }

            return didRemove;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b) {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; //if (a.Order == b.Order)
        }

        protected virtual float CalculateFinalValue() {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++) {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat) {
                    finalValue += mod.Value;
                } else if (mod.Type == StatModType.PercentAdd) {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd) {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                } else if (mod.Type == StatModType.PercentMult) {
                    finalValue *= 1 + mod.Value;
                }
            }

            return (float) Math.Round(finalValue, 4);
        }

        #region Observer and Observable realisation

        [SerializeField] private List<Observer> observers = new List<Observer>();

        public void ObserverUpdate() {
            if (owner) {
                owner.GetComponent<UnitStats>().UnitAttributes.CalculateAttributes();
                //Debug.Log("Receive update, someone change stat!");
            }
        }

        public void AddObserver(Observer o) {
            observers.Add(o);
        }

        public void RemoveObserver(Observer o) {
            observers.Remove(o);
        }

        public void NotifyObservers() {
            foreach (Observer observer in observers) {
                observer.ObserverUpdate();
            }
        }

        #endregion
    }
}
/*
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Kryz.CharacterStats {
    [Serializable]
    public class CharacterStat : Observer, Observable {
        public GameObject owner;

        public float BaseValue;

        protected bool isDirty = true;
        protected float lastBaseValue;

        [SerializeField] protected float _value;

        public virtual float Value {
            get {
                if (isDirty || lastBaseValue != BaseValue) {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                NotifyObservers();
                return _value;
            }
        }

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat() {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this() {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod) {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier mod) {
            if (statModifiers.Remove(mod)) {
                isDirty = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source) {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--) {
                if (statModifiers[i].Source == source) {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b) {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; //if (a.Order == b.Order)
        }

        protected virtual float CalculateFinalValue() {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++) {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat) {
                    finalValue += mod.Value;
                } else if (mod.Type == StatModType.PercentAdd) {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd) {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                } else if (mod.Type == StatModType.PercentMult) {
                    finalValue *= 1 + mod.Value;
                }
            }

            return (float) Math.Round(finalValue, 4);
        }

        #region Observer and Observable realisation

        [SerializeField] private List<Observer> observers = new List<Observer>();

        public void ObserverUpdate() {
            if (owner) {
                owner.GetComponent<UnitStats>().UnitAttributes.CalculateAttributes();
                Debug.Log("Receive update, someone change stat!");
            }
        }

        public void AddObserver(Observer o) {
            observers.Add(o);
        }

        public void RemoveObserver(Observer o) {
            observers.Remove(o);
        }

        public void NotifyObservers() {
            foreach (Observer observer in observers) {
                observer.ObserverUpdate();
            }
        }

        #endregion
    }
}
*/