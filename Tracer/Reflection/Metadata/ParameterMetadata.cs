namespace Reflection.Metadata
{
    internal class ParameterMetadata
    {
        #region privateFields
        internal string m_Name;
        internal TypeMetadata m_TypeMetadata;
        #endregion

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }
    }
}