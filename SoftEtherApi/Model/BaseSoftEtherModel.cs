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

        public static SoftEtherList<T> DeserializeMany(SoftEtherParameterCollection collection, bool moreThanOne = true)
        {
            return ModelDeserializer.DeserializeMany<T>(collection, moreThanOne);
        }
    }
}