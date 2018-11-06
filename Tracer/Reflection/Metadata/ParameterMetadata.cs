namespace Reflection.Metadata
{
    public class ParameterMetadata
    {
        #region Fields
        public string m_Name;
        public TypeMetadata m_TypeMetadata;
        #endregion

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }
    }
}