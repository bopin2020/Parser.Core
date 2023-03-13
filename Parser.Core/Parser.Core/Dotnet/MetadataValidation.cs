using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet
{
    public interface IValidate
    {
        bool Validate();
    }

    /// <summary>
    /// II.22.1 Metadata validation rules 
    /// ecma 236
    /// </summary>
    public abstract class MetadataValidation : IValidate
    {
        public abstract string Name { get; set; }

        public List<Dictionary<string, bool>> Results = new();

        public DotnetParser DotnetMetadata;

        public abstract bool Validate();
    }
}
