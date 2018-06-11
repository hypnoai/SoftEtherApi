using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SoftEtherApi.Containers;
using SoftEtherApi.Infrastructure;

namespace SoftEtherApi.Model
{
    public abstract class BaseSoftEtherModel<T> where T : BaseSoftEtherModel<T>, new()
    {
        public SoftEtherError Error = SoftEtherError.NoError;

        public bool Valid()
        {
            return Error == SoftEtherError.NoError;
        }
        
        public bool NotValid()
        {
            return !Valid();
        }

        public static T Deserialize(SoftEtherParameterCollection collection)
        {
            return ModelDeserializer.Deserialize<T>(collection);
        }

        public static SoftEtherList<T> DeserializeMany(SoftEtherParameterCollection collection)
        {
            return ModelDeserializer.DeserializeMany<T>(collection);
        }
    }
}