namespace Reflection
{
    internal class ParameterMetadata
    {
        #region privateFields
        private string m_Name;
        private TypeMetadata m_TypeMetadata;
        #endregion

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }
    }
}