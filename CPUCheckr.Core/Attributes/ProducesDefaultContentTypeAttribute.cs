using Microsoft.AspNetCore.Mvc;

namespace CPUCheckr.Core.Attributes;

public class ProducesDefaultContentTypeAttribute : ProducesAttribute
{
    public ProducesDefaultContentTypeAttribute(Type type) : base(type)
    {
    }

    public ProducesDefaultContentTypeAttribute(params string[] additionalContentTypes) 
        : base("application/json", additionalContentTypes)
    {
    }
}