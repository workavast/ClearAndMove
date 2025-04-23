using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Avastrad.SavingAndLoading
{
    public class BinarySaveAndLoader : ISaveAndLoader
    {
        private readonly string _saveFileName;
        private string SavePath => Path.Combine(Application.dataPath, _saveFileName);

        public BinarySaveAndLoader(string saveFileName = "Save")
        {
            _saveFileName = saveFileName;
        }
        
        public void Save(object data)
        {
            using (FileStream stream = new FileStream(SavePath, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
            }
        }

        public T TryLoad<T>() 
            where T : new()
        {
            if (!Exist())
                return new T();
            
            return Load<T>();
        }

        public T Load<T>()
        {
            if (!Exist())
                throw new ArgumentException("Save doesnt exist");

            T loadedData;
            using (FileStream stream = new FileStream(SavePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                loadedData = (T) binaryFormatter.Deserialize(stream);
            }
            
            return loadedData;
        }

        public bool Exist() 
            => File.Exists(SavePath);

        public void DeleteSave() 
            => File.Delete(SavePath);
    }
}