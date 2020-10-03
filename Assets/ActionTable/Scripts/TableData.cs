using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionTableSpace {
    public class TableData : MonoBehaviour
    {
        [Header("Size")]
        public float height = 25;
        public float width = 40; 

        [Space]

        public List<DataForObject> dataForObjects;

        public TypeResult GetData(TypeObject _object, TypeAction _action) {
            DataForObject _data = dataForObjects.Find(x => x.typeObject == _object);
            DataWithAction _dataAction = _data.withActions.Find(x => x.typeAction == _action);
            return _dataAction.typeResult;
        }

        public void SetData(TypeObject _object, TypeAction _action, TypeResult _result)
        {
            DataForObject _data = dataForObjects.Find(x => x.typeObject == _object);
            DataWithAction _dataAction = _data.withActions.Find(x => x.typeAction == _action);
            _dataAction.typeResult = _result;
        }


        public void Initialization() {
            
            if (dataForObjects == null || dataForObjects.Count == 0)
            {
                Debug.LogError(dataForObjects.Count);
                TypeObject temp;
                for (int i = 0; i < Enum.GetNames(typeof(TypeObject)).Length; i++)
                {
                    temp = (TypeObject)i;
                    dataForObjects.Add(new DataForObject(temp));
                }

            }
        }
    }

    [System.Serializable]
    public class DataForObject
    {
        public TypeObject typeObject;
        public List<DataWithAction> withActions;

        public DataForObject(TypeObject typeObject)
        {
            Initialization(typeObject);
        }

        public void Initialization(TypeObject type)
        {
            typeObject = type;
            withActions = new List<DataWithAction>();
            TypeAction temp;
            for (int i = 0; i < Enum.GetNames(typeof(TypeAction)).Length; i++)
            {
                temp = (TypeAction)i;
                withActions.Add(new DataWithAction(temp));
            }
        }

    }

    [System.Serializable]
    public class DataWithAction{
        public TypeAction typeAction;
        public TypeResult typeResult;

        public DataWithAction(TypeAction typeAction) {
            this.typeAction = typeAction;
        }
    }

    public enum TypeObject {
        Object1,
        Object2
    }

    public enum TypeAction
    {
        Action1,
        Action2,
        Action3
    }

    public enum TypeGUIStyle
    {
        Button,
        BackButton,
        Box,
        EmptyBox
    }

    public enum TypeTable
    {
        Table,
        Select
    }



    public enum TypeResult
    {
        Result1,
        Result2,
        Result3,
        Result4,
        Result5
    }

    public class CurrentSelect {
        public TypeObject typeObject;
        public TypeAction typeAction;

        public CurrentSelect(TypeObject typeObject, TypeAction typeAction) {
            this.typeObject = typeObject;
            this.typeAction = typeAction;
        }
    }
}